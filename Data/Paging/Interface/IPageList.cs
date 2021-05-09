using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Paging.Interface
{
    public interface IPagedList<T> : IList<T>
    {
        /// <summary>
        /// Page index
        /// </summary>
        int IndexOfPage { get; }

        /// <summary>
        /// Page size
        /// </summary>
        int SizeOfPage { get; }

        /// <summary>
        /// Total count
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        /// Total pages
        /// </summary>
        int TotalPages { get; }

        /// <summary>
        /// Has previous page
        /// </summary>
        bool HasPrevious { get; }

        /// <summary>
        /// Has next age
        /// </summary>
        bool HasNext { get; }
    }
}
