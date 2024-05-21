namespace FinanceManager.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public double Value { get; set; }
        public List<Transaction>? Transactions { get; set;}
    }
}
