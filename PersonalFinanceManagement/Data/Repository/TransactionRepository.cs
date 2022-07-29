using PersonalFinanceManagement.Data.Interfaces;
using PersonalFinanceManagement.Dto;
using PersonalFinanceManagement.Mappers;
using PersonalFinanceManagement.Models;
using PersonalFinanceManagement.Models.Helpers;
using PersonalFinanceManagement.Services.Interfaces;

namespace PersonalFinanceManagement.Data.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly PFMDbContext Context;

        public TransactionRepository(PFMDbContext context)
        {
            this.Context = context;
        }


        public async Task<TransactionsModel> GetTransactionById(int id)
        {

            var result = await Context.Transactions.FindAsync(id);
            if (result == null)
            {
                return null;
            }
            else return result;
        }

        public async Task<CategoriesModel> GetCategoryByCode(string code)
        {
            var result = await Context.Categories.FindAsync(code);
            if (result == null)
            {
                return null;
            }
            else return result;
        }

        public async Task<List<TransactionsModel>> InsertTransactions(List<TransactionsModel> transactions)
        {
            using (var DbTransaction = Context.Database.BeginTransaction())
            {
                await Context.Transactions.AddRangeAsync(transactions);

                await Context.SaveChangesAsync();

                await DbTransaction.CommitAsync();

                return transactions;
            }
        }

        public async Task<List<TransactionsModel>> getByCategory(string Category, DateTime? startDate, DateTime? endDate, string? direction)
        {

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

            return transactions.ToList();
        }

        public async Task<List<TransactionDto>> getList(string transactionKind,
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

            return transactions
                .ToList()
                .Select(t => TransactionsFactory.ToDto(t))
                .ToList();

        }


        public async Task<TransactionDto> categorizeTransaction(int id, CategorizeRequest request)
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


        public async Task<TransactionDto> SplitTransactions(int id, List<string> codes)
        {
            using (var DbTransaction = Context.Database.BeginTransaction())
            {

                if (codes == null)
                {
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

                if (categories.Count() != codes.Count())
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

    }
}

