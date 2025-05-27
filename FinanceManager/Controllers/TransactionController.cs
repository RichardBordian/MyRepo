using FinanceManager.common.DTO;
using FinanceManager.Interfaces.Services;
using FinanceManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController(ITransactionServices transactionServices) : ControllerBase
    {
        [HttpGet]
        public async Task<List<TransactionDTO>> GetAllAsync()
        {
            return await transactionServices.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionViewDTO>> GetByIdAsync([FromRoute] int id)
        {
            var transaction = await transactionServices.GetAsync(id);

            return transaction == null ? NotFound() : Ok(transaction);
        }

        [HttpPost]
        public async Task<ActionResult<TransactionCreateDTO>> PostAsync([FromBody]TransactionCreateDTO transactionData)
        {
            if (transactionData == null)
            {
                return BadRequest();
            }

            if(!await transactionServices.CreateAsync(transactionData))
            {
                return BadRequest();
            }

            return Ok(transactionData);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> PutAsync([FromRoute]int id, [FromBody]TransactionUpdateDTO transactionData)
        {
            if(id != transactionData.Id)
            {
                return NotFound();
            }

            if (transactionData == null)
            {
                return BadRequest();
            }

            if(!await transactionServices.EditAsync(id, transactionData))
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]int id)
        {
            var result = await transactionServices.DeleteAsync(id);

            return result == false ? BadRequest() : Ok();
        }
    }
}
