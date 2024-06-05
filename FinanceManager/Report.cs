using FinanceManager.DTO;

namespace FinanceManager
{
    public class Report
    {
        public double TotalIncome { get; set; }
        public double TotalExpences { get; set; }
        public List<TransactionDTO> Transactions { get; set; }
    }
}
