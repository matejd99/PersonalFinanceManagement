using PersonalFinanceManagement.Dto;
using PersonalFinanceManagement.Models;

namespace PersonalFinanceManagement.Mappers
{
    public static class TransactionsFactory
    {
        public static TransactionDto ToDto(TransactionsModel transaction)
        {
            return new TransactionDto()
            {
               Id = transaction.Id,
               BeneficiaryName = transaction.BeneficiaryName,
               Date = transaction.Date,
               Direction = transaction.Direction,
               Amount = transaction.Amount,
               Description = transaction.Description,
               Currency = transaction.Currency,
               MCC = transaction.MCC,
               Kind = transaction.Kind
           };
        }
    }
}
