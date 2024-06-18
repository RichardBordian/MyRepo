using FinanceManager.Models;
using FinanceManager.common.DTO;
using FinanceManager.Repositories;

namespace FinanceManager.Services
{
    public class CategoryServices//: IServices<CategoryViewDTO>
    {
        private readonly CategoryRepository _categoryRepository;

        private CategoryServices()
        { }

        public CategoryServices(CategoryRepository categoryRepository) => _categoryRepository = categoryRepository;

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return categories
                .Select(x => new CategoryDTO() { Id = x.Id, Name = x.Name, IsIncome = x.IsIncome })
                .ToList();
        }

        public async Task<CategoryViewDTO?> GetAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category is null)
            {
                return null;
            }

            var transactions = category.Transactions;

            var operations = transactions?.Select(x => new TransactionByCategoryDTO()
            {
                Date = x.Date,
                Description = x.Description,
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Storage = new StorageDTO() { Id = x.Storage.Id, Name = x.Storage.Name, Value = x.Storage.Value },
            })
                .ToList();

            return new CategoryViewDTO() { Id = category.Id, Name = category.Name, Description = category.Description, IsIncome = category.IsIncome, Transactions = operations };
        }

        public async Task<bool> CreateAsync(CategoryCreateDTO categoryData)
        {
            var category = new Category()
            {
                Name = categoryData.Name,
                Description = categoryData.Description,
                IsIncome = categoryData.IsIncome
            };

            return await _categoryRepository.CreateAsync(category);
        }

        public async Task<bool> EditAsync(int id, CategoryUpdateDTO categoryData)
        {
            if (id != categoryData.Id)
            {
                return false;
            }

            var category = new Category()
            {
                Name = categoryData.Name == null ? "" : categoryData.Name,
                Description = categoryData.Description,
                IsIncome = categoryData.IsIncome,
                Id = categoryData.Id,
            };

            try
            {
                await _categoryRepository.EditAsync(category);
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
                await _categoryRepository.DeleteAsync(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
