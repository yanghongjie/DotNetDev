using System;
using System.Collections.Generic;
using System.Linq;

namespace Dev.Paginated
{
    public static class PaginatedListExtensions
    {
        public static IPaginated<T> ToPaginatedDto<T, TEntity>(this IPaginated<TEntity> source, Func<TEntity, T> selector)
        {
            return new Paginated<T>(source.PageIndex, source.PageSize, source.TotalCount, source.Items.Select(selector));
        }

        public static IPaginated<T> ToPaginatedDto<T, TEntity>(this IPaginated<TEntity> source, IEnumerable<T> items)
        {
            return new Paginated<T>(source.PageIndex, source.PageSize, source.TotalCount, items);
        }
    }

    public class Paginated<T> : List<T>, IPaginated<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Paginated{T}"/> class.
        /// Only for document.
        /// </summary>
        public Paginated()
        {
            this.Items = new List<T>();
        }

        public Paginated(int pageIndex, int pageSize, int totalCount, IEnumerable<T> source)
        {
            this.AddRange(source);

            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalCount = totalCount;
            this.TotalPageCount = (int)Math.Ceiling(totalCount / (double)pageSize);
        }

        #region IPaginatedDto<T> Members

        /// <summary>
        ///     是否有下一页
        /// </summary>
        public bool HasNextPage
        {
            get { return this.PageIndex < this.TotalPageCount; }
        }

        /// <summary>
        ///     数据
        /// </summary>
        public IEnumerable<T> Items
        {
            get { return this; }
            set
            {
                this.Clear();
                this.AddRange(value);
            }
        }

        /// <summary>
        ///     当前页码
        /// </summary>
        public int PageIndex { get; private set; }

        /// <summary>
        ///     页记录数
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        ///     总数据数量
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        ///     总页数
        /// </summary>
        public int TotalPageCount { get; private set; }

        #endregion IPaginatedDto<T> Members

        public static Paginated<T> LoadData<TEntity>(Paginated<TEntity> source, Func<TEntity, T> selector)
        {
            return new Paginated<T>
            {
                PageIndex = source.PageIndex,
                PageSize = source.PageSize,
                TotalCount = source.TotalCount,
                TotalPageCount = source.TotalPageCount,
                Items = source.Select(selector)
            };
        }
    }
}