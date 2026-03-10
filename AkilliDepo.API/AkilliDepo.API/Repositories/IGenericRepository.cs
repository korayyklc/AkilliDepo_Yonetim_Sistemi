using AkilliDepo.API.Entities;

namespace AkilliDepo.API.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(string companyId);
        Task<T?> GetByIdAsync(int id, string companyId);
        Task AddAsync(T entity);
        void Update(T entity);
        void SoftDelete(T entity);
        Task<int> SaveChangesAsync(); // Dönüş türü int olmalı
    }
}