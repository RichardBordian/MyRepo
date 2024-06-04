using FinanceManager.Models;

namespace FinanceManager.DTO
{
    public class CategoryViewDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsIncome { get; set; }
        public List<Transaction>? transactions { get; set; }
    }
}
