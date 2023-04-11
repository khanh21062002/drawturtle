using EPS.Utils.Repository.Audit;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Utils.Repository
{
    public interface IRepository : IDisposable
    {
        IQueryable<TEntity> All<TEntity>()
            where TEntity : class;

        IQueryable<TEntity> FromSqlRaw<TEntity>(string sql, params object[] parameters)
            where TEntity : class;

        int Count<TEntity>(params Expression<Func<TEntity, bool>>[] predicates)
            where TEntity : class;

        Task<int> CountAsync<TEntity>(params Expression<Func<TEntity, bool>>[] predicates)
            where TEntity : class;

        TEntity Find<TEntity>(params Expression<Func<TEntity, bool>>[] predicates)
            where TEntity : class;

        TEntity Find<TEntity>(object id)
            where TEntity : class;

        Task<TEntity> FindAsync<TEntity>(object id)
            where TEntity : class;

        Task<TEntity> FindAsync<TEntity>(params Expression<Func<TEntity, bool>>[] predicates)
            where TEntity : class;

        IQueryable<TEntity> Filter<TEntity>(params Expression<Func<TEntity, bool>>[] predicates)
            where TEntity : class;

        bool Contain<TEntity>(params Expression<Func<TEntity, bool>>[] predicates)
            where TEntity : class;

        Task<bool> ContainAsync<TEntity>(params Expression<Func<TEntity, bool>>[] predicates)
            where TEntity : class;

        void Create<TEntity>(params TEntity[] entities)
            where TEntity : class;

        Task CreateAsync<TEntity>(params TEntity[] entities)
            where TEntity : class;

        int Delete<TEntity, TKey>(TKey id)
            where TEntity : class;

        int Delete<TEntity, TKey>(TKey[] ids)
            where TEntity : class;

        int Delete<TEntity>(TEntity entity)
            where TEntity : class;

        int Delete<TEntity>(TEntity[] entities)
            where TEntity : class;

        int Delete<TEntity>(params Expression<Func<TEntity, bool>>[] predicates)
            where TEntity : class;

        Task<int> DeleteAsync<TEntity, TKey>(TKey id)
            where TEntity : class;

        Task<int> DeleteAsync<TEntity, TKey>(TKey[] ids)
            where TEntity : class;

        Task<int> DeleteAsync<TEntity>(TEntity entity)
            where TEntity : class;

        Task<int> DeleteAsync<TEntity>(TEntity[] entity)
            where TEntity : class;

        Task<int> DeleteAsync<TEntity>(params Expression<Func<TEntity, bool>>[] predicates)
            where TEntity : class;

        int Update<TEntity>(params TEntity[] entities)
            where TEntity : class;

        Task<int> UpdateAsync<TEntity>(params TEntity[] entities)
            where TEntity : class;

        void SaveChanges();

        Task SaveChangesAsync();

        int ExecuteNonQuery(string sql, params object[] sqlParams);

        TContext GetDbContext<TContext>() where TContext : class;

        IDbContextTransaction BeginTransaction();

        IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel);
    }
}
