using FinanceManager.Exceptions;
using FinanceManager.Interfaces;
using FinanceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Repos;

public class StorageRepo(Context context) : IRepo<Storage>
{
    public async Task AddAsync(Storage entity)
        => await context.Storages.AddAsync(entity);

    public void Remove(Storage entity)
        => context.Storages.Remove(entity);

    public async Task SaveAsync()
        => await context.SaveChangesAsync();

    public async Task<Storage> GetByIdAsync(int id)
        => await context.Storages
            .Include(t=> t.Transactions)
            .FirstOrDefaultAsync(x=> x.Id == id) ?? throw new NotFoundException();

    public async Task<List<Storage>> GetAllAsync()
        => await context.Storages.ToListAsync();

    public void Update(Storage entity)
        => context.Update(entity);
    
    public async Task<List<Transaction>> GetTransactionsAsyncByOwnId(int id)
        => await context.Transactions
            .Include(t => t.Category)
            .Include(t => t.Storage)
            .Where(x => x.CategoryId == id)
            .ToListAsync();

    public Task<List<Transaction>> GetDailyReport(DateTime date)
    {
        throw new Exception();
    }

    public Task<List<Transaction>> GetPeriodReport(DateTime startDate, DateTime endDate)
    {
        throw new Exception();
    }
}