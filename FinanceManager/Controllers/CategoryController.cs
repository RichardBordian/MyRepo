using FinanceManager.Models;
using FinanceManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private CategoryServices _categoryServices = new CategoryServices();

        [HttpGet]
        public async Task<List<Category>> GetAllAsync()
        {
            return await _categoryServices.GetAllCategoriesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetByIdAsync(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var category = await _categoryServices.GetCategoryAsync(id);

            return category == null ? NotFound() : Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostAsync(Category category)
        {
            if (category is null)
            {
                return BadRequest();
            }

            if (!await _categoryServices.CreateCategoryAsync(category))
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<Category>> PutAsync(int? id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            if (category is null)
            {
                return NotFound();
            }

            if (!await _categoryServices.EditCategoryAsync(id, category))
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteAsync(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            if (!await _categoryServices.DeleteCategoryAsync(id))
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
