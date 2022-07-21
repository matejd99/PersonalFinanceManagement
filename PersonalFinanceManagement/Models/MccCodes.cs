using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceManagement.Models
{
    public class MccCodes
    {
        [Key]
        public int Code { get; set; }
        public string MerchanTtype { get; set; }
    }
}
