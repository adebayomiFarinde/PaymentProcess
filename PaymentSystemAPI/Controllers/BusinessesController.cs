using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentSystem.Core.Entities;
using PaymentSystem.Core.Helper;
using PaymentSystem.Core.Repository;
using PaymentSystemAPI.Models;

namespace PaymentSystemAPI.Controllers
{
    [Route("api/businesses/")]
    [ApiController]
    [ProducesResponseType(typeof(RequestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RequestResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RequestResponse), StatusCodes.Status500InternalServerError)]
    public class BusinessesController : ControllerBase
    {
        private IGenericRepository<Business> _businessRepository;
        private IGenericRepository<Contact> _contactRepository;

        public BusinessesController(IGenericRepository<Business> businessRepository,
             IGenericRepository<Contact> contactRepository)
        {
            _businessRepository = businessRepository;
            _contactRepository = contactRepository;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateBusiness([FromBody] BusinessModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Select(X => X.Value));
            }

            var contact = new Contact
            {
                Id = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                CreatedDate = DateTime.Now
            };

            await _contactRepository.Create(contact);

            await _contactRepository.SaveChangesAsync();
            var business = new Business
            {
                Id = Guid.NewGuid(),
                TransactionVolume = model.TransactionVolume,
                ContactId = contact.Id,
                MerchantNumber = model.MerchantNumber,
                DateOfEstablishment = model.DateOfEstablishment,
                CreatedDate = DateTime.Now
            };

            await _businessRepository.Create(business);
            await _businessRepository.SaveChangesAsync();


            return Ok(business.Id);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBusiness(Guid id)
        {
            var business = await _businessRepository.Query(x => x.Id == id).Include(c => c.Contact).FirstOrDefaultAsync();

            if (business.IsNull()) return NotFound(id);

            return Ok(business);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllBusinesses()
        {
            var businesses = await _businessRepository.GetAll().Include(c => c.Contact).ToListAsync();

            if (businesses.IsNullOrEmpty()) return NotFound();

            return Ok(businesses);
        }
    }
}
