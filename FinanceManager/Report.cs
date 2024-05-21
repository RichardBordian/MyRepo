using FinanceManager.Models;

namespace FinanceManager
{
    public class Report
    {
        public double TotalIncome { get; set; }
        public double TotalExpences { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
