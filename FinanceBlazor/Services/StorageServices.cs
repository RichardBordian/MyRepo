using FinanceManager.common.DTO;

namespace FinanceBlazor.Services
{
    public class StorageServices
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static IConfiguration _config;
        private readonly string _controllerRoute;

        public StorageServices(IConfiguration config)
        {
            _config = config;
            _controllerRoute = _config.GetValue<string>("ServerUrl") + "/storage";
        }

        public async Task CreateASync(StorageCreateDTO storageCreateDto)
        {
            await _httpClient.PostAsJsonAsync(_controllerRoute, storageCreateDto);
        }

        public async Task EditAsync(StorageUpdateDTO storageUpdateDto, int id)
        {
            var url = $"{_controllerRoute}/{id}";

            await _httpClient.PutAsJsonAsync(url, storageUpdateDto);
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
