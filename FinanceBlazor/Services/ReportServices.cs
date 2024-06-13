using FinanceManager.common.DTO;

namespace FinanceBlazor.Services
{
    public class ReportServices
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static IConfiguration _config;
        private readonly string _controllerRoute;
     
        public ReportServices(IConfiguration config)
        {
            _config = config;
            _controllerRoute = _config.GetValue<string>("ServerUrl") + "/report";
        }

        public async Task<ReportDTO?> GetDailyAsync(DateTime date)
        {
            var url = $"{_controllerRoute}/{date.ToString("MM-dd-yyyy")}";
            return await _httpClient.GetFromJsonAsync<ReportDTO>(url);  
        }

        public async Task<ReportDTO?> GetPeriodAsync(DateTime from, DateTime to)
        {
            var url = $"{_controllerRoute}/{from.ToString("MM-dd-yyyy")}/{to.ToString("MM/dd/yyyy")}";

            return await _httpClient.GetFromJsonAsync<ReportDTO>(url);
        }
    }
}
