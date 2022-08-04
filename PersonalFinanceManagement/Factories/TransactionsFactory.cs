using PersonalFinanceManagement.Dto;
using PersonalFinanceManagement.Factories;
using PersonalFinanceManagement.Models;

namespace PersonalFinanceManagement.Mappers
{
    public static class TransactionsFactory
    {

        public static SplitsDto SplitToDto(TransactionsSplit split)
        {
            return new SplitsDto()
            {
                Amount = split.Amount,
                Category = split.Category
            };

        }

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
                Kind = transaction.Kind,
                categoryDto = transaction.categoriesModel == null ? null : CategoryFactory.ToDto(transaction.categoriesModel),
                SplitsDto = transaction.Splits?.Select(split => SplitToDto(split))?.ToList() ?? new List<SplitsDto>()
            };
        }
    }
}
