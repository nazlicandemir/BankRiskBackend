using BankRiskTracking.Business.Services;
using BankRiskTracking.Entities.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankRiskTracking.Entities.Entities;
using BankRiskTracking.Entities.DTOs;

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

        [HttpGet("ListAll")]
        public IActionResult GetAll()
        {
            var customer = _customerService.ListAll();
            if(!customer.IsSuccess)
            {
                return NotFound("Müşteri bulunamadı");
            }
            return Ok(customer);
        }
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {

            var result = _customerService.Delete(id);
            if (!result.IsSuccess)
            {
                return NotFound("Silinecek müşteri bulunamadı.");
            }
            return Ok(result);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] CustomerCreateDto customer)
        {
            if(customer == null)
            {
                return BadRequest("Geçersiz müşteri verisi gönderildi.");
            }

            var result = _customerService.Create(customer );
            if(result == null)
            {
                return BadRequest("Müşteri oluşturulamadı.");
            }
            return Ok(result);
        }
        [HttpGet("GetByName")]
        public IActionResult GetByName(string name)
        {
            var  result = _customerService.GetByName(name);
            if(result == null)
            {
                return BadRequest("Geçerli bir isim giriniz.");
            }
            return Ok(result.Result);
        }

    }
}
