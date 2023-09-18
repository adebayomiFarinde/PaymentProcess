using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentSystem.Core.Entities;
using PaymentSystem.Core.Helper;
using PaymentSystem.Core.Repository;
using PaymentSystemAPI.Models;

namespace PaymentSystemAPI.Controllers
{
    [Route("api/customers/")]
    [ApiController]
    [ProducesResponseType(typeof(RequestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RequestResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RequestResponse), StatusCodes.Status500InternalServerError)]
    public class CustomersController : ControllerBase
    {
        private IGenericRepository<Customer> _customerRepository;
        private IGenericRepository<TransactionHistory> _transactionHistoryRepository;

        public CustomersController(IGenericRepository<Customer> customerRepository,
             IGenericRepository<TransactionHistory> transactionHistoryRepository)
        {
            _customerRepository = customerRepository;
            _transactionHistoryRepository = transactionHistoryRepository;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Select(X => X.Value));
            }

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                CreatedDate = DateTime.Now,
                DOB = model.DOB,
                NationalIDNumber = model.NationalIDNumber,
                PhoneNumber = model.PhoneNumber
            };

            await _customerRepository.Create(customer);

            await _customerRepository.SaveChangesAsync();

            return Ok(customer.Id);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCustomer(Guid id)
        {
            var customer = await _customerRepository.Query(x => x.Id == id).Include(c => c.TransactionHistories).FirstOrDefaultAsync();

            if (customer.IsNull()) return NotFound(id);

            return Ok(customer);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAll().Include(c => c.TransactionHistories).ToListAsync();

            if (customers.IsNullOrEmpty()) return NotFound();

            return Ok(customers);
        }

        [HttpPost]
        [Route("/transaction")]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Select(X => X.Value));
            }

            var transaction = new TransactionHistory
            {
                Id = Guid.NewGuid(),
                Amount = model.Amount, 
                BusinessId = model.BusinessId,
                CustomerId = model.CustomerId,
                CreatedDate = DateTime.Now,
            };

            await _transactionHistoryRepository.Create(transaction);

            await _transactionHistoryRepository.SaveChangesAsync();

            return Ok(transaction.Id);
        }
    }
}
