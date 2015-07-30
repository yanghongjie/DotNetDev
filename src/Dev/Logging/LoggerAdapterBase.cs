using System;
using System.Collections.Concurrent;

//Copyright
//Copyright (c) 2014 OSharp. All rights reserved.
//ProjectAddress:https://github.com/i66soft/osharp
using Dev.Extensions;
using Dev.Properties;

namespace Dev.Logging
{
    public abstract class LoggerAdapterBase : ILoggerAdapter
    {
        private readonly ConcurrentDictionary<string, ILog> _cacheLoggers;

        /// <summary>
        ///     初始化一个<see cref="LoggerAdapterBase" />类型的新实例
        /// </summary>
        protected LoggerAdapterBase()
        {
            _cacheLoggers = new ConcurrentDictionary<string, ILog>();
        }

        #region Implementation of ILoggerAdapter

        /// <summary>
        ///     由指定类型获取<see cref="ILog" />日志实例
        /// </summary>
        /// <param name="type">指定类型</param>
        /// <returns></returns>
        public ILog GetLogger(Type type)
        {
            type.CheckNotNull("type");
            return GetLoggerInternal(type.FullName);
        }

        /// <summary>
        ///     由指定名称获取<see cref="ILog" />日志实例
        /// </summary>
        /// <param name="name">指定名称</param>
        /// <returns></returns>
        public ILog GetLogger(string name)
        {
            name.CheckNotNullOrEmpty("name");
            return GetLoggerInternal(name);
        }

        #endregion

        /// <summary>
        ///     创建指定名称的缓存实例
        /// </summary>
        /// <param name="name">指定名称</param>
        /// <returns></returns>
        protected abstract ILog CreateLogger(string name);

        /// <summary>
        ///     清除缓存中的日志实例
        /// </summary>
        protected virtual void Clear()
        {
            _cacheLoggers.Clear();
        }

        private ILog GetLoggerInternal(string name)
        {
            ILog log;
            if (_cacheLoggers.TryGetValue(name, out log))
            {
                return log;
            }
            log = CreateLogger(name);
            if (log == null)
            {
                throw new NotSupportedException(Resources.Logging_CreateLogInstanceReturnNull.FormatWith(name,
                    GetType().FullName));
            }
            _cacheLoggers[name] = log;
            return log;
        }
    }
}