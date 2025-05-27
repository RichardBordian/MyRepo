using FinanceManager.Exceptions;
using FinanceManager.Interfaces;
using FinanceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Repos;

public class CategoryRepo(Context context) : IRepo<Category>
{

    public async Task AddAsync(Category entity)
        => await context.Categories.AddAsync(entity);

    public void Remove(Category entity)
        => context.Categories.Remove(entity);

    public async Task SaveAsync()
        => await context.SaveChangesAsync();

    public async Task<Category> GetByIdAsync(int id)
        => await context.Categories
            .Include(t=> t.Transactions)
            .FirstOrDefaultAsync(x=> x.Id == id) ?? throw new NotFoundException();

    public async Task<List<Category>> GetAllAsync()
        => await context.Categories.ToListAsync();

    public void Update(Category entity)
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