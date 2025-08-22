using BankRiskTracking.Entities.DTOs;
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
        private readonly ICustomerNoteService _customerNoteService;

          public CustomerNoteController(ICustomerNoteService customerNoteService)
        {
            _customerNoteService = customerNoteService;
        }

        // GET: api/CustomerNote/ListAll
        [HttpGet("ListAll")]
        public IActionResult ListAll()
        {
            var result = _customerNoteService.ListAll();
            return result.IsSuccess ? Ok(result) : NotFound(result.Message);
        }

        // GET: api/CustomerNote/{id}
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var result = _customerNoteService.GetById(id);
            return result.IsSuccess ? Ok(result) : NotFound(result.Message);
        }

        // GET: api/CustomerNote/GetByName?name=...
        [HttpGet("GetByName")]
        public IActionResult GetByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("İsim boş olamaz.");

            var result = _customerNoteService.GetByName(name);
            return result.IsSuccess ? Ok(result) : NotFound(result.Message);
        }

        // POST: api/CustomerNote/Create
        [HttpPost("Create")]
        public IActionResult Create([FromBody] CustomerNoteCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Geçersiz müşteri notu verisi.");

            var result = _customerNoteService.Create(dto);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Message);
        }

        // PUT: api/CustomerNote/Update
        [HttpPut("Update")]
        public IActionResult Update([FromBody] CustomerNoteUpdateDto dto)
        {
            if (dto == null)
                return BadRequest("Güncelleme verisi boş olamaz.");

            var result = _customerNoteService.Update(dto);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Message);
        }

        // DELETE: api/CustomerNote/{id}
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _customerNoteService.Delete(id);

            if (!result.IsSuccess)
            {
                var msg = result.Message?.ToLowerInvariant() ?? "";
                if (msg.Contains("bulunamadı"))
                    return NotFound(result.Message);

                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}

