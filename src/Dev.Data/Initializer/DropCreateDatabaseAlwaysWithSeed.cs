using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Dev.Data.Context;

namespace Dev.Data.Initializer
{
    /// <summary>
    /// 总是重新创建数据库
    /// </summary>
    public class DropCreateDatabaseAlwaysWithSeed<TContext> : DropCreateDatabaseAlways<TContext> where TContext : DbContextBase
    {
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