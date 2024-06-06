using FinanceManager.common.DTO;

namespace FinanceBlazor.Services
{
    public class StorageServices
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static IConfiguration _configuration;
        
        private readonly string _controllerRoute = _configuration.GetValue<string>("BaseUrl") + "/storage";

        public async Task CreateASync(StorageCreateDTO storageCreateDto)
        {
            await _httpClient.PutAsJsonAsync(_controllerRoute, storageCreateDto);
        }

        public async Task EditAsync(StorageUpdateDTO storageUpdateDto, int id)
        {
            var url = $"{_controllerRoute}/{id}";

            await _httpClient.PostAsJsonAsync(url, storageUpdateDto);
        }

        public async Task DeleteAsync(int id)
        {
            var url = $"{_controllerRoute}/{id}";

            await _httpClient.DeleteAsync(url);
        }

        public async Task<List<StorageDTO>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<StorageDTO>>(_controllerRoute);
        }
        public async Task<StorageViewDTO?> GetByIdAsync(int id)
        {
            var url = $"{_controllerRoute}/{id}";

            return await _httpClient.GetFromJsonAsync<StorageViewDTO>(url);
        }
    }
}
