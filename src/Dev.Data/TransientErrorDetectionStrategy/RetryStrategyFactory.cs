using System;
using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

namespace Dev.Data.TransientErrorDetectionStrategy
{
    /// <summary>
    /// 重试策咯工厂类
    /// </summary>
    public static class RetryStrategyFactory
    {
        /// <summary>
        /// 获取Couchbase重试策咯
        /// </summary>
        /// <returns>重试策咯</returns>
        public static RetryStrategy GetCouchbaseContextRetryPolicy()
        {
            return new ExponentialBackoff(5, TimeSpan.FromMilliseconds(0), TimeSpan.FromMilliseconds(1), TimeSpan.FromMilliseconds(10)) { FastFirstRetry = true };
        }
        /// <summary>
        /// 获取Http重试策咯
        /// </summary>
        /// <returns>重试策咯</returns>
        public static RetryStrategy GetHttpRetryPolicy()
        {
            return new FixedInterval("HttpRetryPolicy", 5, TimeSpan.FromSeconds(3), true);
        }
        /// <summary>
        /// 获取MySql数据库重试策咯
        /// </summary>
        /// <returns>重试策咯</returns>
        public static RetryStrategy GetMySqlDbContextRetryPolicy()
        {
            return new ExponentialBackoff(5, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(0.5)) { FastFirstRetry = true };
        }
        /// <summary>
        /// 获取Redis重试策咯
        /// </summary>
        /// <returns>重试策咯</returns>
        public static RetryStrategy GetRedisDbContextRetryPolicy()
        {
            return new ExponentialBackoff(5, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(3)) { FastFirstRetry = false };
        }
        /// <summary>
        /// 获取SqlServer数据库重试策咯
        /// </summary>
        /// <returns>重试策咯</returns>
        public static RetryStrategy GetSqlDbContextRetryPolicy()
        {
            return new ExponentialBackoff(3, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(0.5)) { FastFirstRetry = true };
        }
    }
}