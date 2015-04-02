namespace SubAccount.Common
{
    using System;

    public class Transaction
    {
        public Guid Id { get; set; }

        public string CheckNumber { get; set; }

        public decimal Amount { get; set; }

        public string Name { get; set; }

        public string Memo { get; set; }

        public DateTime? DateAvailable { get; set; }

        public DateTime? DatePosted { get; set; }

        public Goal Goal { get; set; }
    }
}
