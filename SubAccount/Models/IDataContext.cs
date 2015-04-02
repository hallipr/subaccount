namespace SubAccount.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using SubAccount.Common;

    public interface IDataContext : IDisposable
    {
        IDbSet<Transaction> Transactions { get; set; }
        IDbSet<Goal> Goals { get; set; }
        IDbSet<Category> Categories { get; set; }

        int SaveChanges();
    }
}