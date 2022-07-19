using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinanceManagement.Models
{
    public class CategoriesModel
    {
        [Key]
        public string Code { get; set; }
        public string ParentCode { get; set; }
        [ForeignKey("TransactionsModel")]
        public string Name { get; set; }
    }
}
