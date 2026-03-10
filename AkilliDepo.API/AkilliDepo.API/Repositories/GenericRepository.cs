using AkilliDepo.API.Data;
using AkilliDepo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace AkilliDepo.API.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAll(string companyId) => _dbSet.Where(x => x.CompanyId == companyId);
        public async Task<T?> GetByIdAsync(int id, string companyId) => await _dbSet.FirstOrDefaultAsync(x => x.Id == id && x.CompanyId == companyId);
        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        public void Update(T entity) => _dbSet.Update(entity);
        public void SoftDelete(T entity) { entity.IsDeleted = true; _dbSet.Update(entity); }

        // HATA VEREN DÖNÜŞ TÜRÜ BURADA DÜZELTİLDİ:
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}