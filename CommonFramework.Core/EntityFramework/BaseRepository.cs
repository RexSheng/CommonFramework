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
    public class BaseRepository<TDbContext, TEntity> : IBaseRepository<TDbContext, TEntity>
       where TDbContext : DbContext, new()
       where TEntity : class
    {
        private readonly IDbContextProvider _contextProvider;
        public BaseRepository(IDbContextProvider contextProvider)
        {
            _contextProvider = contextProvider;
        }
        public TDbContext GetContext(object dbIndex=null)
        {
            return _contextProvider.GetContext<TDbContext>(dbIndex);
        }

        public virtual TEntity Add(TEntity entity, object conn=null)
        {
            TDbContext _context = GetContext(conn);
            _context.Entry<TEntity>(entity).State = EntityState.Added;
            _context.SaveChanges();
            return entity;
        }
        public virtual IEnumerable<TEntity> Add( IEnumerable<TEntity> entities, object conn = null)
        {
            TDbContext _context = GetContext(conn);
            _context.Set<TEntity>().AddRange(entities);
            _context.SaveChanges();
            return entities;
        }
        public virtual bool Update(TEntity entity, object conn = null)
        {
            TDbContext _context = GetContext(conn);
            _context.Set<TEntity>().Attach(entity);
            _context.Entry<TEntity>(entity).State = System.Data.Entity.EntityState.Modified;
            return _context.SaveChanges() > 0;
        }
        public virtual bool Delete( TEntity entity, object conn = null)
        {
            TDbContext _context = GetContext(conn);
            _context.Set<TEntity>().Attach(entity);
            _context.Entry<TEntity>(entity).State = System.Data.Entity.EntityState.Deleted;
            return _context.SaveChanges() > 0;
        }
        public virtual IQueryable<TEntity> GetAll(object conn=null)
        {
            TDbContext _context = GetContext(conn);
            return _context.Set<TEntity>();
        }
        public virtual IQueryable<TEntity> GetAllIncluding(object conn, params Expression<Func<TEntity, object>>[] propertySelectors)
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
        public virtual List<TEntity> GetAllList(object conn=null)
        {
            return GetAll(conn).ToList<TEntity>();
        }
        public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate, object conn = null)
        {
            return GetAll(conn).Where(predicate).ToList<TEntity>();
        }
        public virtual Task<List<TEntity>> GetAllListAsync(object conn=null)
        {
            return Task.FromResult(GetAllList(conn));
        }
        public virtual Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate, object conn = null)
        {
            return Task.FromResult(GetAllList(predicate,conn));
        }

        public virtual CommonPagedList<TEntity> ToCommonPagedList<TOrderKey>( Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc, int pageIndex, int pageSize, object conn = null)
        {
            return GetAll(conn).ToCommonPagedList(orderPredicate, asc, pageIndex, pageSize);
        }

        public virtual Task<CommonPagedList<TEntity>> ToCommonPagedListAsync<TOrderKey>( Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc, int pageIndex, int pageSize, object conn = null)
        {
            return Task.FromResult(ToCommonPagedList(orderPredicate, asc, pageIndex, pageSize,conn));
        }
        public virtual int Count(object conn=null)
        {
            return GetAll(conn).Count();
        }
        public virtual int Count(Expression<Func<TEntity, bool>> predicate, object conn = null)
        {
            return GetAll(conn).Count(predicate);
        }
        public virtual Task<int> CountAsync(object conn=null)
        {
            return Task.FromResult(Count(conn));
        }
        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, object conn = null)
        {
            return Task.FromResult(Count(predicate,conn));
        }
        public virtual long LongCount(object conn=null)
        {
            return GetAll(conn).LongCount();
        }
        public virtual long LongCount(Expression<Func<TEntity, bool>> predicate, object conn = null)
        {
            return GetAll(conn).LongCount(predicate);
        }
        public virtual Task<long> LongCountAsync(object conn=null)
        {
            return Task.FromResult(LongCount(conn));
        }
        public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate, object conn = null)
        {
            return Task.FromResult(LongCount(predicate,conn));
        }
        public virtual bool Exist(Expression<Func<TEntity, bool>> predicate, object conn = null)
        {
            return GetAll(conn).Any(predicate);
        }
        public virtual Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, object conn = null)
        {
            return Task.FromResult(Exist(predicate,conn));
        }
        public virtual TEntity FirstOrDefault(object conn=null)
        {
            return GetAll(conn).FirstOrDefault<TEntity>();
        }
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, object conn = null)
        {
            return GetAll(conn).FirstOrDefault<TEntity>(predicate);
        }
        public virtual TEntity FirstOrDefault<TOrderKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc, object conn = null)
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
        public virtual Task<TEntity> FirstOrDefaultAsync(object conn=null)
        {
            return Task.FromResult(FirstOrDefault(conn));
        }
        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, object conn = null)
        {
            return Task.FromResult(FirstOrDefault(predicate,conn));
        }
        public virtual Task<TEntity> FirstOrDefaultAsync<TOrderKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TOrderKey>> orderPredicate, bool asc, object conn = null)
        {
            return Task.FromResult(FirstOrDefault(predicate, orderPredicate, asc,conn));
        }
        public virtual TEntity Get<TPrimaryKey>(TPrimaryKey key, object conn = null)
        {
            TDbContext _context = GetContext(conn);
            return _context.Set<TEntity>().Find(key);
        }
        public virtual Task<TEntity> GetAsync<TPrimaryKey>(TPrimaryKey key, object conn = null)
        {
            TDbContext _context = GetContext(conn);
            return _context.Set<TEntity>().FindAsync(key);
        }
        public virtual List<T> GetList<T>( string sql, object conn = null)
        {
            return GetList<T>(conn,sql, null);
        }
        public virtual List<T> GetList<T>(object conn, string sql, params object[] parameters)
        {
            TDbContext _context = GetContext(conn);
            return _context.Database.SqlQuery<T>(sql, parameters).ToList();
        }
        public virtual int ExecuteSql(object conn, string sql, params object[] parameters)
        {
            TDbContext _context = GetContext(conn);
            return _context.Database.ExecuteSqlCommand(sql, parameters);
        }
        public virtual int ExecuteSql(string sql, object conn = null)
        {
            return ExecuteSql(conn, sql, null);
        }
        public virtual Task<int> ExecuteSqlAsync(object conn, string sql, params object[] parameters)
        {
            TDbContext _context = GetContext(conn);
            return _context.Database.ExecuteSqlCommandAsync(sql, parameters);
        }
        public virtual Task<int> ExecuteSqlAsync(string sql, object conn = null)
        {
            return ExecuteSqlAsync(conn,sql, null);
        }
        public virtual T GetEntity<T>(object conn, string sql, params object[] parameters)
        {
            TDbContext _context = GetContext(conn);
            return _context.Database.SqlQuery<T>(sql, parameters).FirstOrDefault();
        }
        public virtual T GetEntity<T>(string sql, object conn=null)
        {
            return GetEntity<T>(conn, sql, null);
        }
        public virtual Task<T> GetEntityAsync<T>(object conn, string sql, params object[] parameters)
        {
            TDbContext _context = GetContext(conn);
            return _context.Database.SqlQuery<T>(sql, parameters).FirstOrDefaultAsync();
        }
        public virtual Task<T> GetEntityAsync<T>(string sql, object conn = null)
        {
            return GetEntityAsync<T>(conn, sql, null);
        }

        public virtual bool AddByTrans(TEntity entity, Action<TDbContext, TEntity> beforeAction = null, Action<TDbContext, TEntity> afterAction = null, object conn = null)
        {
            TDbContext _context = GetContext(conn);
            using (var trans = _context.Database.BeginTransaction())
            {
                try
                {
                    if (beforeAction != null)
                    {
                        beforeAction.Invoke(_context, entity);
                    }

                    _context.Entry<TEntity>(entity).State = System.Data.Entity.EntityState.Added;
                    _context.SaveChanges();

                    if (afterAction != null)
                    {
                        afterAction.Invoke(_context, entity);
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
        public virtual bool UpdateByTrans(TEntity entity, Action<TDbContext, TEntity> beforeAction = null, Action<TDbContext, TEntity> afterAction = null, object conn = null)
        {
            TDbContext _context = GetContext(conn);
            using (var trans = _context.Database.BeginTransaction())
            {
                try
                {
                    if (beforeAction != null)
                    {
                        beforeAction.Invoke(_context, entity);
                    }
                    _context.Set<TEntity>().Attach(entity);
                    _context.Entry<TEntity>(entity).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();
                    if (afterAction != null)
                    {
                        afterAction.Invoke(_context, entity);
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
        public virtual bool DeleteByTrans(TEntity entity, Action<TDbContext, TEntity> beforeAction=null, Action<TDbContext, TEntity> afterAction = null, object conn = null)
        {
            TDbContext _context = GetContext(conn);
            using (var trans = _context.Database.BeginTransaction())
            {
                try
                {
                    if (beforeAction!=null)
                    {
                        beforeAction.Invoke(_context, entity);
                    }
                    _context.Set<TEntity>().Attach(entity);
                    _context.Entry<TEntity>(entity).State = System.Data.Entity.EntityState.Deleted;
                    _context.SaveChanges();
                    if (afterAction!=null)
                    {
                        afterAction.Invoke(_context, entity);
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

        public virtual bool DbTransaction(Action<TDbContext> action,object conn=null) {

            TDbContext _context = GetContext(conn);
            using (var trans = _context.Database.BeginTransaction())
            {
                try
                { 
                    action.Invoke(_context);
                    
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

    
}
