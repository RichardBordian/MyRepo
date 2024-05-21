namespace FinanceManager.DTO
{
    public class TransactionDTO
    {
        public string? Name { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public int StorageId { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
    }
}
