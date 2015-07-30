using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Dev.Data.TransientErrorDetectionStrategy;
using Dev.Extensions;
using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

namespace Dev.Data.Context
{
    /// <summary>
    /// EntityFramework 上下文基类
    /// </summary>
    public abstract class DbContextBase : DbContext
    {
        /// <summary>
        /// 重试策略
        /// </summary>
        protected RetryPolicy retryPolicy;
        /// <summary>
        /// 初始化<see cref="DbContextBase"/>类的实例
        /// </summary>
        /// <param name="nameOrConnectionString">数据库连接名</param>
        public DbContextBase(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            retryPolicy = new RetryPolicy(new TransientErrorIgnoreStrategy(), RetryStrategy.NoRetry);
        }
        /// <summary>
        /// 添加一个实体到上下文
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体实例</param>
        public void Add<T>(T entity) where T : class
        {
            entity.CheckNotNull("entity");
            if (Entry(entity).State == EntityState.Detached) Set<T>().Add(entity);
        }
        /// <summary>
        /// 添加多个实体到上下文
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">实体实例集合</param>
        public void AddRange<T>(IEnumerable<T> entities) where T : class
        {
            entities.CheckNotNullOrEmpty("entities");
            foreach (T entity in from entity in entities let entry = Entry(entity) where entry.State == EntityState.Detached select entity)
            {
                Set<T>().Add(entity);
            }
        }
        /// <summary>
        /// 移除实体（WithRetry）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体实例</param>
        /// <returns>影响的行数</returns>
        public int Remove<T>(T entity) where T : class
        {
            entity.CheckNotNull("entity");
            Entry(entity).State = EntityState.Deleted;
            return retryPolicy.ExecuteAction(() => SaveChanges());
        }
        /// <summary>
        /// 异步移除实体（WithRetry）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体实例</param>
        /// <returns>影响的行数</returns>
        public Task<int> RemoveAsync<T>(T entity) where T : class
        {
            entity.CheckNotNull("entity");
            Entry(entity).State = EntityState.Deleted;
            return retryPolicy.ExecuteAction(() => SaveChangesAsync());
        }
        /// <summary>
        /// 保存更改（WithRetry）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体实例</param>
        /// <returns>影响的行数</returns>
        public int Save<T>(T entity) where T : class
        {
            Add(entity);
            return retryPolicy.ExecuteAction(() => SaveChanges());
        }
        /// <summary>
        /// 异步保存更改（WithRetry）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体实例</param>
        /// <returns>影响的行数</returns>
        public Task<int> SaveAsync<T>(T entity) where T : class
        {
            Add(entity);
            return retryPolicy.ExecuteAction(() => SaveChangesAsync());
        }
        /// <summary>
        /// 保存或更改（WithRetry）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体实例</param>
        /// <param name="identifierExpression">查询表达式</param>
        /// <returns>影响的行数</returns>
        public int SaveOrUpdate<T>(T entity, Expression<Func<T, bool>> identifierExpression) where T : class
        {
            Add(entity);
            if (Set<T>().Any(identifierExpression))
            {
                Entry(entity).State = EntityState.Modified;
            }
            return retryPolicy.ExecuteAction(() => SaveChanges());
        }
        /// <summary>
        /// 异步保存或更改（WithRetry）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体实例</param>
        /// <param name="identifierExpression">查询表达式</param>
        /// <returns>影响的行数</returns>
        public Task<int> SaveOrUpdateAsync<T>(T entity, Expression<Func<T, bool>> identifierExpression) where T : class
        {
            Add(entity);
            if (Set<T>().Any(identifierExpression))
            {
                Entry(entity).State = EntityState.Modified;
            }
            return retryPolicy.ExecuteAction(() => SaveChangesAsync());
        }
        public IQueryable<T> Query<T>() where T : class
        {
            return Set<T>();
        }
        public IQueryable<T> ReadonlyQuery<T>() where T : class
        {
            return Set<T>().AsNoTracking();
        }
        public T TryFind<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return Set<T>().AsNoTracking().FirstOrDefault(predicate);
        }
        public Task<T> TryFindAsync<T>(Expression<Func<T, bool>> predicate) where T :class 
        {
            return Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }
        public void ExecuteAction(Action action)
        {
            action.CheckNotNull("action");
            this.ExecuteAction(() =>
            {
                action();
                return (object)null;
            });
        }
        public TResult ExecuteAction<TResult>(Func<TResult> func)
        {
            return this.retryPolicy.ExecuteAction(func);
        }
        public Task ExecuteAsync(Func<Task> taskAction)
        {
            return this.retryPolicy.ExecuteAsync(taskAction);
        }
        public Task ExecuteAsync(Func<Task> taskAction, CancellationToken cancellationToken)
        {
            return this.retryPolicy.ExecuteAsync(taskAction, cancellationToken);
        }
        public Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> taskFunc, CancellationToken cancellationToken = new CancellationToken())
        {
            return this.retryPolicy.ExecuteAsync(taskFunc, cancellationToken);
        }
        public Task<int> ExecuteSaveChangesAsync(CancellationToken cancellationToken)
        {
            return this.ExecuteAsync(() => this.SaveChangesAsync(cancellationToken), cancellationToken);
        }
        public Task<int> ExecuteSaveChangesAsync()
        {
            return this.ExecuteAsync(this.SaveChangesAsync);
        }
    }
}