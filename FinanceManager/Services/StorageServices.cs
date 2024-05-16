using FinanceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Services
{
    public class StorageServices
    {
        private readonly Context _context = new Context();

        public async Task<Storage?> GetStorageAsync(int? id)
        {
            if (id is null)
            {
                return null;
            }

            return await _context.Storages
                .Include(t=> t.Transactions)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Storage>> GetAllStoragesAsync()
        {
            return await _context.Storages
                .ToListAsync();
        }

        public async Task<bool> CreateStorageAsync(Storage storage)
        {
            if(!StorageIsValid(storage))
            {
                return false;
            }

            await _context.Storages.AddAsync(storage);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditStorageAsync(int? id, Storage storage)
        {
            if(id!= storage.Id) 
            {
                return false;
            }

            if(!_context.Storages.Any(x=> x.Id  == id)) 
            {
                return false;
            }

            try
            {
                _context.Update(storage);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) 
            {
                throw new Exception("Update exception");
            }

            return true;
        }

        public async Task<bool> DeleteStorageAsync(int? id)
        {
            if(id is null)
            {
                return false;
            }

            var storage = await _context.Storages
                .Include(t => t.Transactions)
                .FirstOrDefaultAsync(s => s.Id == id);

            if(storage == null)
            {
                return false;
            }

            _context.Storages.Remove(storage);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool StorageIsValid(Storage storage)
        {
            if(_context.Storages.FirstOrDefault(s=> s.Name == storage.Name) != null & storage.Name == null)
            {
                return false;
            }
            return true;
        }
    }
}
