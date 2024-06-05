namespace FinanceManager.DTO
{
    public class TransactionByStorageDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Date { get; set; }
        public CategoryDTO Category { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
    }
}
