namespace FinanceManager.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime Date { get; set; }
        public required int CategoryId { get; set; }
        public double Price { get; set; }
        public string? Descrpition { get; set; }
        public required int StorageId { get; set; }
    }
}
