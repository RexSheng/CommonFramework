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
    public class BaseRepository<TConnectionString, TDbContext, TEntity> : IBaseRepository<TConnectionString, TDbContext, TEntity>
       where TDbContext : DbContext, new()
       where TEntity : class
    {
        private readonly IDbContextProvider _contextProvider;
        public BaseRepository(IDbContextProvider contextProvider)
        {
            _contextProvider = contextProvider;
        }
        public TDbContext GetContext(TConnectionString dbIndex)
        {
            return _contextProvider.GetContext<TDbContext, TConnectionString>(dbIndex);
        }

        public virtual TEntity Add(TConnectionString conn, TEntity entity)
        {
            TDbContext _context = GetContext(conn);
            _context.Entry<TEntity>(entity).State = EntityState.Added;
            _context.SaveChanges();
            return entity;
        }
        public virtual IEnumerable<TEntity> Add(TConnectionString conn, IEnumerable<TEntity> entities)
        {
            TDbContext _context = GetContext(conn);
            _context.Set<TEntity>().AddRange(entities);
            _context.SaveChanges();
            return entities;
        }
        public virtual bool Update(TConnectionString conn, TEntity entity)
        {
            TDbContext _context = GetContext(conn);
            _context.Set<TEntity>().Attach(entity);
            _context.Entry<TEntity>(entity).State = System.Data.Entity.EntityState.Modified;
            return _context.SaveChanges() > 0;
        }
        public virtual bool Delete(TConnectionString conn, TEntity entity)
        {
            TDbContext _context = GetContext(conn);
            _context.Set<TEntity>().Attach(entity);
            _context.Entry<TEntity>(entity).State = System.Data.Entity.EntityState.Deleted;
            return _context.SaveChanges() > 0;
        }
        public virtual IQueryable<TEntity> GetAll(TConnectionString conn)
        {
            TDbContext _context = GetContext(conn);
            return _context.Set<TEntity>();
        }
        public virtual IQueryable<TEntity> GetAllIncluding(TConnectionString conn, params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            TDbContext _context = GetContext(conn);
            var query = _context.Set<TEntity>().AsQueryable();

            if (propertySelectors != null)
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }
            return query;
        }
        public virtual List<TEntity> GetAllList(TConnectionString conn)
        {
            return GetAll(conn).ToList<TEntity>();
        }
        public virtual List<TEntity> GetAllList(TConnectionString conn, Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll(conn).Where(predicate).ToList<TEntity>();
        }
        public virtual Task<List<TEntity>> GetAllListAsync(TConnectionString conn)
        {
            return Task.FromResult(GetAllList(conn));
        }
        public virtual Task<List<TEntity>> GetAllListAsync(TConnectionString conn, Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(GetAllList(conn, predicate));
        }

        public virtual CommonPagedList<TEntity> ToCommonPagedList<TOrderKey>(TConnectionString conn, Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc, int pageIndex, int pageSize)
        {
            return GetAll(conn).ToCommonPagedList(orderPredicate, asc, pageIndex, pageSize);
        }

        public virtual Task<CommonPagedList<TEntity>> ToCommonPagedListAsync<TOrderKey>(TConnectionString conn, Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc, int pageIndex, int pageSize)
        {
            return Task.FromResult(ToCommonPagedList(conn, orderPredicate, asc, pageIndex, pageSize));
        }
        public virtual int Count(TConnectionString conn)
        {
            return GetAll(conn).Count();
        }
        public virtual int Count(TConnectionString conn, Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll(conn).Count(predicate);
        }
        public virtual Task<int> CountAsync(TConnectionString conn)
        {
            return Task.FromResult(Count(conn));
        }
        public virtual Task<int> CountAsync(TConnectionString conn, Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Count(conn, predicate));
        }
        public virtual long LongCount(TConnectionString conn)
        {
            return GetAll(conn).LongCount();
        }
        public virtual long LongCount(TConnectionString conn, Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll(conn).LongCount(predicate);
        }
        public virtual Task<long> LongCountAsync(TConnectionString conn)
        {
            return Task.FromResult(LongCount(conn));
        }
        public virtual Task<long> LongCountAsync(TConnectionString conn, Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(LongCount(conn, predicate));
        }
        public virtual bool Exist(TConnectionString conn, Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll(conn).Any(predicate);
        }
        public virtual Task<bool> ExistAsync(TConnectionString conn, Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Exist(conn, predicate));
        }
        public virtual TEntity FirstOrDefault(TConnectionString conn)
        {
            return GetAll(conn).FirstOrDefault<TEntity>();
        }
        public virtual TEntity FirstOrDefault(TConnectionString conn, Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll(conn).FirstOrDefault<TEntity>(predicate);
        }
        public virtual TEntity FirstOrDefault<TOrderKey>(TConnectionString conn, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc)
        {
            if (asc)
            {
                return GetAll(conn).OrderBy(orderPredicate).FirstOrDefault<TEntity>(predicate);
            }
            else
            {
                return GetAll(conn).OrderByDescending(orderPredicate).FirstOrDefault<TEntity>(predicate);
            }

        }
        public virtual Task<TEntity> FirstOrDefaultAsync(TConnectionString conn)
        {
            return Task.FromResult(FirstOrDefault(conn));
        }
        public virtual Task<TEntity> FirstOrDefaultAsync(TConnectionString conn, Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(FirstOrDefault(conn, predicate));
        }
        public virtual Task<TEntity> FirstOrDefaultAsync<TOrderKey>(TConnectionString conn, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc)
        {
            return Task.FromResult(FirstOrDefault(conn, predicate, orderPredicate, asc));
        }
        public virtual TEntity Get<TPrimaryKey>(TConnectionString conn, TPrimaryKey key)
        {
            TDbContext _context = GetContext(conn);
            return _context.Set<TEntity>().Find(key);
        }
        public virtual Task<TEntity> GetAsync<TPrimaryKey>(TConnectionString conn, TPrimaryKey key)
        {
            TDbContext _context = GetContext(conn);
            return _context.Set<TEntity>().FindAsync(key);
        }
        public virtual List<T> GetList<T>(TConnectionString conn, string sql)
        {
            return GetList<T>(conn, sql, null);
        }
        public virtual List<T> GetList<T>(TConnectionString conn, string sql, params object[] parameters)
        {
            TDbContext _context = GetContext(conn);
            return _context.Database.SqlQuery<T>(sql, parameters).ToList();
        }
        public virtual int ExecuteSql(TConnectionString conn, string sql, params object[] parameters)
        {
            TDbContext _context = GetContext(conn);
            return _context.Database.ExecuteSqlCommand(sql, parameters);
        }
        public virtual int ExecuteSql(TConnectionString conn, string sql)
        {
            return ExecuteSql(conn, sql, null);
        }
        public virtual Task<int> ExecuteSqlAsync(TConnectionString conn, string sql, params object[] parameters)
        {
            TDbContext _context = GetContext(conn);
            return _context.Database.ExecuteSqlCommandAsync(sql, parameters);
        }
        public virtual Task<int> ExecuteSqlAsync(TConnectionString conn, string sql)
        {
            return ExecuteSqlAsync(conn, sql, null);
        }
        public virtual T GetEntity<T>(TConnectionString conn, string sql, params object[] parameters)
        {
            TDbContext _context = GetContext(conn);
            return _context.Database.SqlQuery<T>(sql, parameters).FirstOrDefault();
        }
        public virtual T GetEntity<T>(TConnectionString conn, string sql)
        {
            return GetEntity<T>(conn, sql, null);
        }
        public virtual Task<T> GetEntityAsync<T>(TConnectionString conn, string sql, params object[] parameters)
        {
            TDbContext _context = GetContext(conn);
            return _context.Database.SqlQuery<T>(sql, parameters).FirstOrDefaultAsync();
        }
        public virtual Task<T> GetEntityAsync<T>(TConnectionString conn, string sql)
        {
            return GetEntityAsync<T>(conn, sql, null);
        }

        public virtual bool AddByTrans(TConnectionString conn, TEntity entity, Action<TDbContext, TEntity> otherAction, bool actionFirst = true)
        {
            TDbContext _context = GetContext(conn);
            using (var trans = _context.Database.BeginTransaction())
            {
                try
                {
                    if (actionFirst)
                    {
                        otherAction.Invoke(_context, entity);
                    }

                    _context.Entry<TEntity>(entity).State = System.Data.Entity.EntityState.Added;
                    _context.SaveChanges();

                    if (!actionFirst)
                    {
                        otherAction.Invoke(_context, entity);
                    }
                    trans.Commit();
                    return true;
                }
                catch (Exception)
                {
                    trans.Rollback();
                    return false;
                }
            }
        }
        public virtual bool UpdateByTrans(TConnectionString conn, TEntity entity, Action<TDbContext, TEntity> otherAction, bool actionFirst = true)
        {
            TDbContext _context = GetContext(conn);
            using (var trans = _context.Database.BeginTransaction())
            {
                try
                {
                    if (actionFirst)
                    {
                        otherAction.Invoke(_context, entity);
                    }
                    _context.Set<TEntity>().Attach(entity);
                    _context.Entry<TEntity>(entity).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();
                    if (!actionFirst)
                    {
                        otherAction.Invoke(_context, entity);
                    }
                    trans.Commit();
                    return true;
                }
                catch (Exception)
                {
                    trans.Rollback();
                    return false;
                }
            }
        }
        public virtual bool DeleteByTrans(TConnectionString conn, TEntity entity, Action<TDbContext, TEntity> otherAction, bool actionFirst = true)
        {
            TDbContext _context = GetContext(conn);
            using (var trans = _context.Database.BeginTransaction())
            {
                try
                {
                    if (actionFirst)
                    {
                        otherAction.Invoke(_context, entity);
                    }
                    _context.Set<TEntity>().Attach(entity);
                    _context.Entry<TEntity>(entity).State = System.Data.Entity.EntityState.Deleted;
                    _context.SaveChanges();
                    if (!actionFirst)
                    {
                        otherAction.Invoke(_context, entity);
                    }
                    trans.Commit();
                    return true;
                }
                catch (Exception)
                {
                    trans.Rollback();
                    return false;
                }
            }
        }
    }

    public class BaseRepository<TDbContext, TEntity> : BaseRepository<string, TDbContext, TEntity>, IBaseRepository<TDbContext, TEntity>
        where TDbContext : DbContext, new()
        where TEntity : class
    {
        public BaseRepository(IDbContextProvider contextProvider) : base(contextProvider)
        {
        }

        private string DefaultConnectionString
        {
            get
            {
                return null;
            }
        }
        //public BaseDao(Func<string, string> connectionProvider, string dbIndex):base(connectionProvider, dbIndex)
        //{

        //}
        /// <summary>
        /// 获取数据库实体
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public TDbContext GetContext()
        {
            return GetContext(DefaultConnectionString);
        }
        public virtual TEntity Add(TEntity entity)
        {
            return base.Add(DefaultConnectionString, entity);
        }
        public virtual IEnumerable<TEntity> Add(IEnumerable<TEntity> entities)
        {
            return base.Add(DefaultConnectionString, entities);
        }
        public virtual bool Update(TEntity entity)
        {
            return base.Update(DefaultConnectionString, entity);
        }
        public virtual bool Delete(TEntity entity)
        {
            return base.Delete(DefaultConnectionString, entity);
        }
        public virtual IQueryable<TEntity> GetAll()
        {
            return base.GetAll(DefaultConnectionString);
        }
        public virtual IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            return base.GetAllIncluding(DefaultConnectionString, propertySelectors);
        }
        public virtual List<TEntity> GetAllList()
        {
            return base.GetAllList(DefaultConnectionString);
        }
        public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return base.GetAllList(DefaultConnectionString, predicate);
        }
        public virtual Task<List<TEntity>> GetAllListAsync()
        {
            return base.GetAllListAsync(DefaultConnectionString);
        }
        public virtual Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return base.GetAllListAsync(DefaultConnectionString, predicate);
        }
        public virtual CommonPagedList<TEntity> ToCommonPagedList<TOrderKey>(Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc, int pageIndex, int pageSize)
        {
            return base.ToCommonPagedList(DefaultConnectionString, orderPredicate, asc, pageIndex, pageSize);
        }
        public virtual Task<CommonPagedList<TEntity>> ToCommonPagedListAsync<TOrderKey>(Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc, int pageIndex, int pageSize)
        {
            return base.ToCommonPagedListAsync(DefaultConnectionString, orderPredicate, asc, pageIndex, pageSize);
        }
        public virtual int Count()
        {
            return base.Count(DefaultConnectionString);
        }
        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return base.Count(DefaultConnectionString, predicate);
        }
        public virtual Task<int> CountAsync()
        {
            return base.CountAsync(DefaultConnectionString);
        }
        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return base.CountAsync(DefaultConnectionString, predicate);
        }
        public virtual long LongCount()
        {
            return base.LongCount(DefaultConnectionString);
        }
        public virtual long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return base.LongCount(DefaultConnectionString, predicate);
        }
        public virtual Task<long> LongCountAsync()
        {
            return base.LongCountAsync(DefaultConnectionString);
        }
        public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return base.LongCountAsync(DefaultConnectionString, predicate);
        }
        public virtual bool Exist(Expression<Func<TEntity, bool>> predicate)
        {
            return base.Exist(DefaultConnectionString, predicate);
        }
        public virtual Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return base.ExistAsync(DefaultConnectionString, predicate);
        }
        public virtual TEntity FirstOrDefault()
        {
            return base.FirstOrDefault(DefaultConnectionString);
        }
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return base.FirstOrDefault(DefaultConnectionString, predicate);
        }
        public virtual TEntity FirstOrDefault<TOrderKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc)
        {
            return base.FirstOrDefault(DefaultConnectionString, predicate, orderPredicate, asc);
        }
        public virtual Task<TEntity> FirstOrDefaultAsync()
        {
            return base.FirstOrDefaultAsync(DefaultConnectionString);
        }
        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return base.FirstOrDefaultAsync(DefaultConnectionString, predicate);
        }
        public virtual Task<TEntity> FirstOrDefaultAsync<TOrderKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc)
        {
            return base.FirstOrDefaultAsync(DefaultConnectionString, predicate, orderPredicate, asc);
        }
        public virtual TEntity Get<TPrimaryKey>(TPrimaryKey key)
        {
            return base.Get<TPrimaryKey>(DefaultConnectionString, key);
        }
        public virtual Task<TEntity> GetAsync<TPrimaryKey>(TPrimaryKey key)
        {
            return base.GetAsync<TPrimaryKey>(DefaultConnectionString, key);
        }
        public virtual List<T> GetList<T>(string sql)
        {
            return base.GetList<T>(DefaultConnectionString, sql);
        }
        public virtual List<T> GetList<T>(string sql, params object[] parameters)
        {
            return base.GetList<T>(DefaultConnectionString, sql, parameters);
        }
        public virtual int ExecuteSql(string sql, params object[] parameters)
        {
            return base.ExecuteSql(DefaultConnectionString, sql, parameters);
        }
        public virtual int ExecuteSql(string sql)
        {
            return base.ExecuteSql(DefaultConnectionString, sql);
        }
        public virtual Task<int> ExecuteSqlAsync(string sql, params object[] parameters)
        {
            return base.ExecuteSqlAsync(DefaultConnectionString, sql, parameters);
        }
        public virtual Task<int> ExecuteSqlAsync(string sql)
        {
            return base.ExecuteSqlAsync(DefaultConnectionString, sql);
        }
        public virtual T GetEntity<T>(string sql, params object[] parameters)
        {
            return base.GetEntity<T>(DefaultConnectionString, sql, parameters);
        }
        public virtual T GetEntity<T>(string sql)
        {
            return base.GetEntity<T>(DefaultConnectionString, sql);
        }
        public virtual Task<T> GetEntityAsync<T>(string sql, params object[] parameters)
        {
            return base.GetEntityAsync<T>(DefaultConnectionString, sql, parameters);
        }
        public virtual Task<T> GetEntityAsync<T>(string sql)
        {
            return base.GetEntityAsync<T>(DefaultConnectionString, sql);
        }
        public virtual bool AddByTrans(TEntity entity, Action<TDbContext, TEntity> otherAction, bool actionFirst = true)
        {
            return base.AddByTrans(DefaultConnectionString, entity, otherAction, actionFirst);
        }
        public virtual bool UpdateByTrans(TEntity entity, Action<TDbContext, TEntity> otherAction, bool actionFirst = true)
        {
            return base.UpdateByTrans(DefaultConnectionString, entity, otherAction, actionFirst);
        }
        public virtual bool DeleteByTrans(TEntity entity, Action<TDbContext, TEntity> otherAction, bool actionFirst = true)
        {
            return base.DeleteByTrans(DefaultConnectionString, entity, otherAction, actionFirst);
        }
    }

}
