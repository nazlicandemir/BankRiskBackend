using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankRiskTracking.DataAccess.Repository
{
   public interface IGenericRepository<TEntity>where TEntity : class
    {
        #region CRUD
        void Create (TEntity entity);
        IQueryable<TEntity> GetAll();

        public Task AddAsync(TEntity entity);
        void UpdateAsync(TEntity entity);
        void Delete(TEntity entity);

        #endregion

        Task<TEntity> GetByIdAsync(int id);

        void DeleteRange(List<TEntity> entities);
        IQueryable<TEntity> Queryable();
    }
}
