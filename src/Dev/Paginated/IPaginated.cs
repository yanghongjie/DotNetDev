using System.Collections.Generic;

namespace Dev.Paginated
{
    public interface IPaginated<out T>
    {
        bool HasNextPage { get; }

        IEnumerable<T> Items { get; }

        int PageIndex { get; }

        int PageSize { get; }

        int TotalCount { get; }

        int TotalPageCount { get; }
    }
}