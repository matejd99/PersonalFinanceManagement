using CsvHelper;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Data;
using PersonalFinanceManagement.Data.Repository;
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

        private readonly TransactionRepository repository;
        public TransactionsService(TransactionRepository repository)
        {
            this.repository = repository;
        }

        //private PFMDbContext Context;

        //public TransactionsService(PFMDbContext context)
        //{
        //    this.Context = context;
        //}

        public async Task<List<TransactionDto>> GetList(string transactionKind,
                                            DateTime? startDate,
                                            DateTime? endDate,
                                            int? page,
                                            int? pageSize)
        {
            return await repository.getList(transactionKind, startDate, endDate, page, pageSize);
        }

        public async Task<TransactionDto> GetTransactionById(int id)
        {
            TransactionsModel transaction = await repository.GetTransactionById(id);
            if (transaction != null)
            {
                return TransactionsFactory.ToDto(transaction);
            }
            else return null;
        }

        public async Task<List<TransactionsModel>> ImportAsync(IFormFile csv)
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
                        else if (t.Kind == null)
                        {
                            return false;
                        }
                        else if (t.Direction == null)
                        {
                            return false;
                        }
                        return true;
                    }).ToList();
                    return await repository.InsertTransactions(transactions);
                }
            }
        }


        public async Task<TransactionDto> CategorizeTransaction(int id, CategorizeRequest request)
        {
            return await repository.categorizeTransaction(id, request);
        }

        public async Task<GroupCategories> SpendingAnalytics(string Category, DateTime? startDate, DateTime? endDate, string? direction)
        {
            var group = new GroupCategories();
            int count = 0;
            float Ammount = 0;

            var transactions = await repository.getByCategory(Category, startDate, endDate, direction);

            foreach (var transaction in transactions)
            {
                count++;
                Ammount += transaction.Amount;
            }

            group.SpendingInCategories.Add(new SpendingInCat() { Code = Category, Amount = Ammount, Count = count });
            return group;

        }

        public async Task<TransactionDto> SplitTransaction(int id, List<string> codes)
        {
            return await repository.SplitTransactions(id, codes);
        }

        public async Task<CategoriesModel> GetCategoryByCode(string code)
        {
            return await repository.GetCategoryByCode(code);
        }

    }
}