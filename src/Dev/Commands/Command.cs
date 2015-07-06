using System;
using Dev.Domain;
using Dev.Events;
using Dev.Common.Data;
using Dev.Common.Develop;

namespace Dev.Commands
{
    /// <summary>
    /// 命令消息
    /// </summary>
    [Serializable]
    public class Command : ICommand
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        protected Command()
        {
            Id = GuidHelper.New();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        protected Command(IEntity source)
        {
            Id = GuidHelper.New();
            Source = source;
        }

        #endregion

        #region IEntity Members

        /// <summary>
        /// 命令标识
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 命令源实体
        /// </summary>
        public IEntity Source { get; set; }


        #endregion

        #region Public Methods
        /// <summary>
        /// 当前领域事件的HashCode
        /// </summary>
        /// <returns>HashCode.</returns>
        public override int GetHashCode()
        {
            return CodeUtils.GetHashCode(Source.GetHashCode(), Id.GetHashCode());
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