namespace FinanceManager.Repositories
{
    public interface IRepositories<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<bool> CreateAsync(T entity);
        Task<bool> EditAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
