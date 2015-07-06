using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Dev.Data.Context;

namespace Dev.Data.Initializer
{
    /// <summary>
    /// 在数据库不存在时使用种子数据创建数据库
    /// </summary>
    public class CreateDatabaseIfNotExistsWithSeed<TContext> : CreateDatabaseIfNotExists<TContext> where TContext : DbContextBase
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