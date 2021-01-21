using ChurrasTrica.Data.Data;
using ChurrasTrica.Domain.Entities;
using ChurrasTrinca.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurrasTrica.Data.Repository
{
    public class BaseRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly DataContext _dataContext;

        public BaseRepository(DataContext dataContext) => _dataContext = dataContext;
        
        public async Task<bool> Delete(TEntity entity)
        {
            try
            {
                var result = _dataContext.Set<TEntity>();
                if(result == null)
                {
                    return false;
                }

                result.Remove(entity);
                await _dataContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            try
            {
                _dataContext.Set<TEntity>().Add(entity);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return entity;
        }

        public async Task<IEnumerable<TEntity>> SelectAll()
        {
            try
            {
               return await _dataContext.Set<TEntity>().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            try
            {
                _dataContext.Entry(entity).State = EntityState.Modified;
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Erro ao atualizar");
            }

            return entity;
        }
    }
}
