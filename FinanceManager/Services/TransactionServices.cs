using FinanceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Services
{
    public class TransactionServices
    {
        private readonly Context _context = new Context();

        public async Task<List<Transaction>> GetTransactionListAsync(int? id)
        {
            if (id is null)
            {
                return new List<Transaction>();
            }

            return await _context.Transactions
                .Include(c => c.Category)
                .Where(c => c.Id == id)
                .ToListAsync();
        }

        public async Task<Transaction?> GetTransactionAsync(int? id)
        {
            if (id is null)
            {
                return null;
            }

            return await _context.Transactions
                .FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<bool> CreateTransactionAsync(Transaction transaction)
        {
            if(transaction is null) 
            {
                return false;
            }

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions
                .ToListAsync();
        }

        public async Task<bool> EditTransactionAsync(int? id, Transaction transaction)
        {
            if(id != transaction.Id)
            {
                return false;
            }

            if(!_context.Transactions.Any(x=> x.Id == transaction.Id))
            {
                return false;
            }

            try
            {
                _context.Update(transaction);
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException) 
            {
                throw new Exception("Update exception");
            }

            return true;
        }

        public async Task<bool> DeleteTransactionAsync(int? id)
        {
            if( id is null)
            {
                return false;
            }

            var transaction = await _context.Transactions
                .Include(c=> c.Category)
                .FirstOrDefaultAsync(t=> t.Id == id);

            if(transaction is null)
            {
                return false;
            }

            _context.Remove(transaction);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
