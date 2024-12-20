﻿namespace eCommerceBase.Insfrastructure.Utilities.Grid.PagedList
{
    /// <summary>
    /// paged list inter face
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPagedList<T>
    {
        public List<T> Data { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<GridPropertyInfo> PropertyInfos { get; set; }
    }
}
