using CommonFramework.Core.Dependency;
using CommonFramework.Core.EntityFramework.PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CommonFramework.Core.EntityFramework
{
    /// <summary>
    /// 基础数据接口
    /// by Rex Sheng @2017
    /// </summary>
    /// <typeparam name="TDbContext">上下文</typeparam>
    /// <typeparam name="TEntity">实体</typeparam>
    public interface IBaseRepository<TDbContext, TEntity> : ITransientDependency
         where TDbContext : DbContext, new()
        where TEntity : class
    {
        /// <summary>
        /// 获取数据库实体
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        TDbContext GetContext(object conn=null);

        /// 新增实体数据
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="entity">要新增的实体</param>
        /// <returns>新增后的实体</returns>
        TEntity Add(  TEntity entity,object conn= null);

        /// 新增多个实体数据
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="entity">要新增的实体</param>
        /// <returns>新增后的实体</returns>
        IEnumerable<TEntity> Add( IEnumerable<TEntity> entities, object conn = null);

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="entity">更新的实体</param>
        /// <returns>是否更新成功</returns>
        bool Update(TEntity entity, object conn = null);

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="entity">删除的实体</param>
        /// <returns>是否删除成功</returns>
        bool Delete(TEntity entity, object conn = null);

        /// <summary>
        /// 查询所有数据（不立即执行查询）
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        IQueryable<TEntity> GetAll(object conn=null);

        /// <summary>
        /// 懒加载
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="propertySelectors"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetAllIncluding(object conn, params Expression<Func<TEntity, object>>[] propertySelectors);


        /// <summary>
        /// 查询所有数据（立即执行查询）
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        List<TEntity> GetAllList(object conn=null);

        /// <summary>
        /// 查询数据（立即执行查询）
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate, object conn = null);

        /// <summary>
        /// 异步查询数据
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        Task<List<TEntity>> GetAllListAsync(object conn=null);

        /// <summary>
        /// 异步查询数据
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate, object conn = null);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TOrderKey">排序字段</typeparam>
        /// <param name="conn">连接字符串</param>
        /// <param name="orderPredicate">排序表达式</param>
        /// <param name="asc">是否升序</param>
        /// <param name="pageIndex">要查询的页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        CommonPagedList<TEntity> ToCommonPagedList<TOrderKey>( Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc, int pageIndex, int pageSize, object conn = null);

        /// <summary>
        /// 异步分页查询
        /// </summary>
        /// <typeparam name="TOrderKey">排序字段</typeparam>
        /// <param name="conn">连接字符串</param>
        /// <param name="orderPredicate">排序表达式</param>
        /// <param name="asc">是否升序</param>
        /// <param name="pageIndex">要查询的页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        Task<CommonPagedList<TEntity>> ToCommonPagedListAsync<TOrderKey>(Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc, int pageIndex, int pageSize, object conn = null);

        /// <summary>
        /// 求数量
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        int Count(object conn=null);

        /// <summary>
        /// 求数量
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate, object conn = null);

        /// <summary>
        /// 求数量
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        Task<int> CountAsync(object conn=null);

        /// <summary>
        /// 求数量
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        Task<int> CountAsync( Expression<Func<TEntity, bool>> predicate, object conn = null);

        /// <summary>
        /// 求数量
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        long LongCount(object conn=null);

        /// <summary>
        /// 求数量
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        long LongCount(Expression<Func<TEntity, bool>> predicate, object conn = null);

        /// <summary>
        /// 求数量
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        Task<long> LongCountAsync(object conn=null);

        /// <summary>
        /// 求数量
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        Task<long> LongCountAsync( Expression<Func<TEntity, bool>> predicate, object conn = null);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        bool Exist( Expression<Func<TEntity, bool>> predicate, object conn = null);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        Task<bool> ExistAsync(  Expression<Func<TEntity, bool>> predicate, object conn = null);

        /// <summary>
        /// 第一条
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        TEntity FirstOrDefault(object conn=null);

        /// <summary>
        /// 第一条
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        TEntity FirstOrDefault( Expression<Func<TEntity, bool>> predicate, object conn = null);

        /// <summary>
        /// 第一条
        /// </summary>
        /// <typeparam name="TOrderKey">排序字段</typeparam>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderPredicate">排序表达式</param>
        /// <param name="asc">是否升序</param>
        /// <returns></returns>
        TEntity FirstOrDefault<TOrderKey>( Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc, object conn = null);

        /// <summary>
        /// 第一条
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(object conn=null);

        /// <summary>
        /// 第一条
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync( Expression<Func<TEntity, bool>> predicate, object conn = null);

        /// <summary>
        /// 第一条
        /// </summary>
        /// <typeparam name="TOrderKey">排序字段</typeparam>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderPredicate">排序表达式</param>
        /// <param name="asc">是否升序</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync<TOrderKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc, object conn = null);

        /// <summary>
        /// 根据主键获取
        /// </summary>
        /// <typeparam name="TPrimaryKey">主键类型</typeparam>
        /// <param name="conn">连接字符串</param>
        /// <param name="key">主键值</param>
        /// <returns></returns>
        TEntity Get<TPrimaryKey>( TPrimaryKey key, object conn = null);

        /// <summary>
        /// 根据主键获取
        /// </summary>
        /// <typeparam name="TPrimaryKey">主键类型</typeparam>
        /// <param name="conn">连接字符串</param>
        /// <param name="key">主键值</param>
        /// <returns></returns>
        Task<TEntity> GetAsync<TPrimaryKey>( TPrimaryKey key, object conn = null);

        /// <summary>
        /// 执行sql,查询数据
        /// </summary>
        /// <typeparam name="T">返回的数据模型</typeparam>
        /// <param name="conn">连接字符串</param>
        /// <param name="sql">要执行的sql语句</param>
        /// <returns></returns>
        List<T> GetList<T>( string sql, object conn = null);

        /// <summary>
        /// 执行sql,查询数据
        /// </summary>
        /// <typeparam name="T">返回的数据模型</typeparam>
        /// <param name="conn">连接字符串</param>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        List<T> GetList<T>(object conn, string sql, params object[] parameters);

        /// <summary>
        /// 立即执行sql
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>受影响的行数</returns>
        int ExecuteSql(object conn, string sql, params object[] parameters);

        /// <summary>
        /// 立即执行sql
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="sql">要执行的sql语句</param>
        /// <returns>受影响的行数</returns>
        int ExecuteSql( string sql, object conn = null);

        /// <summary>
        /// 立即执行sql
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>受影响的行数</returns>
        Task<int> ExecuteSqlAsync(object conn, string sql, params object[] parameters);

        /// <summary>
        /// 立即执行sql
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="sql">要执行的sql语句</param>
        /// <returns>受影响的行数</returns>
        Task<int> ExecuteSqlAsync( string sql, object conn = null);

        /// <summary>
        /// 获取sql执行结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        T GetEntity<T>(object conn, string sql, params object[] parameters);

        /// <summary>
        /// 获取sql执行结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        T GetEntity<T>(string sql, object conn = null);

        /// <summary>
        /// 获取sql执行结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<T> GetEntityAsync<T>(object conn, string sql, params object[] parameters);

        /// <summary>
        /// 获取sql执行结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        Task<T> GetEntityAsync<T>(string sql, object conn = null);

        /// <summary>
        /// 先执行sql,然后添加数据实体
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="entity">要添加的数据实体</param>
        /// <param name="beforeAction">事务中对context执行前的其他操作</param>
        /// <param name="afterAction">事务中对context执行后的其他操作</param>
        /// <param name="conn">字符串</param>
        /// <returns></returns>
        bool AddByTrans( TEntity entity, Action<TDbContext, TEntity> beforeAction = null, Action<TDbContext, TEntity> afterAction = null, object conn = null);

        /// <summary>
        /// 先执行sql,然后更新数据实体
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="entity">要更新的数据实体</param>
        /// <param name="beforeAction">事务中对context执行前的其他操作</param>
        /// <param name="afterAction">事务中对context执行后的其他操作</param>
        /// <param name="conn">字符串</param>
        /// <returns></returns>
        bool UpdateByTrans( TEntity entity, Action<TDbContext, TEntity> beforeAction = null, Action<TDbContext, TEntity> afterAction = null, object conn = null);

        /// <summary>
        /// 先执行sql,然后删除数据实体
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="entity">要删除的数据实体</param>
        /// <param name="beforeAction">事务中对context执行前的其他操作</param>
        /// <param name="afterAction">事务中对context执行后的其他操作</param>
        /// <param name="conn">字符串</param>
        /// <returns></returns>
        bool DeleteByTrans( TEntity entity, Action<TDbContext, TEntity> beforeAction = null, Action<TDbContext, TEntity> afterAction = null, object conn = null);

        /// <summary>
        /// dbcontext事物操作
        /// </summary>
        /// <param name="action"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        bool DbTransaction(Action<TDbContext> action, object conn = null);
         
    }

   
}
