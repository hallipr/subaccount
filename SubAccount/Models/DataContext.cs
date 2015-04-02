namespace SubAccount.Models
{
    using System.Data.Entity;
    using SubAccount.Common;

    public class DataContext : DbContext, IDataContext
    {
        public IDbSet<Transaction> Transactions { get; set; }
        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Goal> Goals { get; set; }
        
        public DataContext(Config config) : base(config.ConnectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TransactionConfiguration());
            modelBuilder.Configurations.Add(new GoalConfiguration());
            modelBuilder.Configurations.Add(new CategoryConfiguration());
        }
    }
}