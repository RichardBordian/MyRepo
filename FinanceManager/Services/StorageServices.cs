using FinanceManager.Models;
using FinanceManager.common.DTO;
using FinanceManager.Repositories;

namespace FinanceManager.Services
{
    public class StorageServices
    {
        private readonly StorageRepository _storageRepository;

        private StorageServices()
        {

        }

        public StorageServices(StorageRepository storageRepository) => _storageRepository = storageRepository;

        public async Task<List<StorageDTO>> GetAllAsync()
        {
            var storages = await _storageRepository.GetAllAsync();

            return storages
                .Select(x => new StorageDTO() { Id = x.Id, Name = x.Name, Value = x.Value })
                .ToList();
        }

        public async Task<StorageViewDTO?> GetAsync(int id)
        {
            var storage = await _storageRepository.GetByIdAsync(id);
            if (storage is null)
            {
                return null;
            }

            var transactions = storage.Transactions;

            var operations = transactions?
                .Select(x => new TransactionByStorageDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Date = x.Date,
                    Category = new CategoryDTO() { Name = x.Category.Name, Id = x.Category.Id, IsIncome = x.Category.IsIncome },
                    Price = x.Price,
                    Description = x.Description
                })
                .ToList();

            return new StorageViewDTO() { Name = storage.Name, Value = storage.Value, Transactions = operations };
        }

        public async Task<bool> CreateAsync(StorageCreateDTO storageData)
        {
            var storage = new Storage()
            {
                Name = storageData.Name,
                Value = storageData.Value
            };

            return await _storageRepository.CreateAsync(storage);
        }

        public async Task<bool> EditAsync(int id, StorageUpdateDTO storageData)
        {
            if (id != storageData.Id)
            {
                return false;
            }

            var storage = new Storage()
            {
                Id = storageData.Id,
                Name = storageData.Name == null ? "" : storageData.Name,
                Value = storageData.Value,
            };

            try
            {
                await _storageRepository.EditAsync(storage);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _storageRepository.DeleteAsync(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}