using FinanceManager.common.DTO;
using FinanceManager.Interfaces.Services;
using FinanceManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategorySerivces categorySerivces) : ControllerBase
    {
        [HttpGet]
        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            return await categorySerivces.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryViewDTO>> GetByIdAsync([FromRoute] int id)
        {
            var category = await categorySerivces.GetAsync(id);

            return category == null ? NotFound() : Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryCreateDTO>> PostAsync([FromBody] CategoryCreateDTO categoryDataDto)
        {
            if (!await categorySerivces.CreateAsync(categoryDataDto))
            {
                return BadRequest();
            }

            return Ok(categoryDataDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> PutAsync([FromRoute] int id, [FromBody] CategoryUpdateDTO categoryDataDto)
        {
            if (id != categoryDataDto.Id)
            {
                return BadRequest();
            }
            
            if (!await categorySerivces.EditAsync(id, categoryDataDto))
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute] int id)
        {
            try
            {
                if (!await categorySerivces.DeleteAsync(id))
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
