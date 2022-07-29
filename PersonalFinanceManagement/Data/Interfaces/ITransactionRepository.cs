using PersonalFinanceManagement.Dto;
using PersonalFinanceManagement.Models;
using PersonalFinanceManagement.Models.Helpers;

namespace PersonalFinanceManagement.Data.Interfaces
{
    public interface ITransactionRepository
    {
        Task<TransactionsModel> GetTransactionById(int id);
        Task<CategoriesModel> GetCategoryByCode(string code);

        Task<List<TransactionsModel>> InsertTransactions(List<TransactionsModel> transactions);

        Task<List<TransactionsModel>> getByCategory(string Category, DateTime? startDate, DateTime? endDate, string? direction);

        Task<List<TransactionDto>> getList(string transactionKind,
                                            DateTime? startDate,
                                            DateTime? endDate,
                                            int? page,
                                            int? pageSize);
        Task<TransactionDto> categorizeTransaction(int id, CategorizeRequest request);


        Task<TransactionDto> SplitTransactions(int id, List<string> codes);
        


    }
}
