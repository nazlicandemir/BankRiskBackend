
using BankRiskTracking.Entities.Entities;
using Microsoft.EntityFrameworkCore;


namespace BankRiskTracking.DataAccess.Repository
{
    public class GenericRepository<TEntity>(DatabaseConnection dbContext) : IGenericRepository<TEntity>
        where TEntity : class
    {
        

        protected readonly DatabaseConnection DbContext = dbContext;
        private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            // Senkron Create'de SaveChanges var; burada istersen SaveChangesAsync ekleyebilirsin.
            // Projende tutarlılık için burada şimdilik kaydetmiyorum.
        }

        public void Create(TEntity entity)
        {
            _dbSet.Add(entity);
            DbContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
             
            if (_dbSet.Entry(entity).State == EntityState.Detached) // ✅ EKLE
                _dbSet.Attach(entity);

            _dbSet.Remove(entity);
            DbContext.SaveChanges();

        }

        public void DeleteRange(List<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            DbContext.SaveChanges();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            // Gerçekten async oldu
            return await _dbSet.FindAsync(id);
        }

        public IQueryable<TEntity> Queryable()
        {
            return _dbSet.AsQueryable();
        }

        // SENKRON UPDATE (mevcudun)
        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
            DbContext.SaveChanges();
        }

        // ASENKRON UPDATE (ekledik)
        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await DbContext.SaveChangesAsync();
        }

        public int SaveChange()
        {
            return DbContext.SaveChanges();
        }
    }
}
