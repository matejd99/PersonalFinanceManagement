using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinanceManagement.Models
{
    public class TransactionsModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string BeneficiaryName { get; set; }
        public DateTime Date { get; set; }
        public string Direction { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        [ForeignKey("MccCodes")]
        public int? MCC { get; set; }
        public string Kind { get; set; }
        public CategoriesModel? categoriesModel { get; set; }
        public List<TransactionsSplit> Splits { get; set; }
    }
}