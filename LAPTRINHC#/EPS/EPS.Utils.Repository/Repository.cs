using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EPS.Utils.Repository.Audit;
using System.Reflection;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using EPS.Utils.Common;
using System.Linq.Dynamic.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EPS.Utils.Repository
{
    public class Repository<TContext, TUser, TUserKey> : IRepository
        where TContext : DbContext, new()
        where TUser : class
        where TUserKey : struct
    {
        protected TContext _dbContext;
        protected IUserIdentity<TUserKey> _currentUser;
        protected ILogger _logger;
        private static JsonSerializerSettings _jsonSettings = new JsonSerializerSettings()
        {
            ContractResolver = new CustomResolver(),
            PreserveReferencesHandling = PreserveReferencesHandling.None,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented,
        };

        public Repository(TContext dbContext, IUserIdentity<TUserKey> currentUser, ILogger logger)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
            _logger = logger;
        }

        public virtual IQueryable<TEntity> All<TEntity>()
            where TEntity : class
        {
            return _dbContext.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> FromSqlRaw<TEntity>(string sql, params object[] parameters)
            where TEntity : class
        {
            return _dbContext.Set<TEntity>().FromSqlRaw(sql, parameters);
        }

        public virtual int Count<TEntity>(params Expression<Func<TEntity, bool>>[] predicates)
            where TEntity : class
        {
            return _dbContext.Set<TEntity>()
                .WhereMany(predicates)
                .Count();
        }

        public async virtual Task<int> CountAsync<TEntity>(params Expression<Func<TEntity, bool>>[] predicates)
            where TEntity : class
        {
            return await _dbContext.Set<TEntity>()
                .WhereMany(predicates)
                .CountAsync();
        }

        public virtual TEntity Find<TEntity>(params Expression<Func<TEntity, bool>>[] predicates)
            where TEntity : class
        {
            return _dbContext.Set<TEntity>()
                .WhereMany(predicates)
                .FirstOrDefault();
        }

        public virtual TEntity Find<TEntity>(object id)
            where TEntity : class
        {
            if (id is object[])
            {
                return _dbContext.Set<TEntity>().Find(id as object[]);
            }
            else
            {
                return _dbContext.Set<TEntity>().Find(id);
            }
        }

        public async virtual Task<TEntity> FindAsync<TEntity>(params Expression<Func<TEntity, bool>>[] predicates)
            where TEntity : class
        {
            return await _dbContext.Set<TEntity>()
                .WhereMany(predicates)
                .FirstOrDefaultAsync();
        }

        public async virtual Task<TEntity> FindAsync<TEntity>(object id)
            where TEntity : class
        {
            if (id is object[])
            {
                return await _dbContext.Set<TEntity>().FindAsync(id as object[]);
            }
            else
            {
                var entity = await _dbContext.Set<TEntity>().FindAsync(id);
                return entity;
            }
        }

        public virtual IQueryable<TEntity> Filter<TEntity>(params Expression<Func<TEntity, bool>>[] predicates)
            where TEntity : class
        {
            return _dbContext.Set<TEntity>()
                .WhereMany(predicates);
        }

        public bool Contain<TEntity>(params Expression<Func<TEntity, bool>>[] predicates)
            where TEntity : class
        {
            return _dbContext.Set<TEntity>().WhereMany(predicates).Any();
        }

        public async Task<bool> ContainAsync<TEntity>(params Expression<Func<TEntity, bool>>[] predicates)
            where TEntity : class
        {
            return await _dbContext.Set<TEntity>().WhereMany(predicates).AnyAsync();
        }

        #region CRUD

        public virtual void Create<TEntity>(params TEntity[] entities)
            where TEntity : class
        {
            foreach (var entity in entities)
            {
                AddCreateInfo<TEntity>(entity);
                _dbContext.Set<TEntity>().Add(entity);
            }
            _dbContext.SaveChanges();

            foreach (var entity in entities)
            {
                AddCreateLog<TEntity>(entity);
            }
            _dbContext.SaveChanges();
        }

        public async virtual Task CreateAsync<TEntity>(params TEntity[] entities)
            where TEntity : class
        {
            foreach (var entity in entities)
            {
                AddCreateInfo<TEntity>(entity);
                _dbContext.Set<TEntity>().Add(entity);
            }
            await _dbContext.SaveChangesAsync();

            foreach (var entity in entities)
            {
                AddCreateLog<TEntity>(entity);
            }
            await _dbContext.SaveChangesAsync();
        }

        public virtual int Delete<TEntity, TKey>(TKey id)
            where TEntity : class
        {
            var entity = Find<TEntity>(id);

            return Delete(entity);
        }

        public virtual int Delete<TEntity, TKey>(TKey[] ids)
            where TEntity : class
        {
            foreach (var id in ids)
            {
                var entity = Find<TEntity>(id);

                AddDeleteLog<TEntity>(entity);

                if (entity is ICascadeDelete)
                {
                    (entity as ICascadeDelete).OnDelete();
                }

                if (typeof(IDeleteInfo<TUser, TUserKey>).IsAssignableFrom(typeof(TEntity)))
                {
                    AddDeleteInfo<TEntity>(entity);
                }
                else
                {
                    _dbContext.Set<TEntity>().Remove(entity);
                }
            }
            return _dbContext.SaveChanges();
        }

        public virtual int Delete<TEntity>(TEntity entity)
            where TEntity : class
        {
            AddDeleteLog<TEntity>(entity);

            if (entity is ICascadeDelete)
            {
                (entity as ICascadeDelete).OnDelete();
            }

            if (typeof(IDeleteInfo<TUser, TUserKey>).IsAssignableFrom(typeof(TEntity)))
            {
                AddDeleteInfo<TEntity>(entity);
            }
            else
            {
                _dbContext.Set<TEntity>().Remove(entity);
            }

            return _dbContext.SaveChanges();
        }

        public virtual int Delete<TEntity>(TEntity[] entities)
            where TEntity : class
        {
            foreach (var entity in entities)
            {
                AddDeleteLog<TEntity>(entity);

                if (entity is ICascadeDelete)
                {
                    (entity as ICascadeDelete).OnDelete();
                }

                if (typeof(IDeleteInfo<TUser, TUserKey>).IsAssignableFrom(typeof(TEntity)))
                {
                    AddDeleteInfo<TEntity>(entity);
                }
                else
                {
                    _dbContext.Set<TEntity>().Remove(entity);
                }
            }
            return _dbContext.SaveChanges();
        }

        public virtual int Delete<TEntity>(params Expression<Func<TEntity, bool>>[] predicates)
            where TEntity : class
        {
            var entities = Filter<TEntity>(predicates).ToArray();

            foreach (var entity in entities)
            {
                AddDeleteLog<TEntity>(entity);

                if (entity is ICascadeDelete)
                {
                    (entity as ICascadeDelete).OnDelete();
                }

                if (typeof(IDeleteInfo<TUser, TUserKey>).IsAssignableFrom(typeof(TEntity)))
                {
                    AddDeleteInfo<TEntity>(entity);
                }
                else
                {
                    _dbContext.Set<TEntity>().Remove(entity);
                }
            }
            return _dbContext.SaveChanges();
        }

        public async virtual Task<int> DeleteAsync<TEntity, TKey>(TKey id)
            where TEntity : class
        {
            var entity = await FindAsync<TEntity>(id);

            return await DeleteAsync(entity);
        }

        public async virtual Task<int> DeleteAsync<TEntity, TKey>(params TKey[] ids)
            where TEntity : class
        {
            foreach (var id in ids)
            {
                var entity = await FindAsync<TEntity>(id);

                AddDeleteLog<TEntity>(entity);

                if (entity is ICascadeDelete)
                {
                    (entity as ICascadeDelete).OnDelete();
                }

                if (typeof(IDeleteInfo<TUser, TUserKey>).IsAssignableFrom(typeof(TEntity)))
                {
                    AddDeleteInfo<TEntity>(entity);
                }
                else
                {
                    _dbContext.Set<TEntity>().Remove(entity);
                }
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async virtual Task<int> DeleteAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            AddDeleteLog<TEntity>(entity);

            if (entity is ICascadeDelete)
            {
                (entity as ICascadeDelete).OnDelete();
            }

            if (typeof(IDeleteInfo<TUser, TUserKey>).IsAssignableFrom(typeof(TEntity)))
            {
                AddDeleteInfo<TEntity>(entity);
            }
            else
            {
                _dbContext.Set<TEntity>().Remove(entity);
            }

            return await _dbContext.SaveChangesAsync();
        }

        public async virtual Task<int> DeleteAsync<TEntity>(params TEntity[] entities)
            where TEntity : class
        {
            foreach (var entity in entities)
            {
                AddDeleteLog<TEntity>(entity);

                if (entity is ICascadeDelete)
                {
                    (entity as ICascadeDelete).OnDelete();
                }

                if (typeof(IDeleteInfo<TUser, TUserKey>).IsAssignableFrom(typeof(TEntity)))
                {
                    AddDeleteInfo<TEntity>(entity);
                }
                else
                {
                    _dbContext.Set<TEntity>().Remove(entity);
                }
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async virtual Task<int> DeleteAsync<TEntity>(params Expression<Func<TEntity, bool>>[] predicates)
            where TEntity : class
        {
            var entities = await Filter<TEntity>(predicates).ToArrayAsync();

            foreach (var entity in entities)
            {
                AddDeleteLog<TEntity>(entity);

                if (entity is ICascadeDelete)
                {
                    (entity as ICascadeDelete).OnDelete();
                }

                if (typeof(IDeleteInfo<TUser, TUserKey>).IsAssignableFrom(typeof(TEntity)))
                {
                    AddDeleteInfo<TEntity>(entity);
                }
                else
                {
                    _dbContext.Set<TEntity>().Remove(entity);
                }
            }
            return await _dbContext.SaveChangesAsync();
        }

        public virtual int Update<TEntity>(params TEntity[] entities)
            where TEntity : class
        {
            foreach (var entity in entities)
            {
                var entry = _dbContext.Entry(entity);

                AddUpdateLog<TEntity>(entity);
                AddUpdateInfo<TEntity>(entity);
            }

            return _dbContext.SaveChanges();
        }

        public async virtual Task<int> UpdateAsync<TEntity>(params TEntity[] entities)
            where TEntity : class
        {
            foreach (var entity in entities)
            {
                var entry = _dbContext.Entry(entity);

                AddUpdateLog<TEntity>(entity);
                AddUpdateInfo<TEntity>(entity);
            }

            return await _dbContext.SaveChangesAsync();
        }

        #endregion

        public virtual void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async virtual Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public virtual int ExecuteNonQuery(string sql, params object[] sqlParams)
        {
            return _dbContext.Database.ExecuteSqlCommand(sql, sqlParams);
        }

        public TDbContext GetDbContext<TDbContext>()
            where TDbContext : class
        {
            return this._dbContext as TDbContext;
        }

        public virtual IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        public virtual IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return _dbContext.Database.BeginTransaction(isolationLevel);
        }

        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
                _dbContext = null;
            }
        }

        #region Helpers
        private void AddCreateInfo<TEntity>(TEntity entity)
        {
            //ADD CREATE INFO IF APPLICABLE
            if (entity is ICreateInfo<TUser, TUserKey>)
            {
                (entity as ICreateInfo<TUser, TUserKey>).CreatedDate = DateTime.Now;
                (entity as ICreateInfo<TUser, TUserKey>).CreatedUserId = _currentUser.UserId;
            }
        }

        private void AddUpdateInfo<TEntity>(TEntity entity)
        {
            //ADD UPDATE INFO IF APPLICABLE
            if (entity is IUpdateInfo<TUser, TUserKey>)
            {
                (entity as IUpdateInfo<TUser, TUserKey>).LastUpdatedDate = DateTime.Now;
                (entity as IUpdateInfo<TUser, TUserKey>).LastUpdatedUserId = _currentUser.UserId;
            }
        }

        private void AddDeleteInfo<TEntity>(TEntity entity)
        {
            //ADD DELETE INFO IF APPLICABLE
            if (entity is IDeleteInfo<TUser, TUserKey>)
            {
                (entity as IDeleteInfo<TUser, TUserKey>).DeletedDate = DateTime.Now;
                (entity as IDeleteInfo<TUser, TUserKey>).DeletedUserId = _currentUser.UserId;
            }
        }

        private void AddCreateLog<TEntity>(TEntity entity)
            where TEntity : class
        {
            _logger.Log(LogLevel.Information,
            default(EventId),
            new ExtraPropertyLogger("User {username} added new {entity} {identifier}", _currentUser.Username, typeof(TEntity).Name, entity.ToString())
                .AddProp("data", JsonConvert.SerializeObject(entity, _jsonSettings)),
            null,
            ExtraPropertyLogger.Formatter);
        }

        private void AddUpdateLog<TEntity>(TEntity entity)
            where TEntity : class
        {
            _logger.Log(LogLevel.Information,
            default(EventId),
            new ExtraPropertyLogger("User {username} updated {entity} \"{identifier}\"", _currentUser.Username, typeof(TEntity).Name, entity.ToString())
                .AddProp("data", JsonConvert.SerializeObject(entity, _jsonSettings)),
            null,
            ExtraPropertyLogger.Formatter);
        }

        private void AddDeleteLog<TEntity>(TEntity entity)
            where TEntity : class
        {
            _logger.Log(LogLevel.Information,
            default(EventId),
            new ExtraPropertyLogger("User {username} deleted {entity} \"{identifier}\"", _currentUser.Username, typeof(TEntity).Name, entity.ToString())
                .AddProp("data", JsonConvert.SerializeObject(entity, _jsonSettings)),
            null,
            ExtraPropertyLogger.Formatter);
        }

        #endregion
    }

}
