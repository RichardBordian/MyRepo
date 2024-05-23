﻿using FinanceManager.DTO;
using FinanceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Services
{
    public class TransactionServices
    {
        private readonly Context _context;

        private TransactionServices()
        {}

        public TransactionServices(Context context) => _context = context;

        public async Task<List<TransactionDTO>> GetAllAsync()
        {
            return await _context.Transactions
                .Select(x=> new TransactionDTO() { Name = x.Name, Date = x.Date, CategoryId = x.CategoryId, StorageId = x.StorageId, Price= x.Price, Description = x.Descrpition  })
                .ToListAsync();
        }

        public async Task<TransactionDTO?> GetAsync(int id)
        {
            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(x => x.Id == id);

            return transaction == null
                    ? null
                    : new TransactionDTO() { Name = transaction.Name, Date = transaction.Date, CategoryId = transaction.CategoryId, StorageId = transaction.StorageId, Price = transaction.Price, Description = transaction.Descrpition };
        }

        public async Task<bool> CreateAsync(TransactionCreateDTO transactionData)
        {
            if(!IsValid(transactionData)) 
            {
                return false;
            }

            var transaction = new Transaction() 
            {
                Name =  transactionData.Name, 
                Date = transactionData.Date, 
                CategoryId = transactionData.CategoryId,
                Price = transactionData.Price,
                Descrpition = transactionData.Description,
                StorageId = transactionData.StorageId,
            };

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditAsync(int id, TransactionUpdateDTO transactionData)
        {
            if (id != transactionData.Id)
            {
                return false;
            }

            if (!_context.Transactions.Any(x => x.Id == transactionData.Id))
            {
                return false;
            }

            var transaction = await _context.Transactions.FirstAsync(x => x.Id == id);

            if (!string.IsNullOrEmpty(transactionData.Name) & transactionData.Name != transaction.Name)
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
            if (transactionData.Descrpition != null & transactionData.Descrpition != transaction.Descrpition)
            {
                transaction.Descrpition = transactionData.Descrpition;
            }
            if (transactionData.StorageId != transaction.StorageId)
            {
                transaction.StorageId = transactionData.StorageId;
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

        public async Task<bool> DeleteAsync(int id)
        {
            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(t=> t.Id == id);

            if(transaction is null)
            {
                return false;
            }

            _context.Remove(transaction);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool IsValid(TransactionCreateDTO transactionData)
        {
            if (transactionData.Name == null & _context.Transactions.FirstOrDefault(t=> t.Name == transactionData.Name) != null)
            {
                return false;
            }
            return true;
        }
    }
}
