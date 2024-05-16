using FinanceManager.Services;
using FinanceManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private TransactionServices _transactionServices = new TransactionServices();

        [HttpGet]
        public async Task<List<Transaction>> GetAllAsync()
        {
            return await _transactionServices.GetAllTransactionsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetByIdAsync(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var transaction = await _transactionServices.GetTransactionAsync(id);

            return transaction == null ? NotFound() : Ok(transaction);
        }

        [HttpPost]
        public async Task<ActionResult<Transaction>> PostAsync(Transaction transaction)
        {
            if (transaction == null)
            {
                return BadRequest();
            }

            var result = await _transactionServices.CreateTransactionAsync(transaction);

            return result == false ? BadRequest() : Ok();
        }

        [HttpPut]
        public async Task<ActionResult<Transaction>> PutAsync(int? id, Transaction transaction)
        {
            if(id != transaction.Id)
            {
                return NotFound();
            }

            if (transaction == null)
            {
                return BadRequest();
            }

            var result = await _transactionServices.EditTransactionAsync(id, transaction);

            return result == false ? BadRequest() : Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Transaction>> DeleteAsync(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var result = await _transactionServices.DeleteTransactionAsync(id);

            return result == false ? BadRequest() : Ok();
        }
    }
}
