namespace Application.Interfaces
{
    public interface IElasticRepository<T> where T : class
    {
        Task<bool> IndexAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(string id);
        Task<T?> GetByIdAsync(string id);
        Task<IEnumerable<T>> SearchAsync(string query);

    }
}
