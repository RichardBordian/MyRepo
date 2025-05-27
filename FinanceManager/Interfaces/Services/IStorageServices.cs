using FinanceManager.common.DTO;

namespace FinanceManager.Interfaces.Services;

public interface IStorageServices
{
    public Task<List<StorageDTO>> GetAllAsync();

    public Task<StorageViewDTO?> GetAsync(int id);

    public Task<bool> CreateAsync(StorageCreateDTO categoryData);

    public Task<bool> EditAsync(int id, StorageUpdateDTO categoryData);

    public Task<bool> DeleteAsync(int id);
}