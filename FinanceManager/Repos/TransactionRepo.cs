using FinanceManager.Exceptions;
using FinanceManager.Interfaces;
using FinanceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Repos;

public class TransactionRepo(Context context) : IRepo<Transaction>
{
    public async Task AddAsync(Transaction entity)
        => await context.Transactions.AddAsync(entity);

    public void Remove(Transaction entity)
        => context.Transactions.Remove(entity);

    public async Task SaveAsync()
        => await context.SaveChangesAsync();

    public async Task<Transaction> GetByIdAsync(int id)
        => await context.Transactions.FindAsync(id) ?? throw new NotFoundException();

    public async Task<List<Transaction>> GetAllAsync()
        => await context.Transactions
            .Include(t => t.Category)
            .Include(t => t.Storage)
            .ToListAsync();

    public void Update(Transaction entity)
        => context.Update(entity);
    
    public async Task<List<Transaction>> GetDailyReport(DateTime date)
        => await context.Transactions
            .Include(t => t.Category)
            .Include(t => t.Storage)
            .Where(x => x.Date.Date == date.Date)
            .ToListAsync();

    public async Task<List<Transaction>> GetPeriodReport(DateTime startDate, DateTime endDate)
        => await context.Transactions
            .Include(t => t.Category)
            .Include(t => t.Storage)
            .Where(x => x.Date.Date >= startDate && x.Date.Date <= endDate)
            .ToListAsync();
    
    public Task<List<Transaction>> GetTransactionsAsyncByOwnId(int id)
    {
        throw new Exception();
    }
}