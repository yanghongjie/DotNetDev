using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dev.Common.Develop;

//Copyright
//Copyright (c) 2014 OSharp. All rights reserved.
//ProjectAddress:https://github.com/i66soft/osharp

namespace Dev.Common.Model
{
    /// <summary>
    /// 可持久化到数据库的数据模型基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public abstract class EntityBase<TKey>
    {
        protected EntityBase()
        {
            IsDeleted = false;
            CreatedTime = DateTime.Now;
        }

        #region 属性

        /// <summary>
        /// 获取或设置 实体唯一标识，主键
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; }

        /// <summary>
        /// 获取或设置 是否删除，逻辑上的删除，非物品删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 获取或设置 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置 版本控制标识，用于处理并发
        /// </summary>
        [ConcurrencyCheck]
        [Timestamp]
        public byte[] Timestamp { get; set; }

        #endregion

        #region 方法

        /// <summary>
        /// 判断两个实体是否是同一数据记录的实体
        /// </summary>
        /// <param name="obj">要比较的实体信息</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj == null)
                return false;
            var other = obj as EntityBase<TKey>;
            if (other == null)
                return false;
            return Id.Equals(other.Id);
        }

        /// <summary>
        /// 用作特定类型的哈希函数。
        /// </summary>
        /// <returns>
        /// 当前 <see cref="T:System.Object"/> 的哈希代码。
        /// </returns>
        public override int GetHashCode()
        {
            return CodeUtils.GetHashCode(CreatedTime.GetHashCode(), Id.GetHashCode());
        }

        #endregion
    }
}