using PersonalFinanceManagement.Models;

namespace PersonalFinanceManagement.Dto
{
    public class SplitsDto
    {
        public CategoriesModel Category { get; set; }
        public double Amount { get; set; }
    }
}
