using FinanceManager.common.DTO;
using FinanceManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FinanceManager.Repositories
{
    public class StorageRepository : IRepositories<Storage>
    {
        private readonly Context _context;

        private StorageRepository() { }

        public StorageRepository(Context context) => _context = context;

        public async Task<List<Storage>> GetAllAsync()
        {
            return await _context.Storages.ToListAsync();
        }

        public async Task<Storage?> GetByIdAsync(int id)
        {
            return await _context.Storages
                .Include(t => t.Transactions)
                .FirstOrDefaultAsync(x => x.Id == x.Id);
        }

        public async Task<bool> CreateAsync(Storage storage)
        {
            if(!IsValid(storage))
            {
                return false;
            }

            await _context.Storages.AddAsync(storage);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditAsync(Storage storageData)
        {
            var storage = await _context.Storages.FirstOrDefaultAsync(x => x.Id == storageData.Id);
            
            if(storage is null)
            {
                return false;
            }
            
            if (!string.IsNullOrEmpty(storageData.Name) && storage.Name != storageData.Name)
            {
                storage.Name = storageData.Name;
            }

            if (storage.Value != storageData.Value)
            {
                storage.Value = storageData.Value;
            }

            try
            {
                _context.Update(storage);
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
            var storage = await _context.Storages
                .FirstOrDefaultAsync(x=> x.Id == id);

            if(storage is null)
            {
                return false;
            }

            if(storage.Transactions?.Count >0)
            {
                throw new Exception("This category contain transaction");
            }

            _context.Storages.Remove(storage);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool IsValid(Storage storage)
        {
            if(_context.Storages.FirstOrDefaultAsync(x=> x.Name == storage.Name) != null)
            {
                return false;
            }

            return true;
        }
    }
}
