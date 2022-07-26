namespace PersonalFinanceManagement.Models
{
    public class TransactionsCategory
    {
        TransactionsModel Transaction { get; set; }

        List<CategoriesModel> Categories { get; set; }
    }
}
