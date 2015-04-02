namespace SubAccount.Common
{
    using System;
    using System.Collections.Generic;

    public class Goal
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public decimal Balance { get; set; }

        public Category Category { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}