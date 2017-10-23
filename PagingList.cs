#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace ToolBox {

    public interface IPagingList<T> : IEnumerable<T> {
        int PageNumber { get; }
        /// <summary>
        /// The number of items in each page.
        /// </summary>
        int PageSize { get; }
        /// <summary>
        /// The total number of items.
        /// </summary>
        int TotalItems { get; }
        /// <summary>
        /// The total number of pages.
        /// </summary>
        int TotalPages { get; }
        /// <summary>
        /// The index of the first item in the page.
        /// </summary>
        int FirstItem { get; }
        /// <summary>
        /// The index of the last item in the page.
        /// </summary>
        int LastItem { get; }
        /// <summary>
        /// Whether there are pages before the current page.
        /// </summary>
        bool HasPreviousPage { get; }
        /// <summary>
        /// Whether there are pages after the current page.
        /// </summary>
        bool HasNextPage { get; }
    }
    public class PagingList<T> : IPagingList<T> {
        protected IList<T> DataSource;

        /// <summary>
        /// Creates a new instance of CustomPagination
        /// </summary>
        /// <param name="dataSource">A pre-paged slice of data</param>
        /// <param name="pageNumber">The current page number</param>
        /// <param name="pageSize">The number of items per page</param>
        /// <param name="totalItems">The total number of items in the overall datasource</param>
        public PagingList(IEnumerable<T> dataSource, int pageNumber, int pageSize, int totalItems) {
            DataSource = dataSource.ToList();
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = totalItems;
        }

        public IEnumerator<T> GetEnumerator() {
            return DataSource.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public int PageNumber { get; protected set; }
        public int PageSize { get; protected set; }
        public int TotalItems { get; protected set; }

        public int TotalPages {
            get { return (int)Math.Ceiling(((double)TotalItems) / PageSize); }
        }

        public int FirstItem {
            get {
                return ((PageNumber - 1) * PageSize) + 1;
            }
        }

        public int LastItem {
            get { return FirstItem + DataSource.Count - 1; }
        }

        public bool HasPreviousPage {
            get { return PageNumber > 1; }
        }

        public bool HasNextPage {
            get { return PageNumber < TotalPages; }
        }
    }
}
