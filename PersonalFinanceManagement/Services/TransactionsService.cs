﻿using CsvHelper;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Data;
using PersonalFinanceManagement.Dto;
using PersonalFinanceManagement.Mappers;
using PersonalFinanceManagement.Models;
using PersonalFinanceManagement.Models.Helpers;
using PersonalFinanceManagement.Services.Interfaces;
using System.Globalization;

namespace PersonalFinanceManagement.Services
{
    public class TransactionsService : ITransactionsInterface
    {
        private PFMDbContext Context;

        public TransactionsService(PFMDbContext context)
        {
            this.Context = context;
        }

        public List<TransactionDto> GetList(string transactionKind,
                                            DateTime? startDate,
                                            DateTime? endDate,
                                            int? page,
                                            int? pageSize)
        {
            var transactions = Context.Transactions.Where(t => t.Kind == transactionKind);

            if (startDate != null)
            {
                transactions = transactions.Where(t => t.Date >= startDate);
            }

            if (endDate != null)
            {
                transactions = transactions.Where(t => t.Date <= endDate);
            }

            transactions = transactions.Skip((page ?? 0) * (pageSize ?? 25)).Take(pageSize ?? 25);

            return Context.Transactions
                .ToList()
                .Select(t => TransactionsFactory.ToDto(t))
                .ToList();
        }

        public async Task<TransactionDto> GetTransactionById(int id)
        {
            TransactionsModel transaction = await Context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                return TransactionsFactory.ToDto(transaction);
            }
            else return null;
        }

        public async Task<List<TransactionsModel>> ImportAsync(IFormFile csv)
        {
            using (var DbTransaction = Context.Database.BeginTransaction())
            {
                using (var streamReader = new StreamReader(csv.OpenReadStream()))
                {
                    using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        csvReader.Context.RegisterClassMap<TransactionMapper>();

                        List<TransactionsModel> transactions = csvReader.GetRecords<TransactionsModel>().ToList();

                        transactions = transactions.Where(t =>
                        {
                            if (t.Amount < 0)
                            {
                                return false;
                            }
                            else if (t.Id == null)
                            {
                                return false;
                            }
                            else if (t.Kind == null)
                            {
                                return false;
                            }
                            //else if (t.Kind != "pmt" || t.Kind != "dep" || t.Kind != "fee" || t.Kind != "sal" || t.Kind != "wdw")
                            //{
                            //    return false;
                            //}
                            else if (t.Kind == null)
                            {
                                return false;
                            }
                            else if (t.Direction == null)
                            {
                                return false;
                            }
                            //else if (t.Direction != "d" || t.Direction != "c")
                            //{
                            //    return false;
                            //}
                            return true;
                        }).ToList();

                        await Context.Transactions.AddRangeAsync(transactions);

                        await Context.SaveChangesAsync();

                        await DbTransaction.CommitAsync();

                        return transactions;
                    }
                }
            }
        }

        public async Task<TransactionDto> CategorizeTransaction(int id, CategorizeRequest request)
        {
            using (var DbTransaction = Context.Database.BeginTransaction())
            {
                CategoriesModel category = await GetCategoryByCode(request.CategoryCode);
                TransactionsModel transaction = await Context.Transactions.FindAsync(id);
                if (category == null || transaction == null)
                {
                    await DbTransaction.RollbackAsync();
                    return null;
                }

                transaction.categoriesModel = category;

                await Context.SaveChangesAsync();

                await DbTransaction.CommitAsync();

                return TransactionsFactory.ToDto(transaction);
            }
        }

        public async Task<GroupCategories> SpendingAnalytics(string Category, DateTime? startDate, DateTime? endDate, string? direction)
        {
            using (var DbTransaction = Context.Database.BeginTransaction())
            {
                var group = new GroupCategories();
                int count = 0;
                float Ammount = 0;

                var transactions = Context.Transactions
                    .Where(c => c.categoriesModel.Code == Category || c.categoriesModel.ParentCode == Category);

                if (startDate != null)
                {
                    transactions = transactions.Where(t => t.Date >= startDate);
                }

                if (endDate != null)
                {
                    transactions = transactions.Where(t => t.Date <= endDate);
                }

                if (direction != null)
                {
                    transactions = transactions.Where(t => t.Direction == direction);
                }

                foreach (var transaction in (await transactions.ToListAsync()))
                {
                    count++;
                    Ammount += transaction.Amount;
                }

                group.SpendingInCategories.Add(new SpendingInCat() { Code = Category, Amount = Ammount, Count = count });
                await DbTransaction.CommitAsync();
                return group;
            }
        }

        public async Task<TransactionDto> SplitTransaction(int id, List<string> codes)
        {
            using (var DbTransaction = Context.Database.BeginTransaction())
            {

                if (codes == null) {
                    await DbTransaction.RollbackAsync();
                    return null;
                }

                var transaction = Context.Transactions.Find(id);

                if (transaction == null)
                {
                    await DbTransaction.RollbackAsync();
                    return null;
                }

                Context.TransactionSplits.RemoveRange(transaction.Splits);

                var categories = Context.Categories.Where(c => codes.Contains(c.Code));

                if(categories.Count() != codes.Count())
                {
                    await DbTransaction.RollbackAsync();
                    return null;
                }

                List<TransactionsSplit> splits = new List<TransactionsSplit>();

                foreach (var category in categories)
                {
                    splits.Add(new TransactionsSplit
                    {
                        Transaction = transaction,
                        Category = category,
                        Amount = transaction.Amount / codes.Count
                    });
                }

                transaction.Splits = splits;

                await Context.TransactionSplits.AddRangeAsync(splits);

                await Context.SaveChangesAsync();

                await DbTransaction.CommitAsync();

                return TransactionsFactory.ToDto(transaction);
            }
        }

        public async Task<CategoriesModel> GetCategoryByCode(string code)
        {
            return await Context.Categories.FindAsync(code);
        }

        //public async Task<TransactionDto> AutoCategorize()
        //{
        //    var transactions = Context.Transactions.ToList();
        //    foreach(var transaction in transactions)
        //    {
        //        if (transaction.categoriesModel != null)
        //        {
        //            return null;
        //        }
        //        else {          
        //        }
        //    }
        //    return null;
        //}

    }
}