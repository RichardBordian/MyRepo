using Microsoft.AspNetCore.Mvc;
using FinanceManager.Models;
using FinanceManager.Services;

namespace FinanceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private StorageServices _storageServices = new StorageServices();

        [HttpGet]
        public async Task<List<Storage>> GetStoragesAsync()
        {
            return await _storageServices.GetAllStoragesAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Storage>> GetByIdAsync(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var result = await _storageServices.GetStorageAsync(id);

            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Storage>> PostAsync(Storage storage)
        {
            if (storage == null)
            {
                return BadRequest();
            }

            if (!await _storageServices.CreateStorageAsync(storage))
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<Storage>> PutAsync(int? id, Storage storage)
        {
            if (id != storage.Id)
            {
                return BadRequest();
            }

            if (storage == null)
            {
                return BadRequest();
            }

            if (!await _storageServices.EditStorageAsync(id, storage))
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Storage>> DeleteAsync(int? id)
        {
            if(id is null)
            {
                return NotFound();
            }

            if(!await _storageServices.DeleteStorageAsync(id))
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
