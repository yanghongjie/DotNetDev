using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Dev.Data.Context;

namespace Dev.Data.Initializer
{
    /// <summary>
    /// 模型改变时重新创建数据库
    /// </summary>
    public class DropCreateDatabaseIfModelChangesWithSeed<TContext> : DropCreateDatabaseIfModelChanges<TContext> where TContext : DbContextBase
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