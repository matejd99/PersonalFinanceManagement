using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinanceManagement.Models
{
    public class TransactionsSplit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public TransactionsModel Transaction { get; set; }
        public CategoriesModel Category { get; set; }
        public double Amount { get; set; }
    }
}
