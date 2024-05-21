using FinanceManager.DTO;
using FinanceManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private StorageServices _storageServices;

        private StorageController()
        { }

        public StorageController(StorageServices storageServices) => _storageServices = storageServices;

        [HttpGet]
        public async Task<List<StoragesDTO>> GetAllAsync()
        {
            return await _storageServices.GetAllAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<StorageViewDTO>> GetByIdAsync([FromRoute] int id)
        {
            var storage = await _storageServices.GetAsync(id);

            return storage == null ? NotFound() : Ok(storage);
        }

        [HttpPost]
        public async Task<ActionResult<StorageCreateDTO>> PostAsync([FromBody] StorageCreateDTO storageData)
        {
            if (storageData is null)
            {
                return BadRequest();
            }

            if (!await _storageServices.CreateAsync(storageData))
            {
                return BadRequest();
            }

            return Ok(storageData);
        }

        [HttpPut]
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

            if (!await _storageServices.EditAsync(id, storageData))
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
                if (!await _storageServices.DeleteAsync(id))
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
