namespace SubAccount.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using SubAccount.Common;

    public class DataStore : IDataStore
    {
        private readonly IDataContext dataContext;

        public DataStore(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public Goal GetGoal(Guid id)
        {
            return this.dataContext.Goals.Find(id);
        }

        public void StoreGoal(Goal goal)
        {
            this.dataContext.Goals.AddOrUpdate(goal);
            this.dataContext.SaveChanges();
        }

        public Transaction GetTransaction(Guid id)
        {
            return this.dataContext.Transactions.Find(id);
        }

        public void StoreTransactions(IEnumerable<Transaction> transactions)
        {
            this.dataContext.Transactions.AddOrUpdate(transactions.ToArray());
            this.dataContext.SaveChanges();
        }

        public Category GetCategory(Guid id)
        {
            return this.dataContext.Categories.Find(id);
        }

        public void StoreCategory(Category category)
        {
            this.dataContext.Categories.AddOrUpdate(category);
            this.dataContext.SaveChanges();
        }

        public IEnumerable<Category> GetCategories()
        {
            return this.dataContext.Categories;
        }

        public IEnumerable<Goal> GetGoals()
        {
            return this.dataContext.Goals;
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            return this.dataContext.Transactions;
        }
    }
}