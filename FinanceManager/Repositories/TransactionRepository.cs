using FinanceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Repositories
{
    public class TransactionRepository : IRepositories<Transaction>
    {
        private readonly Context _context;

        private TransactionRepository() { }

        public TransactionRepository(Context context) => _context = context;

        public async Task<List<Transaction>> GetAllAsync()
        {
            return await _context.Transactions
                .Include(s=> s.Storage)
                .Include(c=> c.Category)
                .ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(int id)
        {
            return await _context.Transactions
                .Include(s => s.Storage)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> CreateAsync(Transaction transaction)
        {
            if(!IsValid(transaction))
            {
                return false;
            }

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditAsync(Transaction transactionData)
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(x => x.Id == transactionData.Id);

            if(transaction is null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(transactionData.Name) && transactionData.Name != transaction.Name)
            {
                transaction.Name = transactionData.Name;
            }

            if (transactionData.Date != transaction.Date)
            {
                transaction.Date = transactionData.Date;
            }

            if (transactionData.CategoryId != transaction.CategoryId)
            {
                transaction.CategoryId = transactionData.CategoryId;
            }

            if (transactionData.Price != transaction.Price)
            {
                transaction.Price = transactionData.Price;
            }

            if (transactionData.Description != null && transactionData.Description != transaction.Description)
            {
                transaction.Description = transactionData.Description;
            }

            if (transactionData.StorageId != transaction.StorageId)
            {
                transaction.StorageId = transactionData.StorageId;
            }

            try
            {
                _context.Update(transaction);
                await _context.SaveChangesAsync();
                return true;
            }

            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Update exception");
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(x=> x.Id == id);

            if(transaction is null)
            {
                return false;
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool IsValid(Transaction transaction)
        {
            if(_context.Transactions.FirstOrDefault(t=> t.Name == transaction.Name) != null)
            {
                return false;
            }

            return true;
        }
    }
}
