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

        public TransactionsModel GetTransactionById(int id)
        {
            TransactionsModel transaction = Context.Transactions.Find(id);
            return transaction;
        }

        public List<TransactionsModel> Import(IFormFile csv)
        {
            using (var streamReader = new StreamReader(csv.OpenReadStream()))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<TransactionMapper>();

                    List<TransactionsModel> transactions = csvReader.GetRecords<TransactionsModel>().ToList();

                    Context.Transactions.AddRange(transactions);

                    return transactions;
                }
            }
        }


        public async Task<TransactionsModel> CategorizeTransaction(int id, CategorizeRequest request)
        {
            CategoriesModel category = await GetCategoryByCode(request.CategoryCode);
            TransactionsModel transaction = GetTransactionById(id);
            if (category == null || transaction == null)
            {
                return null;
            }

            transaction.categoriesModel = category;

            Context.SaveChanges();

            return transaction;
        }

        private async Task<CategoriesModel> GetCategoryByCode(string code)
        {
            return await Context.Categories.Where(c => c.Code == code).FirstAsync();
        }
    }
}