using Core.Paging.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Paging
{
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public PagedList()
        { }
        /// <summary>        
        /// Ctor       
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="skip">Skip rows from source</param>
        /// <param name="take">Take rows after skip</param>
        public PagedList(IList<T> source, int skip, int take)
        {
            this.TotalCount = source.Count;
            this.TotalPages = this.TotalCount / take;

            if (this.TotalCount % take > 0)
            {
                this.TotalPages++;
            }

            this.SizeOfPage = take;
            this.IndexOfPage = skip / take == 0 ? 1 : skip / take;
            this.AddRange(source.Skip(skip).Take(take).ToList());
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="skip">Skip rows from source</param>
        /// <param name="take">Take rows after skip</param>
        /// <param name="totalCount">Total count</param>
        public PagedList(IEnumerable<T> source, int skip, int take, int totalCount)
        {
            this.TotalCount = totalCount;
            this.TotalPages = this.TotalCount / take;

            if (this.TotalCount % take > 0)
            {
                this.TotalPages++;
            }

            this.SizeOfPage = take;
            this.IndexOfPage = skip / take == 0 ? 1 : skip / take;
            this.AddRange(source);
        }

        /// <summary>
        /// Page index
        /// </summary>
        public int IndexOfPage { get; }

        /// <summary>
        /// Page size
        /// </summary>
        public int SizeOfPage { get; }

        /// <summary>
        /// Total count
        /// </summary>
        public int TotalCount { get; }

        /// <summary>
        /// Total pages
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// Has previous page
        /// </summary>
        public bool HasPrevious => this.IndexOfPage > 0;

        /// <summary>
        /// Has next page
        /// </summary>
        public bool HasNext => this.IndexOfPage + 1 < this.TotalPages;
    }
}
