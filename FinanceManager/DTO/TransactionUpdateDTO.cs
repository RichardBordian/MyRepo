namespace FinanceManager.DTO
{
    public class TransactionUpdateDTO
    {
        public required int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public int StorageId { get; set; }
        public double Price { get; set; }
        public string? Descrpition { get; set; }
    }
}
