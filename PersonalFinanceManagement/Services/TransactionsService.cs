using CsvHelper;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Data;
using PersonalFinanceManagement.Dto;
using PersonalFinanceManagement.Mappers;
using PersonalFinanceManagement.Models;
using PersonalFinanceManagement.Models.Helpers;
using System.Globalization;

namespace PersonalFinanceManagement.Services
{
    public class TransactionsService
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
            using (var streamReader = new StreamReader(csv.OpenReadStream()))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<TransactionMapper>();

                    List<TransactionsModel> transactions = csvReader.GetRecords<TransactionsModel>().ToList();

                    

                    //tuka proverki pravam

                    await Context.Transactions.AddRangeAsync(transactions);

                    await Context.SaveChangesAsync();

                    return transactions;
                }
            }
        }

        public async Task<TransactionDto> CategorizeTransaction(int id, CategorizeRequest request)
        {
            CategoriesModel category = await GetCategoryByCode(request.CategoryCode);
            TransactionsModel transaction = await Context.Transactions.FindAsync(id);
            if (category == null || transaction == null)
            {
                return null;
            }

            transaction.categoriesModel = category;

            Context.SaveChangesAsync();

            return TransactionsFactory.ToDto(transaction);
        }

        public async Task<GroupCategories> SpendingAnalytics(string Category, DateTime? startDate, DateTime? endDate, string? direction)
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

            if(direction != null)
            {
                transactions = transactions.Where(t => t.Direction == direction);
            }

            foreach (var transaction in (await transactions.ToListAsync()))
            {
                count++;
                Ammount += transaction.Amount;
            }

            group.SpendingInCategories.Add(new SpendingInCat() { Code = Category, Amount = Ammount, Count = count });
            return group;

        }

        //public async Task<TransactionDto> SplitTransaction() 
        //{
        
        //}

        private async Task<CategoriesModel> GetCategoryByCode(string code)
        {
            return await Context.Categories.FindAsync(code);
        }
    }
}