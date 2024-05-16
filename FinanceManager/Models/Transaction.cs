namespace FinanceManager.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public required Category Category { get; set; }
        public double Price { get; set; }
        public string? Descrpition { get; set; }
        public int StorageId { get; set; }
        public Storage Storage { get; set; }
    }
}
