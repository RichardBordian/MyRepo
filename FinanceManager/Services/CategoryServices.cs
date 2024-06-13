using FinanceManager.common.DTO;
using FinanceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Services
{
    public class CategoryServices
    {
        private readonly Context _context;

        private CategoryServices()
        { }

        public CategoryServices(Context context) => _context = context;

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            return await _context.Categories
                .Select(x=> new CategoryDTO() { Id = x.Id, Name = x.Name, IsIncome = x.IsIncome })
                .ToListAsync();
        }

        public async Task<CategoryViewDTO?> GetAsync(int id)
        {
            var category = await _context.Categories
            .Include(t => t.Transactions)
            .FirstOrDefaultAsync(c => c.Id == id);

            var transactions = await _context.Transactions
                .Where(x => x.CategoryId == id)
                .Select(x => new TransactionByCategoryDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Date = x.Date,
                    Storage = new StorageDTO() { Name = _context.Storages.FirstOrDefault(c => c.Id == x.StorageId).Name, Id = _context.Storages.FirstOrDefault(c => c.Id == x.StorageId).Id },
                    Price = x.Price,
                    Description = x.Description
                })
                .ToListAsync();

            return category == null
                ? null
                : new CategoryViewDTO() { Id = category.Id, Name = category.Name, Description = category.Description, IsIncome = category.IsIncome, Transactions = transactions };
        }

        public async Task<bool> CreateAsync(CategoryCreateDTO categoryData)
        {
            if (!IsValid(categoryData))
            {
                return false;
            }

            var category = new Category()
            { 
                Name = categoryData.Name, 
                Description = categoryData.Description,
                IsIncome = categoryData.IsIncome 
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditAsync(int id, CategoryUpdateDTO categoryData)
        {
            if (id != categoryData.Id)
            {
                return false;
            }

            if (!_context.Categories.Any(x => x.Id == id))
            {
                return false;
            }

            var category = await _context.Categories.FirstAsync(x => x.Id == id);

            if (!string.IsNullOrEmpty(categoryData.Name) & category.Name != categoryData.Name)
            {
                category.Name = categoryData.Name;
            }

            if (categoryData.Description != null & category.Description != categoryData.Description)
            {
                category.Description = categoryData.Description;
            }

            if (category.IsIncome != categoryData.IsIncome)
            {
                category.IsIncome = categoryData.IsIncome;
            }

            try
            {
                _context.Update(category);
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
            var category = await _context.Categories
                .Include(t => t.Transactions)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category is null)
            {
                return false;
            }

            if(category.Transactions.Count >0)
            {
                throw new Exception("This category contain transaction");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool IsValid(CategoryCreateDTO category)
        {
            if (category.Name == null & _context.Categories.FirstOrDefault(c => c.Name == category.Name) != null)
            {
                return false;
            }

            return true;
        }
    }
}
