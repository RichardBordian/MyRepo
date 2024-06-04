namespace FinanceManager.DTO
{
    public class TransactionByStorageDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
    }
}
