using FinanceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Repositories
{
    public class CategoryRepository : IRepositories<Category>
    {
        private readonly Context _context;
        private CategoryRepository() { }

        public CategoryRepository(Context context) => _context = context;
        
        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }
        
        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Include(t => t.Transactions)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> CreateAsync(Category category)
        {
            if (!IsValid(category))
            {
                return false;
            }

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditAsync(Category categoryData)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == categoryData.Id);
            
            if(category is null)
            {
                return false;
            }
            
            if (!string.IsNullOrEmpty(categoryData.Name) && category.Name != categoryData.Name)
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
                _context.Update(category);
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
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category is null)
            {
                return false;
            }

            if (category.Transactions?.Count > 0)
            {
                throw new Exception("This category contain transaction");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool IsValid(Category category)
        {
            if (_context.Categories.FirstOrDefault(c => c.Name == category.Name) != null)
            {
                return false;
            }

            return true;
        }
    }
}
