using PersonalFinanceManagement.Models;

namespace PersonalFinanceManagement.Dto
{
    public class SplitsDto
    {
        public TransactionsModel Transaction { get; set; }
        public CategoriesModel Category { get; set; }
        public double Amount { get; set; }
    }
}
