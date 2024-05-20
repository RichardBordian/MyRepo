using FinanceManager.DTO;
using FinanceManager.Models;
using FinanceManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private CategoryServices _categoryServices;

        private CategoryController()
        {}

        public CategoryController(CategoryServices categoryServices) => _categoryServices = categoryServices;

        [HttpGet]
        public async Task<List<Category>> GetAllAsync()
        {
            return await _categoryServices.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetByIdAsync([FromRoute]int id)
        {
            var category = await _categoryServices.GetAsync(id);

            return category == null ? NotFound() : Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostAsync([FromBody]CategoryCreateDTO categoryData)
        {
            if (categoryData is null)
            {
                return BadRequest();
            }

            if (!await _categoryServices.CreateAsync(categoryData))
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<Category>> PutAsync([FromRoute]int id, [FromBody]CategoryUpdateDTO categoryData)
        {
            if (id != categoryData.Id)
            {
                return BadRequest();
            }

            if (categoryData is null)
            {
                return NotFound();
            }

            if (!await _categoryServices.EditAsync(id, categoryData))
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteAsync([FromRoute] int id)
        {
            try
            {
                if (!await _categoryServices.DeleteAsync(id))
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch
            {
                return BadRequest("Category contains transactions");
            }
        }
    }
}
