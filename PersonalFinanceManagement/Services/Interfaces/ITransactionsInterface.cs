using PersonalFinanceManagement.Dto;
using PersonalFinanceManagement.Models;
using PersonalFinanceManagement.Models.Helpers;

namespace PersonalFinanceManagement.Services.Interfaces
{
    public interface ITransactionsInterface
    {
        List<TransactionDto> GetList(string transactionKind,
                                        DateTime? startDate,
                                        DateTime? endDate,
                                        int? page,
                                        int? pageSize);
        Task<TransactionDto> GetTransactionById(int id);
        Task<List<TransactionsModel>> ImportAsync(IFormFile csv);
        Task<TransactionDto> CategorizeTransaction(int id, CategorizeRequest request);
        Task<GroupCategories> SpendingAnalytics(string Category, DateTime? startDate, DateTime? endDate, string? direction);
        Task<TransactionDto> SplitTransaction(int id, List<string> codes);
        Task<CategoriesModel> GetCategoryByCode(string code);
    }
}
