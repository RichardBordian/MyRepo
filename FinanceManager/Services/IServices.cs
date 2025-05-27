namespace FinanceManager.Services
{
    public interface IServices<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetAsync(int id);
        Task<bool> CreateAsync(object Object);
        Task<bool> EditAsync(int id,object Object);
        Task<bool> DeleteAsync(int id);
        private bool IsValid() => true;
    }
}
