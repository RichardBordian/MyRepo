namespace FinanceManager.DTO
{
    public class CategoryUpdateDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
