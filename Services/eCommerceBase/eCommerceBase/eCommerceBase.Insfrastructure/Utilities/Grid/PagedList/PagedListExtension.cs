﻿using Microsoft.EntityFrameworkCore;

namespace eCommerceBase.Insfrastructure.Utilities.Grid.PagedList
{
    public static class PagedListExtension
    {
        /// <summary>
        /// return to grid property veriable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source,
            int pageIndex = 1,
            int pageSize = int.MaxValue)
        {
            if (source == null)
                return new PagedList<T>();
            var result = new PagedList<T>
            {
                PageIndex = pageIndex,
                PageSize = Math.Max(pageSize, 1),
                TotalCount = source.Count()
            };
            result.TotalPages = result.TotalCount / result.PageSize;
            result.Data = result.PageSize < result.TotalCount
                ? await source.Skip((result.PageIndex - 1) * result.PageSize)
                    .Take(pageSize)
                    .ToListAsync()
                : await source.ToListAsync();
            if (result.TotalCount % pageSize > 0)
                result.TotalPages++;
            var sourceType = source.ElementType;
            result.PropertyInfos = sourceType.GetProperties().Select(x =>
            {
                var propertyType = Nullable.GetUnderlyingType(x.PropertyType) ?? x.PropertyType;
                return new GridPropertyInfo
                {
                    PropertyType = propertyType.Name,
                    PropertyName = char.ToLowerInvariant(x.Name[0]) + x.Name[1..]
                };
            });
            return result;
        }
        /// <summary>
        /// if we want to select the pagedlist value
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static PagedList<TResult> Select<TSource, TResult>(this PagedList<TSource> source,
            Func<TSource, TResult> selector)
        {
            var subset = source.Data.Select(selector);
            IEnumerable<GridPropertyInfo> propertyInfoList = new List<GridPropertyInfo>();
            if (source.Data.GetType() != selector.Method.ReturnType)
            {
                propertyInfoList = selector.Method.ReturnType.GetProperties()
                    .Select(x =>
                    {
                        var propertyType = Nullable.GetUnderlyingType(x.PropertyType) ?? x.PropertyType;
                        return new GridPropertyInfo
                        {
                            PropertyType = propertyType.Name,
                            PropertyName = char.ToLowerInvariant(x.Name[0]) + x.Name[1..]
                        };
                    });
            }
            else
            {
                propertyInfoList = source.PropertyInfos;
            }
            var result = new PagedList<TResult>(
                data: subset.ToList(),
                pageIndex: source.PageIndex,
                pageSize: source.PageSize,
                totalCount: source.TotalCount,
                totalPages: source.TotalPages,
                propertyInfos: propertyInfoList
                );
            return result;
        }
    }
}
