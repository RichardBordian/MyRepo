using FinanceManager.common.DTO;

namespace FinanceBlazor.Services
{
    public class CategoryServices
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static IConfiguration _config;
        private readonly string _controllerRoute;

        public CategoryServices(IConfiguration config)
        {
            _config = config;
            _controllerRoute = _config.GetValue<string>("ServerUrl") + "/category";
        }

        public async Task CreateAsync(CategoryCreateDTO categoryData)
        {
            await _httpClient.PostAsJsonAsync(_controllerRoute, categoryData);
        }

        public async Task EditAsync(CategoryUpdateDTO categoryUpdateDTO, int id)
        {
            var url = $"{_controllerRoute}/{id}";
            await _httpClient.PutAsJsonAsync(url, categoryUpdateDTO);
        }

        public async Task DeleteAsync(int id)
        {
            var url = $"{_controllerRoute}/{id}";
            await _httpClient.DeleteAsync(url);
        }

        public async Task<List<CategoryDTO>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CategoryDTO>>(_controllerRoute);
        }

        public async Task<CategoryViewDTO?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<CategoryViewDTO>($"{_controllerRoute}/{id}");
        }
    }
}
