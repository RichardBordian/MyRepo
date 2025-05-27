using FinanceManager.common.DTO;

namespace FinanceManager.Interfaces.Services;

public interface ITransactionServices
{
    
    public Task<List<TransactionDTO>> GetAllAsync();

    public Task<TransactionViewDTO?> GetAsync(int id);

    public Task<bool> CreateAsync(TransactionCreateDTO categoryData);

    public Task<bool> EditAsync(int id, TransactionUpdateDTO categoryData);

    public Task<bool> DeleteAsync(int id);
}