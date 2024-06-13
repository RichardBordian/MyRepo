namespace FinanceManager.common.DTO
{
    public class ReportDTO
    {
        public double TotalIncome { get; set; }
        public double TotalExpenses { get; set; }
        public List<TransactionDTO> Transactions { get; set; }
    }
}
