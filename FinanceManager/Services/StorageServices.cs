using FinanceManager.DTO;
using FinanceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Services
{
    public class StorageServices
    {
        private readonly Context _context;

        private StorageServices()
        {

        }

        public StorageServices(Context context) => _context = context;

        public async Task<List<Storage>> GetAllAsync()
        {
            return await _context.Storages
                .ToListAsync();
        }

        public async Task<Storage?> GetAsync(int id)
        {
            return await _context.Storages
                .Include(t=> t.Transactions)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> CreateAsync(StorageCreateDTO storageData)
        {
            if(!IsValid(storageData))
            {
                return false;
            }

            var storage = new Storage()
            {
                Name = storageData.Name,
                Value = storageData.Value
            };

            await _context.Storages.AddAsync(storage);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditAsync(int id, StorageUpdateDTO storageData)
        {
            if(id!= storageData.Id) 
            {
                return false;
            }

            if(!_context.Storages.Any(x=> x.Id  == id)) 
            {
                return false;
            }

            var storage = await _context.Storages.FirstAsync(x=> x.Id == id);

            if(!string.IsNullOrEmpty(storageData.Name) & storage.Name != storageData.Name)
            {
                storage.Name = storageData.Name;
            }
            if(storage.Value != storageData.Value)
            {
                storage.Value = storageData.Value;
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

        public async Task<bool> DeleteAsync(int id)
        {
            var storage = await _context.Storages
                .Include(t => t.Transactions)
                .FirstOrDefaultAsync(s => s.Id == id);

            if(storage == null)
            {
                return false;
            }

            if(storage.Transactions.Count >0)
            {
                throw new Exception("This storage contain transactions");
            }

            _context.Storages.Remove(storage);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool IsValid(StorageCreateDTO storageData)
        {
            if (storageData.Name == null & _context.Storages.FirstOrDefault(s=> s.Name == storageData.Name) != null)
            {
                return false;
            }

            return true;
        }
    }
}
