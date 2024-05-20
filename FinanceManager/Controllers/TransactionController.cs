using FinanceManager.DTO;
using FinanceManager.Services;
using FinanceManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private TransactionServices _transactionServices;

        private TransactionController()
        {

        }

        public TransactionController(TransactionServices transactionServices) => _transactionServices = transactionServices;

        [HttpGet]
        public async Task<List<Transaction>> GetAllAsync()
        {
            return await _transactionServices.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetByIdAsync([FromRoute] int id)
        {
            var transaction = await _transactionServices.GetAsync(id);

            return transaction == null ? NotFound() : Ok(transaction);
        }

        [HttpPost]
        public async Task<ActionResult<Transaction>> PostAsync([FromBody]TransactionCreateDTO transactionData)
        {
            if (transactionData == null)
            {
                return BadRequest();
            }

            if(!await _transactionServices.CreateAsync(transactionData))
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<Transaction>> PutAsync([FromRoute]int id, [FromBody]TransactionUpdateDTO transactionData)
        {
            if(id != transactionData.Id)
            {
                return NotFound();
            }

            if (transactionData == null)
            {
                return BadRequest();
            }

            if(!await _transactionServices.EditAsync(id, transactionData))
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Transaction>> DeleteAsync([FromRoute]int id)
        {
            var result = await _transactionServices.DeleteAsync(id);

            return result == false ? BadRequest() : Ok();
        }
    }
}
