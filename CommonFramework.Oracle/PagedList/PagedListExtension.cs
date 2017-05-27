using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CommonFramework.Oracle.PagedList
{
    public static class PagedListExtension
    { 
        /// <summary>
        /// 共通分页类
        /// </summary>
        /// <typeparam name="T">查询返回类</typeparam>
        /// <param name="allItems">要查询的数据</param>
        /// <param name="pageIndex">要查询的页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static CommonPagedList<T> ToCommonPagedList<T>(this IOrderedQueryable<T> allItems, int pageIndex, int pageSize) where T : class
        {
            CommonPagedList<T> Result = new CommonPagedList<T>(allItems, pageIndex, pageSize);
            return Result;
        }

        /// <summary>
        /// 单个字段排序分页查询
        /// </summary>
        /// <typeparam name="T">查询返回类</typeparam>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="allItems">要查询的数据</param>
        /// <param name="orderator">排序字段表达式</param>
        /// <param name="asc">是否升序</param>
        /// <param name="pageIndex">要查询的页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static CommonPagedList<T> ToCommonPagedList<T, TKey>(this IQueryable<T> allItems, Expression<Func<T, TKey>> orderator, bool asc, int pageIndex, int pageSize) where T : class
        {
            var source = asc ? allItems.OrderBy(orderator) : allItems.OrderByDescending(orderator);
            CommonPagedList<T> Result = new CommonPagedList<T>(source, pageIndex, pageSize);
            return Result;
        }

        /// <summary>
        /// 多个字段排序分页查询
        /// </summary>
        /// <typeparam name="T">查询返回类</typeparam>
        /// <typeparam name="TKey1">排序字段类型1</typeparam>
        /// <typeparam name="TKey2">排序字段类型2</typeparam>
        /// <param name="allItems">要查询的数据</param>
        /// <param name="orderator1">排序字段表达式1</param>
        /// <param name="orderator2">排序字段表达式2</param>
        /// <param name="asc1">是否升序1</param>
        /// <param name="asc2">是否升序2</param>
        /// <param name="pageIndex">要查询的页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static CommonPagedList<T> ToCommonPagedList<T, TKey1,TKey2>(this IQueryable<T> allItems, Expression<Func<T, TKey1>> orderator1, Expression<Func<T, TKey2>> orderator2, bool asc1, bool asc2, int pageIndex, int pageSize) where T : class
        {
            var source = asc1 ? (asc2? allItems.OrderBy(orderator1).ThenBy(orderator2):allItems.OrderBy(orderator1).ThenByDescending(orderator2))
                : (asc2 ? allItems.OrderByDescending(orderator1).ThenBy(orderator2) : allItems.OrderByDescending(orderator1).ThenByDescending(orderator2));
            CommonPagedList<T> Result = new CommonPagedList<T>(source, pageIndex, pageSize);
            return Result;
        }

        /// <summary>
        /// 共通分页类
        /// </summary>
        /// <typeparam name="T">查询返回类</typeparam>
        /// <param name="allItems">要查询的数据</param>
        /// <param name="pageIndex">要查询的页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static CommonPagedList<T> ToCommonPagedList<T>(this IOrderedEnumerable<T> allItems, int pageIndex, int pageSize) where T : class
        {
            CommonPagedList<T> Result = new CommonPagedList<T>(allItems.AsQueryable(), pageIndex, pageSize);
            return Result;
        }
    }
}
