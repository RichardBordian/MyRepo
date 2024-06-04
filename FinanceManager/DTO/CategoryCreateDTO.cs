namespace FinanceManager.DTO
{
    public class CategoryCreateDTO
    {
        public required string Name { get; set; }
        public bool IsIncome { get; set; }
        public string? Description { get; set; }
    }
}
