using FinanceManager.common.DTO;


namespace FinanceBlazor.Services
{
    public class TransactionServices
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static IConfiguration _configuration;

        private readonly string _controllerRoute = _configuration.GetValue<string>("BaseUrl") + "/category";

        public async Task CreateAsync(TransactionCreateDTO transactionCreateDto)
        {
            await _httpClient.PutAsJsonAsync(_controllerRoute, transactionCreateDto);
            
        }

        public async Task EditAsync(TransactionUpdateDTO transactionUpdateDto, int id)
        {
            var url = $"{_controllerRoute}/{id}";

            await _httpClient.PostAsJsonAsync(url, transactionUpdateDto);
        }

        public async Task DeleteAsync(int id)
        {
            var url = $"{_controllerRoute}/{id}";

            await _httpClient.DeleteAsync(url);
        }

        public async Task<List<TransactionDTO>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TransactionDTO>>(_controllerRoute);
        }

        public async Task<TransactionViewDTO?> GetByIdAsync(int id)
        {
            var url = $"{_controllerRoute}/{id}";
            
            return await _httpClient.GetFromJsonAsync<TransactionViewDTO>(url);
        }
    }
}
