using FinanceManager.Models;
using FinanceManager.common.DTO;
using FinanceManager.Interfaces;
using FinanceManager.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Services
{
    public class TransactionServices(IRepo<Transaction> transactionRepo) : ITransactionServices
    {
        public async Task<List<TransactionDTO>> GetAllAsync()
        {
            var transactions = await transactionRepo.GetAllAsync();
            return transactions
                .Select(x => new TransactionDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Date = x.Date,
                    Category = new CategoryDTO() { Name = x.Category.Name, Id = x.CategoryId },
                    Storage = new StorageDTO() { Name = x.Storage.Name, Id = x.StorageId },
                    Price = x.Price,
                    Description = x.Description,
                })
                .ToList();
        }

        public async Task<TransactionViewDTO?> GetAsync(int id)
        {
            var transaction = await transactionRepo.GetByIdAsync(id);

            return new TransactionViewDTO()
            {
                Name = transaction.Name,
                Date = transaction.Date,
                Category = new CategoryDTO() { Name = transaction.Category.Name, Id = transaction.CategoryId},
                Storage = new StorageDTO() { Name = transaction.Storage.Name, Id = transaction.StorageId},
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

            await transactionRepo.AddAsync(transaction);
            await transactionRepo.SaveAsync();
            
            return true
        }

        public async Task<bool> EditAsync(int id, TransactionUpdateDTO transactionData)
        {
            if (id != transactionData.Id)
            {
                return false;
            }
            
            var transaction = await transactionRepo.GetByIdAsync(id);

            if (transactionData.Name != null && transactionData.Name != transaction.Name)
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
                Name = transactionData.Name == null ? "" : transactionData.Name,
                Date = transactionData.Date,
                CategoryId = transactionData.CategoryId,
                Price = transactionData.Price,
                Description = transactionData.Description,
                StorageId = transactionData.StorageId,
            };

            try
            {
                transactionRepo.Update(transaction);
                await transactionRepo.SaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var transaction = await transactionRepo.GetByIdAsync(id);

            transactionRepo.Remove(transaction);
            await transactionRepo.SaveAsync();

            return true;
        }
    }
}
