using FinanceManager.common.DTO;

namespace FinanceBlazor.Services
{
    public class CategoryServices
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static IConfiguration _configuration;
        
        private readonly string _controllerRoute = _configuration.GetValue<string>("BaseUrl") + "/category";

        public async Task CreateAsync(CategoryCreateDTO categoryData)
        {
            await _httpClient.PutAsJsonAsync(_controllerRoute, categoryData);
        }

        public async Task EditAsync(CategoryUpdateDTO categoryUpdateDTO, int id)
        {
            var url = $"{_controllerRoute}/{id}";
            await _httpClient.PostAsJsonAsync(url, categoryUpdateDTO);
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
