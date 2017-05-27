using CommonFramework.SqlServer.PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CommonFramework.SqlServer.EntityFramework
{
    /// <summary>
    /// 基础数据接口
    /// by 生旭鹏 @2017
    /// </summary>
    /// <typeparam name="TConncetionString">连接字符串</typeparam>
    /// <typeparam name="TDbContext">上下文</typeparam>
    /// <typeparam name="TEntity">实体</typeparam>
    public interface IBaseDao<TConncetionString, TDbContext, TEntity>: ICommonInterface
         where TDbContext : DbContext, new()
        where TEntity : class
    {
        /// <summary>
        /// 获取数据库实体
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        TDbContext GetContext(TConncetionString conn);

        /// 新增实体数据
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="entity">要新增的实体</param>
        /// <returns>新增后的实体</returns>
        TEntity Add(TConncetionString conn, TEntity entity);

        /// 新增多个实体数据
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="entity">要新增的实体</param>
        /// <returns>新增后的实体</returns>
        IEnumerable<TEntity> Add(TConncetionString conn, IEnumerable<TEntity> entities);
        
        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="entity">更新的实体</param>
        /// <returns>是否更新成功</returns>
        bool Update(TConncetionString conn, TEntity entity);
        
        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="entity">删除的实体</param>
        /// <returns>是否删除成功</returns>
        bool Delete(TConncetionString conn, TEntity entity);
        
        /// <summary>
        /// 查询所有数据（不立即执行查询）
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        IQueryable<TEntity> GetAll(TConncetionString conn);

        /// <summary>
        /// 懒加载
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="propertySelectors"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetAllIncluding(TConncetionString conn, params Expression<Func<TEntity, object>>[] propertySelectors);
         

        /// <summary>
        /// 查询所有数据（立即执行查询）
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        List<TEntity> GetAllList(TConncetionString conn);

        /// <summary>
        /// 查询数据（立即执行查询）
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        List<TEntity> GetAllList(TConncetionString conn, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 异步查询数据
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        Task<List<TEntity>> GetAllListAsync(TConncetionString conn);

        /// <summary>
        /// 异步查询数据
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        Task<List<TEntity>> GetAllListAsync(TConncetionString conn, Expression<Func<TEntity, bool>> predicate);

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
        CommonPagedList<TEntity> ToCommonPagedList<TOrderKey>(TConncetionString conn, Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc, int pageIndex, int pageSize);

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
        Task<CommonPagedList<TEntity>> ToCommonPagedListAsync<TOrderKey>(TConncetionString conn, Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc, int pageIndex, int pageSize);

        /// <summary>
        /// 求数量
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        int Count(TConncetionString conn);

        /// <summary>
        /// 求数量
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        int Count(TConncetionString conn, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 求数量
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        Task<int> CountAsync(TConncetionString conn);

        /// <summary>
        /// 求数量
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        Task<int> CountAsync(TConncetionString conn, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 求数量
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        long LongCount(TConncetionString conn);

        /// <summary>
        /// 求数量
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        long LongCount(TConncetionString conn, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 求数量
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        Task<long> LongCountAsync(TConncetionString conn);

        /// <summary>
        /// 求数量
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        Task<long> LongCountAsync(TConncetionString conn, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        bool Exist(TConncetionString conn, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        Task<bool> ExistAsync(TConncetionString conn, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 第一条
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        TEntity FirstOrDefault(TConncetionString conn);

        /// <summary>
        /// 第一条
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        TEntity FirstOrDefault(TConncetionString conn, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 第一条
        /// </summary>
        /// <typeparam name="TOrderKey">排序字段</typeparam>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderPredicate">排序表达式</param>
        /// <param name="asc">是否升序</param>
        /// <returns></returns>
        TEntity FirstOrDefault<TOrderKey>(TConncetionString conn, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc);

        /// <summary>
        /// 第一条
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(TConncetionString conn);

        /// <summary>
        /// 第一条
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(TConncetionString conn, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 第一条
        /// </summary>
        /// <typeparam name="TOrderKey">排序字段</typeparam>
        /// <param name="conn">连接字符串</param>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderPredicate">排序表达式</param>
        /// <param name="asc">是否升序</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync<TOrderKey>(TConncetionString conn, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc);

        /// <summary>
        /// 根据主键获取
        /// </summary>
        /// <typeparam name="TPrimaryKey">主键类型</typeparam>
        /// <param name="conn">连接字符串</param>
        /// <param name="key">主键值</param>
        /// <returns></returns>
        TEntity Get<TPrimaryKey>(TConncetionString conn, TPrimaryKey key);

        /// <summary>
        /// 根据主键获取
        /// </summary>
        /// <typeparam name="TPrimaryKey">主键类型</typeparam>
        /// <param name="conn">连接字符串</param>
        /// <param name="key">主键值</param>
        /// <returns></returns>
        Task<TEntity> GetAsync<TPrimaryKey>(TConncetionString conn, TPrimaryKey key);

        /// <summary>
        /// 执行sql,查询数据
        /// </summary>
        /// <typeparam name="T">返回的数据模型</typeparam>
        /// <param name="conn">连接字符串</param>
        /// <param name="sql">要执行的sql语句</param>
        /// <returns></returns>
        List<T> GetList<T>(TConncetionString conn, string sql);

        /// <summary>
        /// 执行sql,查询数据
        /// </summary>
        /// <typeparam name="T">返回的数据模型</typeparam>
        /// <param name="conn">连接字符串</param>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        List<T> GetList<T>(TConncetionString conn, string sql, params object[] parameters);

        /// <summary>
        /// 立即执行sql
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>受影响的行数</returns>
        int ExecuteSql(TConncetionString conn, string sql, params object[] parameters);

        /// <summary>
        /// 立即执行sql
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="sql">要执行的sql语句</param>
        /// <returns>受影响的行数</returns>
        int ExecuteSql(TConncetionString conn, string sql);

        /// <summary>
        /// 立即执行sql
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>受影响的行数</returns>
        Task<int> ExecuteSqlAsync(TConncetionString conn, string sql, params object[] parameters);

        /// <summary>
        /// 立即执行sql
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="sql">要执行的sql语句</param>
        /// <returns>受影响的行数</returns>
        Task<int> ExecuteSqlAsync(TConncetionString conn, string sql);

        /// <summary>
        /// 先执行sql,然后添加数据实体
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="entity">要添加的数据实体</param>
        /// <param name="sqls">要执行的sql语句</param>
        /// <returns></returns>
        bool AddByTrans(TConncetionString conn, TEntity entity, string[] sqls);

        /// <summary>
        /// 先执行sql,然后更新数据实体
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="entity">要更新的数据实体</param>
        /// <param name="sqls">要执行的sql语句</param>
        /// <returns></returns>
        bool UpdateByTrans(TConncetionString conn, TEntity entity, string[] sqls);

        /// <summary>
        /// 先执行sql,然后删除数据实体
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="entity">要删除的数据实体</param>
        /// <param name="sqls">要执行的sql语句</param>
        /// <returns></returns>
        bool DeleteByTrans(TConncetionString conn, TEntity entity, string[] sqls);
        
    }

    /// <summary>
    /// 基础数据接口
    /// by 生旭鹏 @2017
    /// </summary>
    /// <typeparam name="TDbContext">上下文</typeparam>
    /// <typeparam name="TEntity">实体</typeparam>
    public interface IBaseDao<TDbContext, TEntity>: IBaseDao<string,TDbContext, TEntity>
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
        /// 先执行sql,后新增实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sqls"></param>
        /// <returns></returns>
        bool AddByTrans(TEntity entity, string[] sqls);

        /// <summary>
        /// 先执行sql,后更新实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sqls"></param>
        /// <returns></returns>
        bool UpdateByTrans(TEntity entity, string[] sqls);

        /// <summary>
        /// 先执行sql,后删除实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sqls"></param>
        /// <returns></returns>
        bool DeleteByTrans(TEntity entity, string[] sqls);
        
    }
}
