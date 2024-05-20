namespace FinanceManager.DTO
{
    public class TransactionCreateDTO
    {
        public required string Name { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public string? Descrpition { get; set; }
        public int StorageId { get; set; }
    }
}
