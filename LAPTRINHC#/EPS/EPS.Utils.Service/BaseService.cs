using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPS.Utils.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq.Expressions;
using EPS.Utils.Common;
using System.Reflection;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Dynamic.Core;
using EPS.Utils.Repository.Audit;
using Microsoft.EntityFrameworkCore;

namespace EPS.Utils.Service
{
    public class BaseService : IBaseService
    {
        protected IRepository _repository;
        protected IMapper _mapper;

        public BaseService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #region IBaseService Members

        public virtual IQueryable<TDto> All<TEntity, TDto>()
            where TEntity : class
        {
            return _repository.All<TEntity>().ProjectTo<TDto>(_mapper.ConfigurationProvider);
        }

        public virtual IQueryable<TDto> All<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping)
            where TEntity : class
        {
            return _repository.All<TEntity>().Select(mapping);
        }

        public virtual IQueryable<TDto> FromSqlRaw<TEntity, TDto>(string sql, params object[] parameters)
            where TEntity : class
        {
            return _repository.FromSqlRaw<TEntity>(sql, parameters).ProjectTo<TDto>(_mapper.ConfigurationProvider);
        }

        public virtual IQueryable<TDto> FromSqlRaw<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, string sql, params object[] parameters)
            where TEntity : class
        {
            return _repository.FromSqlRaw<TEntity>(sql, parameters).Select(mapping);
        }

        public virtual int Count<TEntity, TDto>(params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            return _repository.All<TEntity>().ProjectTo<TDto>(_mapper.ConfigurationProvider).WhereMany(predicates).Count();
        }

        public virtual int Count<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            return _repository.All<TEntity>().Select(mapping).WhereMany(predicates).Count();
        }

        public virtual async Task<int> CountAsync<TEntity, TDto>(params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            return await _repository.All<TEntity>().ProjectTo<TDto>(_mapper.ConfigurationProvider).WhereMany(predicates).CountAsync();
        }

        public virtual async Task<int> CountAsync<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            return await _repository.All<TEntity>().Select(mapping).WhereMany(predicates).CountAsync();
        }

        public virtual TDto Find<TEntity, TDto>(object id)
            where TEntity : class
        {
            return _mapper.Map<TEntity, TDto>(_repository.Find<TEntity>(id));
        }

        public virtual TDto Find<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, object id)
            where TEntity : class
        {
            return mapping.Compile().Invoke(_repository.Find<TEntity>(id));
        }

        public virtual TDto Find<TEntity, TDto>(params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            return _repository.All<TEntity>().ProjectTo<TDto>(_mapper.ConfigurationProvider).WhereMany(predicates).FirstOrDefault();
        }

        public virtual TDto Find<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            return _repository.All<TEntity>().Select(mapping).WhereMany(predicates).FirstOrDefault();
        }

        public virtual async Task<TDto> FindAsync<TEntity, TDto>(object id)
            where TEntity : class
        {
            var entity = await _repository.FindAsync<TEntity>(id);
            return _mapper.Map<TEntity, TDto>(entity);
        }

        public virtual async Task<TDto> FindAsync<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, object id)
            where TEntity : class
        {
            return mapping.Compile().Invoke(await _repository.FindAsync<TEntity>(id));
        }

        public virtual async Task<TDto> FindAsync<TEntity, TDto>(params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            return await _repository.All<TEntity>().ProjectTo<TDto>(_mapper.ConfigurationProvider).WhereMany(predicates).FirstOrDefaultAsync();
        }

        public virtual async Task<TDto> FindAsync<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            return await _repository.All<TEntity>().Select(mapping).WhereMany(predicates).FirstOrDefaultAsync();
        }

        public virtual IQueryable<TDto> Filter<TEntity, TDto>(params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            return _repository.Filter<TEntity>().ProjectTo<TDto>(_mapper.ConfigurationProvider).WhereMany(predicates);
        }

        public virtual IQueryable<TDto> Filter<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            return _repository.Filter<TEntity>().Select(mapping).WhereMany(predicates);
        }

        public virtual PagingResult<TDto> FilterPaged<TEntity, TDto>(PagingParams<TDto> pagingParams, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            if (pagingParams == null)
            {
                throw new ArgumentNullException("pagingParams");
            }

            var result = new PagingResult<TDto>() { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };

            IQueryable<TDto> query = _repository.Filter<TEntity>().ProjectTo<TDto>(_mapper.ConfigurationProvider);

            var pagingPredicates = pagingParams.GetPredicates();
            if (pagingPredicates != null && pagingPredicates.Any())
            {
                query = query.WhereMany(pagingPredicates);
            }

            if (predicates != null && predicates.Any())
            {
                query = query.WhereMany(predicates);
            }

            result.TotalRows = query.Count();

            // Ordering
            if (pagingParams.SortExpression != null)
            {
                query = query.OrderBy(pagingParams.SortExpression);

                // Skipping only work after ordering
                if (pagingParams.StartingIndex > 0)
                {
                    query = query.Skip(pagingParams.StartingIndex);
                }
            }

            if (pagingParams.ItemsPerPage != -1 && pagingParams.ItemsPerPage <= 0)
            {
                pagingParams.ItemsPerPage = 100;
            }

            if (pagingParams.ItemsPerPage > 0)
            {
                query = query.Take(pagingParams.ItemsPerPage);
            }

            result.Data = query.ToList();

            return result;
        }

        public virtual PagingResult<TDto> FilterPaged<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, PagingParams<TDto> pagingParams, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            if (pagingParams == null)
            {
                throw new ArgumentNullException("pagingParams");
            }

            var result = new PagingResult<TDto>() { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };

            IQueryable<TDto> query = _repository.Filter<TEntity>().Select(mapping);

            var pagingPredicates = pagingParams.GetPredicates();
            if (pagingPredicates != null && pagingPredicates.Any())
            {
                query = query.WhereMany(pagingPredicates);
            }

            if (predicates != null && predicates.Any())
            {
                query = query.WhereMany(predicates);
            }

            result.TotalRows = query.Count();

            // Ordering
            if (pagingParams.SortExpression != null)
            {
                query = query.OrderBy(pagingParams.SortExpression);

                // Skipping only work after ordering
                if (pagingParams.StartingIndex > 0)
                {
                    query = query.Skip(pagingParams.StartingIndex);
                }
            }

            if (pagingParams.ItemsPerPage != -1 && pagingParams.ItemsPerPage <= 0)
            {
                pagingParams.ItemsPerPage = 100;
            }

            if (pagingParams.ItemsPerPage > 0)
            {
                query = query.Take(pagingParams.ItemsPerPage);
            }

            result.Data = query.ToList();

            return result;
        }

        public async virtual Task<PagingResult<TDto>> FilterPagedAsync<TEntity, TDto>(PagingParams<TDto> pagingParams, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            if (pagingParams == null)
            {
                throw new ArgumentNullException("pagingParams");
            }

            var result = new PagingResult<TDto>() { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };

            IQueryable<TDto> query = _repository.Filter<TEntity>().ProjectTo<TDto>(_mapper.ConfigurationProvider);

            var pagingPredicates = pagingParams.GetPredicates();
            if (pagingPredicates != null && pagingPredicates.Any())
            {
                query = query.WhereMany(pagingPredicates);
            }

            if (predicates != null && predicates.Any())
            {
                query = query.WhereMany(predicates);
            }

            result.TotalRows = await query.CountAsync();

            // Ordering
            if (pagingParams.SortExpression != null)
            {
                query = query.OrderBy(pagingParams.SortExpression);

                // Skipping only work after ordering
                if (pagingParams.StartingIndex > 0)
                {
                    query = query.Skip(pagingParams.StartingIndex);
                }
            }

            if (pagingParams.ItemsPerPage != -1 && pagingParams.ItemsPerPage <= 0)
            {
                pagingParams.ItemsPerPage = 100;
            }

            if (pagingParams.ItemsPerPage > 0)
            {
                query = query.Take(pagingParams.ItemsPerPage);
            }

            result.Data = await query.ToListAsync();

            return result;
        }

        public async virtual Task<PagingResult<TDto>> FilterPagedAsync<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, PagingParams<TDto> pagingParams, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            if (pagingParams == null)
            {
                throw new ArgumentNullException("pagingParams");
            }

            var result = new PagingResult<TDto>() { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };

            IQueryable<TDto> query = _repository.Filter<TEntity>().Select(mapping);

            var pagingPredicates = pagingParams.GetPredicates();
            if (pagingPredicates != null && pagingPredicates.Any())
            {
                query = query.WhereMany(pagingPredicates);
            }

            if (predicates != null && predicates.Any())
            {
                query = query.WhereMany(predicates);
            }

            result.TotalRows = await query.CountAsync();

            // Ordering
            if (pagingParams.SortExpression != null)
            {
                query = query.OrderBy(pagingParams.SortExpression);

                // Skipping only work after ordering
                if (pagingParams.StartingIndex > 0)
                {
                    query = query.Skip(pagingParams.StartingIndex);
                }
            }

            if (pagingParams.ItemsPerPage != -1 && pagingParams.ItemsPerPage <= 0)
            {
                pagingParams.ItemsPerPage = 100;
            }

            if (pagingParams.ItemsPerPage > 0)
            {
                query = query.Take(pagingParams.ItemsPerPage);
            }

            result.Data = await query.ToListAsync();

            return result;
        }

        public virtual bool Contain<TEntity, TDto>(params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            return _repository.All<TEntity>().ProjectTo<TDto>(_mapper.ConfigurationProvider).WhereMany(predicates).Any();
        }

        public virtual bool Contain<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            return _repository.All<TEntity>().Select(mapping).WhereMany(predicates).Any();
        }

        public virtual async Task<bool> ContainAsync<TEntity, TDto>(params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            return await _repository.All<TEntity>().ProjectTo<TDto>(_mapper.ConfigurationProvider).WhereMany(predicates).AnyAsync();
        }

        public virtual async Task<bool> ContainAsync<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            return await _repository.All<TEntity>().Select(mapping).WhereMany(predicates).AnyAsync();
        }

        public virtual void Create<TEntity, TDto>(params TDto[] dtos)
            where TEntity : class
            where TDto : class
        {
            var entities = _mapper.Map<TDto[], TEntity[]>(dtos);

            _repository.Create<TEntity>(entities);

            if (_mapper.ConfigurationProvider.FindTypeMapFor<TEntity, TDto>() != null)
            {
                for (var i = 0; i < entities.Length; i++)
                {
                    _mapper.Map(entities[i], dtos[i]);
                }
            }
        }

        public virtual void Create<TEntity, TDto>(Func<TDto, TEntity> mapping, params TDto[] dtos)
            where TEntity : class
            where TDto : class
        {
            var entities = dtos.Select(mapping).ToArray();

            _repository.Create<TEntity>(entities);

            if (_mapper.ConfigurationProvider.FindTypeMapFor<TEntity, TDto>() != null)
            {
                for (var i = 0; i < entities.Length; i++)
                {
                    _mapper.Map(entities[i], dtos[i]);
                }
            }
        }

        public virtual async Task CreateAsync<TEntity, TDto>(params TDto[] dtos)
            where TEntity : class
            where TDto : class
        {
            var entities = _mapper.Map<TDto[], TEntity[]>(dtos);

            await _repository.CreateAsync<TEntity>(entities);

            if (_mapper.ConfigurationProvider.FindTypeMapFor<TEntity, TDto>() != null)
            {
                for (var i = 0; i < entities.Length; i++)
                {
                    _mapper.Map(entities[i], dtos[i]);
                }
            }
        }

        public virtual async Task CreateAsync<TEntity, TDto>(Func<TDto, TEntity> mapping, params TDto[] dtos)
            where TEntity : class
            where TDto : class
        {
            var entities = dtos.Select(mapping).ToArray();

            await _repository.CreateAsync<TEntity>(entities);

            if (_mapper.ConfigurationProvider.FindTypeMapFor<TEntity, TDto>() != null)
            {
                for (var i = 0; i < entities.Length; i++)
                {
                    _mapper.Map(entities[i], dtos[i]);
                }
            }
        }

        public virtual int Delete<TEntity, TKey>(TKey id)
            where TEntity : class
        {
            return _repository.Delete<TEntity, TKey>(id);
        }

        public virtual int Delete<TEntity, TKey>(TKey[] ids)
            where TEntity : class
        {
            return _repository.Delete<TEntity, TKey>(ids);
        }

        public virtual int Delete<TEntity, TDto>(params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            var dtos = Filter<TEntity, TDto>(predicates).ToList();
            return _repository.Delete<TEntity>(_mapper.Map<List<TDto>, List<TEntity>>(dtos).ToArray());
        }

        public virtual int Delete<TEntity, TDto>(Func<TDto, TEntity> mapping, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            var dtos = Filter<TEntity, TDto>(predicates).ToList();
            return _repository.Delete<TEntity>(dtos.Select(mapping).ToArray());
        }

        public virtual async Task<int> DeleteAsync<TEntity, TKey>(TKey id)
            where TEntity : class
        {
            return await _repository.DeleteAsync<TEntity, TKey>(id);
        }

        public virtual async Task<int> DeleteAsync<TEntity, TKey>(TKey[] ids)
            where TEntity : class
        {
            return await _repository.DeleteAsync<TEntity, TKey>(ids);
        }

        public virtual async Task<int> DeleteAsync<TEntity, TDto>(params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            var dtos = await Filter<TEntity, TDto>(predicates).ToListAsync();
            return await _repository.DeleteAsync<TEntity>(_mapper.Map<List<TDto>, List<TEntity>>(dtos).ToArray());
        }

        public virtual async Task<int> DeleteAsync<TEntity, TDto>(Func<TDto, TEntity> mapping, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            var dtos = await Filter<TEntity, TDto>(predicates).ToListAsync();
            return await _repository.DeleteAsync<TEntity>(dtos.Select(mapping).ToArray());
        }

        public virtual int Update<TEntity, TDto>(object id, TDto dto)
            where TEntity : class
            where TDto : class
        {
            var entity = _repository.Find<TEntity>(id);
            _mapper.Map<TDto, TEntity>(dto, entity);

            return _repository.Update<TEntity>(entity);
        }

        public virtual int Update<TEntity, TDto>(Action<TDto, TEntity> mapping, object id, TDto dto)
            where TEntity : class
            where TDto : class
        {
            var entity = _repository.Find<TEntity>(id);
            mapping(dto, entity);

            return _repository.Update<TEntity>(entity);
        }

        public virtual async Task<int> UpdateAsync<TEntity, TDto>(object id, TDto dto)
            where TEntity : class
            where TDto : class
        {
            var entity = await _repository.FindAsync<TEntity>(id);
            _mapper.Map<TDto, TEntity>(dto, entity);

            return await _repository.UpdateAsync<TEntity>(entity);
        }

        public virtual async Task<int> UpdateAsync<TEntity, TDto>(Action<TDto, TEntity> mapping, object id, TDto dto)
            where TEntity : class
            where TDto : class
        {
            var entity = await _repository.FindAsync<TEntity>(id);
            mapping(dto, entity);

            return await _repository.UpdateAsync<TEntity>(entity);
        }

        public virtual int Update<TEntity, TDto, TKey>(params TDto[] dtos)
            where TEntity : class
            where TDto : class, IIdentifier<TKey>
        {
            var entities = new List<TEntity>();
            foreach (var dto in dtos)
            {
                var entity = _repository.Find<TEntity>(dto.Id);
                _mapper.Map<TDto, TEntity>(dto, entity);

                entities.Add(entity);
            }

            return _repository.Update<TEntity>(entities.ToArray());
        }

        public virtual int Update<TEntity, TDto, TKey>(Action<TDto, TEntity> mapping, params TDto[] dtos)
            where TEntity : class
            where TDto : class, IIdentifier<TKey>
        {
            var entities = new List<TEntity>();
            foreach (var dto in dtos)
            {
                var entity = _repository.Find<TEntity>(dto.Id);
                mapping(dto, entity);

                entities.Add(entity);
            }

            return _repository.Update<TEntity>(entities.ToArray());
        }

        public virtual async Task<int> UpdateAsync<TEntity, TDto, TKey>(params TDto[] dtos)
            where TEntity : class
            where TDto : class, IIdentifier<TKey>
        {
            var entities = new List<TEntity>();
            foreach (var dto in dtos)
            {
                var entity = await _repository.FindAsync<TEntity>(dto.Id);
                _mapper.Map<TDto, TEntity>(dto, entity);

                entities.Add(entity);
            }

            return await _repository.UpdateAsync<TEntity>(entities.ToArray());
        }

        public virtual async Task<int> UpdateAsync<TEntity, TDto, TKey>(Action<TDto, TEntity> mapping, params TDto[] dtos)
            where TEntity : class
            where TDto : class, IIdentifier<TKey>
        {
            var entities = new List<TEntity>();
            foreach (var dto in dtos)
            {
                var entity = await _repository.FindAsync<TEntity>(dto.Id);
                mapping(dto, entity);

                entities.Add(entity);
            }

            return await _repository.UpdateAsync<TEntity>(entities.ToArray());
        }

        public virtual int ExecuteNonQuery(string sql, params object[] sqlParams)
        {
            return _repository.ExecuteNonQuery(sql, sqlParams);
        }

        public virtual TContext GetDbContext<TContext>() where TContext : class
        {
            return _repository.GetDbContext<TContext>();
        }

        public virtual IDbContextTransaction BeginTransaction()
        {
            return _repository.BeginTransaction();
        }

        public virtual IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return _repository.BeginTransaction(isolationLevel);
        }

        #endregion

        #region IDisposable Members

        public virtual void Dispose()
        {
            if (_repository != null)
            {
                _repository.Dispose();
                _repository = null;
            }
        }

        #endregion

    }

}
