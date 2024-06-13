using FinanceManager.common.DTO;


namespace FinanceBlazor.Services
{
    public class TransactionServices
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static IConfiguration _config;
        private readonly string _controllerRoute;

        public TransactionServices(IConfiguration config)
        {
            _config = config;
            _controllerRoute = _config.GetValue<string>("ServerUrl") + "/transaction";
        }

        public async Task CreateAsync(TransactionCreateDTO transactionCreateDto)
        {
            await _httpClient.PostAsJsonAsync(_controllerRoute, transactionCreateDto);
            
        }

        public async Task EditAsync(TransactionUpdateDTO transactionUpdateDto, int id)
        {
            var url = $"{_controllerRoute}/{id}";

            await _httpClient.PutAsJsonAsync(url, transactionUpdateDto);
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
