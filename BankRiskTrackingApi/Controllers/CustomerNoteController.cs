using BankRiskTracking.Entities.Entities;
using BankRiskTracking.Entities.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BankRiskTrackingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerNoteController : ControllerBase
    {
        //DI ile servisi inject ettik
        private readonly ICustomerNoteService _customerNoteService;

        public CustomerNoteController(ICustomerNoteService customerNoteService)
        {
            _customerNoteService = customerNoteService;
        }
        [HttpGet("ListAll")]
        public IActionResult GetAll()
        {
            var customerNote = _customerNoteService.ListAll();
            if (customerNote == null)
            {
                return BadRequest("Hiç müşteri notu bulunamadı.");
            }

            //ok 200 doner
            //BadRequest 400 doner
            //NotFound 404 doner    
            return Ok(customerNote);
        }
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {

            var result = _customerNoteService.Delete(id);
            if (result == null)
            {
                return BadRequest("Silinecek not bulunamadı.");
            }
            return Ok(result);
        }


        [HttpPost("Create")]
        public IActionResult Create([FromBody] CustomerNote customerNote)
        {
            if (customerNote == null)
            {
                return BadRequest("Geçersiz müşteri notu bilgisi.");
            }

            var result = _customerNoteService.Create(customerNote);

            if (result == null)
            {
                return BadRequest("Müşteri notu oluşturulamadı.");
            }

            return Ok(result);
        }
        [HttpGet("GetByName")]
        public IActionResult GetByName(string name)
        {
            var result = _customerNoteService.GetByName(name);

            if (result == null)
            {
                return BadRequest("Geçerli bir isim giriniz.");
            }
            var response = _customerNoteService.GetByName(name);

            if (response == null )
            {
                return NotFound("Belirtilen isimde müşteri notu bulunamadı.");
            }

            return Ok(response);

        }






    }
}
