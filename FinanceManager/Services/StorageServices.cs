using FinanceManager.common.DTO;
using FinanceManager.Interfaces;
using FinanceManager.Interfaces.Services;
using FinanceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Services
{
    public class StorageServices(IRepo<Storage> storageRepo, IRepo<Transaction> transactionRepo) : IStorageServices
    {
        public async Task<List<StorageDTO>> GetAllAsync()
        {
            var storages = await storageRepo.GetAllAsync();
            return storages
                .Select(x => new StorageDTO() { Id = x.Id, Name = x.Name, Value = x.Value })
                .ToList();
        }

        public async Task<StorageViewDTO?> GetAsync(int id)
        {
            var storage = await storageRepo.GetByIdAsync(id);

            var temp = await storageRepo.GetTransactionsAsyncByOwnId(id);
                var transactions = temp
                    .Where(x => x.StorageId == id)
                    .Select(x => new TransactionByStorageDTO()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Date = x.Date,
                        Category = new CategoryDTO() { Name = x.Category.Name, Id = x.CategoryId },
                        Price = x.Price,
                        Description = x.Description
                    })
                    .ToList();

            return new StorageViewDTO() { Name = storage.Name, Value = storage.Value, Transactions = transactions };
        }

        public async Task<bool> CreateAsync(StorageCreateDTO storageData)
        {
            var storage = new Storage()
            {
                Name = storageData.Name,
                Value = storageData.Value
            };

            await storageRepo.AddAsync(storage);
            await storageRepo.SaveAsync();

            return true;
        }

        public async Task<bool> EditAsync(int id, StorageUpdateDTO storageData)
        {
            if (id != storageData.Id)
            {
                return false;
            }
            var storage = await storageRepo.GetByIdAsync(id);

            if (storageData.Name != null && storage.Name != storageData.Name)
            {
                storage.Name = storageData.Name;
            }

            if (storage.Value != storageData.Value)
            {
                await transactionRepo.AddAsync(new Transaction()
                {
                    Name = "correcting",
                    Date = DateTime.Now,
                    Price = storage.Value - storageData.Value,
                    StorageId = storageData.Id,
                    CategoryId = 1,
                    Description = null
                });
            }

            try
            {
                storageRepo.Update(storage);
                await storageRepo.SaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var storage = await storageRepo.GetByIdAsync(id);

            if (storage.Transactions != null)

            {
                await _storageRepository.DeleteAsync(id);
                return true;
            }

            storageRepo.Remove(storage);
            await storageRepo.SaveAsync();
            return true;
        }
    }
}