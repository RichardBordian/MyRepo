using FinanceManager.common.DTO;
using FinanceManager.Interfaces.Services;
using FinanceManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController(IStorageServices storageServices) : ControllerBase
    {
        [HttpGet]
        public async Task<List<StorageDTO>> GetAllAsync()
        {
            return await storageServices.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StorageViewDTO>> GetByIdAsync([FromRoute] int id)
        {
            var storage = await storageServices.GetAsync(id);

            return storage == null ? NotFound() : Ok(storage);
        }

        [HttpPost]
        public async Task<ActionResult<StorageCreateDTO>> PostAsync([FromBody] StorageCreateDTO storageData)
        {
            if (storageData is null)
            {
                return BadRequest();
            }

            if (!await storageServices.CreateAsync(storageData))
            {
                return BadRequest();
            }

            return Ok(storageData);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> PutAsync([FromRoute] int id, [FromBody] StorageUpdateDTO storageData)
        {
            if (id != storageData.Id)
            {
                return BadRequest();
            }

            if (storageData == null)
            {
                return NotFound();
            }

            if (!await storageServices.EditAsync(id, storageData))
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
                if (!await storageServices.DeleteAsync(id))
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
