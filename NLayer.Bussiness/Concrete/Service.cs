using Microsoft.EntityFrameworkCore;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.DataAccess.Core.Abstract;
using NLayer.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class Service<T> : IEntityRepository<T> where T : class
    {
        private readonly IEntityRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;
        public Service(IEntityRepository<T> repository, IUnitOfWork unitOfWork = null)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        

        public async Task<T> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            return entities;
        }

        public Task AddRangeAsync<T1>(IEnumerable<T1> entities) where T1 : class
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync().ToListAsync(); //dbden datayı aldım geri döndüm
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(T entity)
        {
            _repository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repository.Where(expression);
        }

        Task IEntityRepository<T>.AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        IQueryable<T> IEntityRepository<T>.GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
