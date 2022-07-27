using PersonalFinanceManagement.Dto;

namespace PersonalFinanceManagement.Services.Interfaces
{
    public interface ITransactionsService
    {
        List<TransactionDto> GetList(string transactionKind,
                                        DateTime? startDate,
                                        DateTime? endDate,
                                        int? page,
                                        int? pageSize);
        Task<TransactionDto> GetTransactionById(int id);
    }
}
