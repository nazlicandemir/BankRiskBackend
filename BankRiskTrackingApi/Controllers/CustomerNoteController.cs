using BankRiskTracking.Entities.Interfaces;
using Microsoft.AspNetCore.Http;
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
            if(customerNote == null)
            {
                return BadRequest("Müşteri bulunamadı ");
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
            if(result == null)
            {
                return BadRequest("Silme işlemi başarısız oldu");
            }
            return Ok(result);
        }

    }
}
