using System.Data.Entity;
using System.Diagnostics;
using Dev.Data.Context;
using Dev.Data.TransientErrorDetectionStrategy;
using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using Test.Common.Database.Mapping;
using Test.Common.Entites;

namespace Test.Common.Database
{
    public class DevTestContext : DbContextBase
    {
        public DevTestContext()
            : base("TestContext")
        {
            retryPolicy = new RetryPolicy<SqlDatabaseTransientErrorDetectionStrategy>(RetryStrategyFactory.GetSqlDbContextRetryPolicy());
            retryPolicy.Retrying += (s, e) => Trace.TraceError("An error occurred in attempt number {1} to access the OrderContext: {0}", e.LastException.Message, e.CurrentRetryCount);
        }

        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new OrderMap());
        }
    }
}