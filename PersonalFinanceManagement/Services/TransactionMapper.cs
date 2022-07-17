using CsvHelper.Configuration;
using PFM.Backend.Models;

namespace PFM.Backend.Services
{
    public class TransactionMapper : ClassMap<TransactionsModel>
    {
        public TransactionMapper()
        {
            Map(m => m.Id).Name("id");
            Map(m => m.BeneficiaryName).Name("beneficiary-name");
            Map(m => m.Date).Name("date");
            Map(m => m.Direction).Name("direction");
            Map(m => m.Amount).Name("amount");
            Map(m => m.Description).Name("description");
            Map(m => m.Currency).Name("currency");
            Map(m => m.MCC).Name("mcc");
            Map(m => m.Kind).Name("kind");
        }
    }
}