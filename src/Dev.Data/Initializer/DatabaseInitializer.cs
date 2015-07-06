using System;
using System.Collections.Generic;
using System.Data.Entity;
using Dev.Data.Context;

namespace Dev.Data.Initializer
{
    /// <summary>
    /// 数据库初始化.
    /// </summary>
    public static class DatabaseInitializer
    {
        static DatabaseInitializer()
        {
            SeedActions = new List<ISeedAction>();
        }
        /// <summary>
        /// 获取 数据库创建时的种子数据操作信息集合，各个模块可以添加自己的初始化数据
        /// </summary>
        public static ICollection<ISeedAction> SeedActions { get; private set; }
        /// <summary>
        /// 设置数据库初始化
        /// </summary>
        /// <param name="initializer">初始化策略</param>
        public static void Initialize<TContext>(IDatabaseInitializer<TContext> initializer) where TContext : DbContextBase, new()
        {
            using (var context = new TContext())
            {
                Database.SetInitializer(initializer);
                context.Database.Initialize(true);
            }
        }
    }
}