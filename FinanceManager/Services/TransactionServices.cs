using FinanceManager.Models;
using FinanceManager.common.DTO;
using FinanceManager.Repositories;

namespace FinanceManager.Services
{
    public class TransactionServices
    {
        private readonly TransactionRepository _transactionRepository;

        private TransactionServices()
        { }

        public TransactionServices(TransactionRepository transactionRepository) => _transactionRepository = transactionRepository;

        public async Task<List<TransactionDTO>> GetAllAsync()
        {
            var transactions = await _transactionRepository.GetAllAsync();

            return transactions
                .Select(x => new TransactionDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Date = x.Date,
                    Category = new CategoryDTO() { Name = x.Category.Name, Id = x.Category.Id },
                    Storage = new StorageDTO() { Name = x.Storage.Name, Id = x.Storage.Id },
                    Price = x.Price,
                    Description = x.Description,
                })
                .ToList();
        }

        public async Task<TransactionViewDTO?> GetAsync(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);

            if (transaction == null)
            {
                return null;
            }

            return new TransactionViewDTO()
            {
                Name = transaction.Name,
                Date = transaction.Date,
                Category = new CategoryDTO() { Name = transaction.Category.Name, Id = transaction.Category.Id},
                Storage = new StorageDTO() { Name = transaction.Storage.Name, Id = transaction.Storage.Id},
                Price = transaction.Price,
                Description = transaction.Description
            };
        }

        public async Task<bool> CreateAsync(TransactionCreateDTO transactionData)
        {
            var transaction = new Transaction()
            {
                Name = transactionData.Name,
                Date = transactionData.Date,
                CategoryId = transactionData.CategoryId,
                Price = transactionData.Price,
                Description = transactionData.Description,
                StorageId = transactionData.StorageId,
            };

            return await _transactionRepository.CreateAsync(transaction);
        }

        public async Task<bool> EditAsync(int id, TransactionUpdateDTO transactionData)
        {
            if (id != transactionData.Id)
            {
                return false;
            }

            var transaction = new Transaction()
            {
                Name = transactionData.Name == null ? "" : transactionData.Name,
                Date = transactionData.Date,
                CategoryId = transactionData.CategoryId,
                Price = transactionData.Price,
                Description = transactionData.Description,
                StorageId = transactionData.StorageId,
            };

            try
            {
                await _transactionRepository.EditAsync(transaction);
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
                await _transactionRepository.DeleteAsync(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
