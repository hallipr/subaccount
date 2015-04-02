namespace SubAccount.Common
{
    public class TransactionAssignment
    {
        public Transaction Transaction { get; set; }
        public Bucket Bucket { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}