using FinanceManager.Models;

namespace FinanceManager.DTO
{
    public class StorageViewDTO
    {
        public string? Name { get; set; }
        public double Value { get; set; }
        public List<Transaction>? Transactions { get; set; }
    }
}
