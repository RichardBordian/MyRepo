namespace FinanceManager.Models
{
    public class Report
    {
        public double TotalIncome { get; set; }
        public double TotalExpences { get; set; }
        public List<Transaction> Tranzactions { get; set; }
    }
}
