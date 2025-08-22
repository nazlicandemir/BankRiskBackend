namespace BankRiskTracking.DataAccess.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        #region CRUD
        void Create(TEntity entity);
        IQueryable<TEntity> GetAll();

        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);   // <-- void yerine Task
        void Update(TEntity entity);        // <-- Senkron sürümü de kalsın (istersen)
        void Delete(TEntity entity);
       

        void DeleteRange(List<TEntity> entities);
        #endregion

        Task<TEntity?> GetByIdAsync(int id); // <-- FindAsync null dönebilir

        IQueryable<TEntity> Queryable();

        int SaveChange();
    }
}
