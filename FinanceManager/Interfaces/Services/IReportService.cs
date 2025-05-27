using FinanceManager.common.DTO;

namespace FinanceManager.Interfaces.Services;

public interface IReportService
{
    public Task<ReportDTO> DailyReport(DateTime date);
    
    public Task<ReportDTO> PeriodReport(DateTime startDate, DateTime endDate);
}