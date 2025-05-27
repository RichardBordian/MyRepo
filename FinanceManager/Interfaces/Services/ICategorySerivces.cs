using FinanceManager.common.DTO;

namespace FinanceManager.Interfaces.Services;

public interface ICategorySerivces
{
    public Task<List<CategoryDTO>> GetAllAsync();

    public Task<CategoryViewDTO?> GetAsync(int id);

    public Task<bool> CreateAsync(CategoryCreateDTO categoryData);

    public Task<bool> EditAsync(int id, CategoryUpdateDTO categoryData);

    public Task<bool> DeleteAsync(int id);
}