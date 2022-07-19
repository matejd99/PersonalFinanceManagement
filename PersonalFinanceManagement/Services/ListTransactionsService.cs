using PersonalFinanceManagement.Data;
using PersonalFinanceManagement.Dto;
using PersonalFinanceManagement.Mappers;
using System.Linq;

namespace PersonalFinanceManagement.Services
{
    public class ListTransactionsService
    {

        public List<TransactionDto> FilterByBeneficiaryName(string BName)
        {

            TransactionsContext transactions = new TransactionsContext();

            return transactions
                .Transactions
                .Where(t => t.BeneficiaryName == BName)
                .ToList()
                .Select(t => TransactionsFactory.ToDto(t))
                .ToList();
        }

    }
}
