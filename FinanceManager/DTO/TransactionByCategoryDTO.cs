namespace FinanceManager.DTO
{
    public class TransactionByCategoryDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Date { get; set; }
        public int StorageId { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
    }
}
