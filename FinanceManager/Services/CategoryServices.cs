using FinanceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Services
{
    public class CategoryServices
    {
        private readonly Context _context = new Context();

        public async Task<List<Category>> GetCategoriesAsync(bool IsIncome)
        {
            return await _context.Categories
                .Include(t => t.Transactions)
                .Where(x => x.IsIncome == IsIncome)
                .ToListAsync();
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .ToListAsync();
        }

        public async Task<bool> CreateCategoryAsync(Category category)
        {
            if(!CategoryIsValid(category))
            {
                return false;
            }

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            
            return true;
        }

        public async Task<Category?> GetCategoryAsync(int? id)
        {
            if (id is null)
            {
                return null;
            }

            return await _context.Categories
                .Include(t => t.Transactions)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> EditCategoryAsync(int? id, Category category)
        {
            if( id != category.Id)
            {
                return false;
            }

            if (!_context.Categories.Any(x => x.Id == id))
            {
                return false;
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

        public async Task<bool> DeleteCategoryAsync(int? id)
        {
            if (id is null)
            {
                return false;
            }

            var category = await _context.Categories
                .Include(t=> t.Transactions)
                .FirstOrDefaultAsync(c=> c.Id == id);

            if(category is null)
            {
                return false;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
       
        private bool CategoryIsValid(Category category)
        {
            if (_context.Categories.FirstOrDefault(c => c.Name == category.Name) != null & category.Name == null)
            {
                return false;
            }

            return true;
        }
    }
}
