using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Dev.Data.Context;

namespace Dev.Data.Initializer
{
    /// <summary>
    /// 数据迁移更新数据库
    /// </summary>
    public class MigrationsConfigurationWithSeed<TContext> : DbMigrationsConfiguration<TContext> where TContext : DbContextBase
    {
        public MigrationsConfigurationWithSeed()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        protected override void Seed(TContext context)
        {
            IEnumerable<ISeedAction> seedActions = DatabaseInitializer.SeedActions.OrderBy(m => m.Order);
            foreach (var seedAction in seedActions)
            {
                seedAction.Action(context);
            }
        }
    }
}