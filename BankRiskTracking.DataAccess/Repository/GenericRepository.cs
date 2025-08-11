using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankRiskTracking.DataAccess.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly DatabaseConnection _dbContext;
        private readonly DbSet<TEntity> _dbSet;


        public GenericRepository(DatabaseConnection dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();

        }


        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Create(TEntity entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {

            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

        public void DeleteRange(List<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            _dbContext.SaveChanges();
        }

        public IQueryable<TEntity> GetAll()
        {
            
           return _dbSet.AsNoTracking().AsQueryable();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return  _dbSet.Find(id);
        }

        public IQueryable<TEntity> Queryable()
        {
            return _dbSet.AsQueryable();
        }

        public void UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
