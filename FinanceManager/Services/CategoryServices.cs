using FinanceManager.common.DTO;
using FinanceManager.Interfaces;
using FinanceManager.Interfaces.Services;
using FinanceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Services
{
    public class CategoryServices(IRepo<Category> repo) : ICategorySerivces
    {
        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            var result = await repo.GetAllAsync();
            return result
                .Select(x=> new CategoryDTO() { Id = x.Id, Name = x.Name, IsIncome = x.IsIncome })
                .ToList();
        }

        public async Task<CategoryViewDTO?> GetAsync(int id)
        {
            var temp = await repo.GetTransactionsAsyncByOwnId(id);
            
            var transactions = temp
                    .Where(x => x.CategoryId == id)
                    .Select(x => new TransactionByCategoryDTO()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Date = x.Date,
                        Storage = new StorageDTO() { Name = x.Storage.Name, Id = x.StorageId, },
                        Price = x.Price,
                        Description = x.Description
                    })
                    .ToList()
                ;
            var category = temp.Select(x => x.Category).FirstOrDefault(x => x.Id == id);
            
            return category == null
                ? null
                : new CategoryViewDTO() { Id = category.Id, Name = category.Name, Description = category.Description, IsIncome = category.IsIncome, Transactions = transactions };
        }

        public async Task<bool> CreateAsync(CategoryCreateDTO categoryData)
        {
            var category = new Category()
            { 
                Name = categoryData.Name, 
                Description = categoryData.Description,
                IsIncome = categoryData.IsIncome 
            };

            await repo.AddAsync(category);
            await repo.SaveAsync();

            return true;
        }

        public async Task<bool> EditAsync(int id, CategoryUpdateDTO categoryData)
        {
            if (id != categoryData.Id)
            {
                return false;
            }

            var category = await repo.GetByIdAsync(id);

            if (categoryData.Name != null && category.Name != categoryData.Name)
            {
                category.Name = categoryData.Name;
            }

            if (categoryData.Description != null && category.Description != categoryData.Description)
            {
                category.Description = categoryData.Description;
            }

            if (category.IsIncome != categoryData.IsIncome)
            {
                category.IsIncome = categoryData.IsIncome;
            }

            try
            {
                repo.Update(category);
                await repo.SaveAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Update exception");
            }

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await repo.GetByIdAsync(id);

            if (category.Transactions != null)
            {
                throw new Exception("This category contain transaction");
            }

            repo.Remove(category);
            await repo.SaveAsync();

            return true;
        }
    }
}
