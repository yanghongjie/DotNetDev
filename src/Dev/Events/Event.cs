using System;
using Dev.Domain;
using Dev.Common.Data;
using Dev.Common.Develop;

namespace Dev.Events
{
    /// <summary>
    /// 事件消息
    /// </summary>
    [Serializable]
    public class Event : IEvent
    {
        #region Ctor

        /// <summary>
        /// 初始化 <see cref="Event"/> 类.
        /// </summary>
        protected Event()
        {
            Id = GuidHelper.New();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        protected Event(IEntity source)
        {
            Id = GuidHelper.New();
            Source = source;
            Version = 0;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="version">The version.</param>
        protected Event(IEntity source, long version)
        {
            Id = GuidHelper.New();
            Source = source;
            Version = version;
        }

        #endregion

        #region IEvent Members

        public IEntity Source { get; set; }
        public DateTime Timestamp { get; set; }
        public long Version { get; set; }

        #endregion

        #region IEntity Members

        /// <summary>
        /// 命令标识
        /// </summary>
        public Guid Id { get; set; }

        #endregion

        #region Public Methods
        /// <summary>
        /// 当前领域事件的HashCode
        /// </summary>
        /// <returns>HashCode.</returns>
        public override int GetHashCode()
        {
            return CodeUtils.GetHashCode(Source.GetHashCode(), Id.GetHashCode(), Timestamp.GetHashCode(), Version.GetHashCode());
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">要与当前对象进行比较的对象。</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj == null)
                return false;
            var other = obj as Event;
            if (other == null)
                return false;
            return Id == other.Id;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return Id.ToString();
        }

        #endregion
    }
}