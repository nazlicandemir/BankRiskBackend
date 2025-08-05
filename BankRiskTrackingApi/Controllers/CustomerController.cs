using BankRiskTracking.Business.Services;
using BankRiskTracking.Entities.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankRiskTrackingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        //Dependency Injection
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("customer/{name}")]
        public void customerBagimliligiOlanMetot(string name)
        {
           var musteriIsmi = _customerService.GetByName(name);
        }

        [HttpGet("ListAll")]
        public IActionResult GetAll()
        {
            var customerNote = _customerService.ListAll();
            if (customerNote == null)
            {
                return BadRequest("Müşteri bulunamadı ");
            }

            //ok 200 doner
            //BadRequest 400 doner
            //NotFound 404 doner    
            return Ok(customerNote);
        }

    }
}
