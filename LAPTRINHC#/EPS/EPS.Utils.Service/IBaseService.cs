using EPS.Utils.Repository;
using EPS.Utils.Repository.Audit;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Utils.Service
{
    public interface IBaseService : IDisposable
    {
        IQueryable<TDto> All<TEntity, TDto>()
            where TEntity : class;

        IQueryable<TDto> All<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping)
            where TEntity : class;

        IQueryable<TDto> FromSqlRaw<TEntity, TDto>(string sql, params object[] parameters)
            where TEntity : class;

        IQueryable<TDto> FromSqlRaw<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, string sql, params object[] parameters)
            where TEntity : class;

        int Count<TEntity, TDto>(params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class;

        int Count<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class;

        Task<int> CountAsync<TEntity, TDto>(params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class;

        Task<int> CountAsync<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class;

        TDto Find<TEntity, TDto>(object id)
            where TEntity : class;

        TDto Find<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, object id)
            where TEntity : class;

        TDto Find<TEntity, TDto>(params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class;

        TDto Find<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class;

        Task<TDto> FindAsync<TEntity, TDto>(object id)
            where TEntity : class;

        Task<TDto> FindAsync<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, object id)
            where TEntity : class;

        Task<TDto> FindAsync<TEntity, TDto>(params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class;

        Task<TDto> FindAsync<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class;

        IQueryable<TDto> Filter<TEntity, TDto>(params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class;

        IQueryable<TDto> Filter<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class;

        PagingResult<TDto> FilterPaged<TEntity, TDto>(PagingParams<TDto> pagingParams, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class;

        PagingResult<TDto> FilterPaged<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, PagingParams<TDto> pagingParams, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class;

        Task<PagingResult<TDto>> FilterPagedAsync<TEntity, TDto>(PagingParams<TDto> pagingParams, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class;

        Task<PagingResult<TDto>> FilterPagedAsync<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, PagingParams<TDto> pagingParams, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class;

        bool Contain<TEntity, TDto>(params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class;

        bool Contain<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class;

        Task<bool> ContainAsync<TEntity, TDto>(params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class;

        Task<bool> ContainAsync<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class;

        void Create<TEntity, TDto>(params TDto[] dtos)
            where TEntity : class
            where TDto : class;

        void Create<TEntity, TDto>(Func<TDto, TEntity> mapping, params TDto[] dtos)
            where TEntity : class
            where TDto : class;

        Task CreateAsync<TEntity, TDto>(params TDto[] dtos)
            where TEntity : class
            where TDto : class;

        Task CreateAsync<TEntity, TDto>(Func<TDto, TEntity> mapping, params TDto[] dtos)
            where TEntity : class
            where TDto : class;

        int Delete<TEntity, TKey>(TKey id)
            where TEntity : class;

        int Delete<TEntity, TKey>(TKey[] ids)
            where TEntity : class;

        int Delete<TEntity, TDto>(params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class;

        int Delete<TEntity, TDto>(Func<TDto, TEntity> mapping, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class;

        Task<int> DeleteAsync<TEntity, TKey>(TKey id)
            where TEntity : class;

        Task<int> DeleteAsync<TEntity, TKey>(TKey[] ids)
            where TEntity : class;

        Task<int> DeleteAsync<TEntity, TDto>(params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class;

        Task<int> DeleteAsync<TEntity, TDto>(Func<TDto, TEntity> mapping, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class;

        int Update<TEntity, TDto>(object id, TDto dto)
            where TEntity : class
            where TDto : class;

        int Update<TEntity, TDto>(Action<TDto, TEntity> mapping, object id, TDto dto)
            where TEntity : class
            where TDto : class;

        Task<int> UpdateAsync<TEntity, TDto>(object id, TDto dto)
            where TEntity : class
            where TDto : class;

        Task<int> UpdateAsync<TEntity, TDto>(Action<TDto, TEntity> mapping, object id, TDto dto)
            where TEntity : class
            where TDto : class;

        int Update<TEntity, TDto, TKey>(params TDto[] dtos)
            where TEntity : class
            where TDto : class, IIdentifier<TKey>;

        int Update<TEntity, TDto, TKey>(Action<TDto, TEntity> mapping, params TDto[] dtos)
            where TEntity : class
            where TDto : class, IIdentifier<TKey>;

        Task<int> UpdateAsync<TEntity, TDto, TKey>(params TDto[] dtos)
            where TEntity : class
            where TDto : class, IIdentifier<TKey>;

        Task<int> UpdateAsync<TEntity, TDto, TKey>(Action<TDto, TEntity> mapping, params TDto[] dtos)
            where TEntity : class
            where TDto : class, IIdentifier<TKey>;

        int ExecuteNonQuery(string sql, params object[] sqlParams);

        TContext GetDbContext<TContext>() where TContext : class;

        IDbContextTransaction BeginTransaction();

        IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel);
    }

}
