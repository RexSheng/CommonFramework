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
    /// <typeparam name="TConnectionString">连接字符串</typeparam>
    /// <typeparam name="TDbContext">上下文</typeparam>
    /// <typeparam name="TEntity">实体</typeparam>
    public interface IBaseRepository<TConnectionString, TDbContext, TEntity> : ITransientDependency
         where TDbContext : DbContext, new()
        where TEntity : class
    {
        /// <summary>
        /// 获取数据库实体
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        TDbContext GetContext(TConnectionString conn);

        /// 新增实体数据
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="entity">要新增的实体</param>
        /// <returns>新增后的实体</returns>
        TEntity Add(TConnectionString conn, TEntity entity);

        /// 新增多个实体数据
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="entity">要新增的实体</param>
        /// <returns>新增后的实体</returns>
        IEnumerable<TEntity> Add(TConnectionString conn, IEnumerable<TEntity> entities);

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="entity">更新的实体</param>
        /// <returns>是否更新成功</returns>
        bool Update(TConnectionString conn, TEntity entity);

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="entity">删除的实体</param>
        /// <returns>是否删除成功</returns>
        bool Delete(TConnectionString conn, TEntity entity);

        /// <summary>
        /// 查询所有数据（不立即执行查询）
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        IQueryable<TEntity> GetAll(TConnectionString conn);

        /// <summary>
        /// 懒加载
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="propertySelectors"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetAllIncluding(TConnectionString conn, params Expression<Func<TEntity, object>>[] propertySelectors);


        /// <summary>
        /// 查询所有数据（立即执行查询）
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        List<TEntity> GetAllList(TConnectionString conn);

        /// <summary>
        /// 查询数据（立即执行查询）
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        List<TEntity> GetAllList(TConnectionString conn, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 异步查询数据
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        Task<List<TEntity>> GetAllListAsync(TConnectionString conn);

        /// <summary>
        /// 异步查询数据
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        Task<List<TEntity>> GetAllListAsync(TConnectionString conn, Expression<Func<TEntity, bool>> predicate);

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
        CommonPagedList<TEntity> ToCommonPagedList<TOrderKey>(TConnectionString conn, Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc, int pageIndex, int pageSize);

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
        Task<CommonPagedList<TEntity>> ToCommonPagedListAsync<TOrderKey>(TConnectionString conn, Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc, int pageIndex, int pageSize);

        /// <summary>
        /// 求数量
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        int Count(TConnectionString conn);

        /// <summary>
        /// 求数量
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        int Count(TConnectionString conn, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 求数量
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        Task<int> CountAsync(TConnectionString conn);

        /// <summary>
        /// 求数量
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        Task<int> CountAsync(TConnectionString conn, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 求数量
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        long LongCount(TConnectionString conn);

        /// <summary>
        /// 求数量
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        long LongCount(TConnectionString conn, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 求数量
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        Task<long> LongCountAsync(TConnectionString conn);

        /// <summary>
        /// 求数量
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        Task<long> LongCountAsync(TConnectionString conn, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        bool Exist(TConnectionString conn, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        Task<bool> ExistAsync(TConnectionString conn, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 第一条
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        TEntity FirstOrDefault(TConnectionString conn);

        /// <summary>
        /// 第一条
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        TEntity FirstOrDefault(TConnectionString conn, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 第一条
        /// </summary>
        /// <typeparam name="TOrderKey">排序字段</typeparam>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderPredicate">排序表达式</param>
        /// <param name="asc">是否升序</param>
        /// <returns></returns>
        TEntity FirstOrDefault<TOrderKey>(TConnectionString conn, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc);

        /// <summary>
        /// 第一条
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(TConnectionString conn);

        /// <summary>
        /// 第一条
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(TConnectionString conn, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 第一条
        /// </summary>
        /// <typeparam name="TOrderKey">排序字段</typeparam>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderPredicate">排序表达式</param>
        /// <param name="asc">是否升序</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync<TOrderKey>(TConnectionString conn, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc);

        /// <summary>
        /// 根据主键获取
        /// </summary>
        /// <typeparam name="TPrimaryKey">主键类型</typeparam>
        /// <param name="conn">连接字符串</param>
        /// <param name="key">主键值</param>
        /// <returns></returns>
        TEntity Get<TPrimaryKey>(TConnectionString conn, TPrimaryKey key);

        /// <summary>
        /// 根据主键获取
        /// </summary>
        /// <typeparam name="TPrimaryKey">主键类型</typeparam>
        /// <param name="conn">连接字符串</param>
        /// <param name="key">主键值</param>
        /// <returns></returns>
        Task<TEntity> GetAsync<TPrimaryKey>(TConnectionString conn, TPrimaryKey key);

        /// <summary>
        /// 执行sql,查询数据
        /// </summary>
        /// <typeparam name="T">返回的数据模型</typeparam>
        /// <param name="conn">连接字符串</param>
        /// <param name="sql">要执行的sql语句</param>
        /// <returns></returns>
        List<T> GetList<T>(TConnectionString conn, string sql);

        /// <summary>
        /// 执行sql,查询数据
        /// </summary>
        /// <typeparam name="T">返回的数据模型</typeparam>
        /// <param name="conn">连接字符串</param>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        List<T> GetList<T>(TConnectionString conn, string sql, params object[] parameters);

        /// <summary>
        /// 立即执行sql
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>受影响的行数</returns>
        int ExecuteSql(TConnectionString conn, string sql, params object[] parameters);

        /// <summary>
        /// 立即执行sql
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="sql">要执行的sql语句</param>
        /// <returns>受影响的行数</returns>
        int ExecuteSql(TConnectionString conn, string sql);

        /// <summary>
        /// 立即执行sql
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>受影响的行数</returns>
        Task<int> ExecuteSqlAsync(TConnectionString conn, string sql, params object[] parameters);

        /// <summary>
        /// 立即执行sql
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="sql">要执行的sql语句</param>
        /// <returns>受影响的行数</returns>
        Task<int> ExecuteSqlAsync(TConnectionString conn, string sql);

        /// <summary>
        /// 先执行sql,然后添加数据实体
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="entity">要添加的数据实体</param>
        /// <param name="otherAction">事务中对context执行的其他操作</param>
        /// <param name="actionFirst">true：otherAction先执行</param>
        /// <returns></returns>
        bool AddByTrans(TConnectionString conn, TEntity entity, Action<TDbContext, TEntity> otherAction, bool actionFirst = true);

        /// <summary>
        /// 先执行sql,然后更新数据实体
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="entity">要更新的数据实体</param>
        /// <param name="otherAction">事务中对context执行的其他操作</param>
        /// <param name="actionFirst">true：otherAction先执行</param>
        /// <returns></returns>
        bool UpdateByTrans(TConnectionString conn, TEntity entity, Action<TDbContext, TEntity> otherAction, bool actionFirst = true);

        /// <summary>
        /// 先执行sql,然后删除数据实体
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="entity">要删除的数据实体</param>
        /// <param name="otherAction">事务中对context执行的其他操作</param>
        /// <param name="actionFirst">true：otherAction先执行</param>
        /// <returns></returns>
        bool DeleteByTrans(TConnectionString conn, TEntity entity, Action<TDbContext, TEntity> otherAction, bool actionFirst = true);

    }

    /// <summary>
    /// 基础数据接口
    /// by Rex Sheng @2017
    /// </summary>
    /// <typeparam name="TDbContext">上下文</typeparam>
    /// <typeparam name="TEntity">实体</typeparam>
    public interface IBaseRepository<TDbContext, TEntity> : IBaseRepository<string, TDbContext, TEntity>
        where TDbContext : DbContext, new()
        where TEntity : class
    {
        /// <summary>
        /// 获取数据库实体
        /// </summary>
        /// <returns></returns>
        TDbContext GetContext();

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Add(IEnumerable<TEntity> entities);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Update(TEntity entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Delete(TEntity entity);

        /// <summary>
        /// 获取所有数据（不立即执行）
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// 懒加载
        /// </summary>
        /// <param name="propertySelectors"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);


        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        List<TEntity> GetAllList();

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAllListAsync();

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="TOrderKey"></typeparam>
        /// <param name="orderPredicate"></param>
        /// <param name="asc"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        CommonPagedList<TEntity> ToCommonPagedList<TOrderKey>(Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc, int pageIndex, int pageSize);

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="TOrderKey"></typeparam>
        /// <param name="orderPredicate"></param>
        /// <param name="asc"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<CommonPagedList<TEntity>> ToCommonPagedListAsync<TOrderKey>(Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc, int pageIndex, int pageSize);

        /// <summary>
        /// 数量
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// 数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 数量
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();

        /// <summary>
        /// 数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 数量
        /// </summary>
        /// <returns></returns>
        long LongCount();

        /// <summary>
        /// 数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        long LongCount(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 数量
        /// </summary>
        /// <returns></returns>
        Task<long> LongCountAsync();

        /// <summary>
        /// 数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Exist(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 取第一条
        /// </summary>
        /// <returns></returns>
        TEntity FirstOrDefault();

        /// <summary>
        /// 取第一条
        /// </summary>
        /// <returns></returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 取第一条
        /// </summary>
        /// <returns></returns>
        TEntity FirstOrDefault<TOrderKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc);

        /// <summary>
        /// 取第一条
        /// </summary>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync();

        /// <summary>
        /// 取第一条
        /// </summary>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 取第一条
        /// </summary>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync<TOrderKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc);

        /// <summary>
        /// 根据主键获取
        /// </summary>
        /// <typeparam name="TPrimaryKey">主键类型</typeparam>
        /// <param name="conn">连接字符串</param>
        /// <param name="key">主键值</param>
        /// <returns></returns>
        TEntity Get<TPrimaryKey>(TPrimaryKey key);

        /// <summary>
        /// 根据主键获取
        /// </summary>
        /// <typeparam name="TPrimaryKey">主键类型</typeparam>
        /// <param name="conn">连接字符串</param>
        /// <param name="key">主键值</param>
        /// <returns></returns>
        Task<TEntity> GetAsync<TPrimaryKey>(TPrimaryKey key);

        /// <summary>
        /// 执行sql查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        List<T> GetList<T>(string sql);

        /// <summary>
        /// 执行sql查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        List<T> GetList<T>(string sql, params object[] parameters);

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteSql(string sql, params object[] parameters);

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int ExecuteSql(string sql);

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<int> ExecuteSqlAsync(string sql, params object[] parameters);

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<int> ExecuteSqlAsync(string sql);

        /// <summary>
        /// 先执行otherAction,后新增实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="otherAction"></param>
        /// <param name="actionFirst">true：otherAction先执行</param>
        /// <returns></returns>
        bool AddByTrans(TEntity entity, Action<TDbContext, TEntity> otherAction, bool actionFirst = true);

        /// <summary>
        /// 先执行otherAction,后更新实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="otherAction"></param>
        /// <param name="actionFirst">true：otherAction先执行</param>
        /// <returns></returns>
        bool UpdateByTrans(TEntity entity, Action<TDbContext, TEntity> otherAction, bool actionFirst = true);

        /// <summary>
        /// 先执行otherAction,后删除实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="otherAction"></param>
        /// <param name="actionFirst">true：otherAction先执行</param>
        /// <returns></returns>
        bool DeleteByTrans(TEntity entity, Action<TDbContext, TEntity> otherAction, bool actionFirst = true);

    }
}
