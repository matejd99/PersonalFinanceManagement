namespace PFM.Backend.Models
{
    public class TransactionsModel
    {
        public string Id { get; set; }
        public string BeneficiaryName { get; set; }
        public string Date { get; set; }
        public string Direction { get; set; }
        public string Amount { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public string MCC { get; set; }
        public string Kind { get; set; }
    }
}