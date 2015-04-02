namespace SubAccount.Models
{
    using System;
    using System.Collections.Generic;
    using SubAccount.Common;

    public interface IDataStore
    {
        IEnumerable<Category> GetCategories();
        Category GetCategory(Guid id);
        void StoreCategory(Category category);

        IEnumerable<Goal> GetGoals();
        Goal GetGoal(Guid id);
        void StoreGoal(Goal goal);

        IEnumerable<Transaction> GetTransactions();
        Transaction GetTransaction(Guid id);
        void StoreTransactions(IEnumerable<Transaction> transactions);
    }
}