namespace FinanceManager.common.DTO
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Date { get; set; }
        public CategoryDTO Category { get; set; }
        public StorageDTO Storage { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
    }
}
