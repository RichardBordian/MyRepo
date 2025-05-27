using FinanceManager.Models;

namespace FinanceManager.Interfaces;

public interface IRepo<T>
{
    public Task AddAsync(T entity);

    public void Remove(T entity);

    public Task SaveAsync();

    public Task<T> GetByIdAsync(int id);

    public Task<List<T>> GetAllAsync();

    public void Update(T entity);
    
    public Task<List<Transaction>> GetTransactionsAsyncByOwnId(int id);

    public Task<List<Transaction>> GetDailyReport(DateTime date);
    
    public Task<List<Transaction>> GetPeriodReport(DateTime startDate, DateTime endDate);
}