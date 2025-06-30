namespace Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task<bool> AddAsync(T entity);
        public Task<bool> UpdateAsync(T entity);
        public Task<bool> DeleteAsync(string id);
        public Task<T> GetByIdAsync(string id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<bool> SaveChangesAsync();


    }
}
