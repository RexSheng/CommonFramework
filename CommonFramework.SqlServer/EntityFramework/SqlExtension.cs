using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CommonFramework.SqlServer
{
    public class SqlExtension
    {
        /// <summary>
        /// 自动更新数据库到最新版本
        /// 需在 TConfiguration 类中开启 AutomaticMigrationsEnabled = true;
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <typeparam name="TConfiguration"></typeparam>
        /// <param name="config"></param>
        /// <param name="connName"></param>
        public static void UpdateToLatestedVersion<TDbContext,TConfiguration>(TConfiguration config, string connName = "")
            where TDbContext : DbContext
            where TConfiguration : DbMigrationsConfiguration<TDbContext>, new()
        {
            if (string.IsNullOrEmpty(connName))
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<TDbContext, TConfiguration>());
            }
            else
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<TDbContext, TConfiguration>(connName));
            }

        }

        /// <summary>
        /// 根据字段排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public static IQueryable<T> CreateOrderByQuery<T>(IQueryable<T> source, string propertyName, bool isAsc)
        {
            if (source == null) throw new ArgumentNullException("source", "不能为空");
            if (string.IsNullOrEmpty(propertyName)) return source;
            var _parameter = Expression.Parameter(source.ElementType);
            var _property = Expression.Property(_parameter, propertyName);
            if (_property == null) throw new ArgumentNullException("propertyName", "属性不存在");
            var _lambda = Expression.Lambda(_property, _parameter);
            var _methodName = isAsc ? "OrderBy" : "OrderByDescending";
            var _resultExpression = Expression.Call(typeof(Queryable), _methodName, new Type[] { source.ElementType, _property.Type }, source.Expression, Expression.Quote(_lambda));
            return source.Provider.CreateQuery<T>(_resultExpression);
        }
    }
}
