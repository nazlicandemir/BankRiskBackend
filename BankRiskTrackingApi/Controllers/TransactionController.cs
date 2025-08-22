using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankRiskTracking.Entities.Interfaces;
using BankRiskTracking.Business.Services;
using BankRiskTracking.Entities.Entities;
using BankRiskTracking.Entities.DTOs;



namespace BankRiskTrackingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        //Dependency Injection 
        private readonly ITransaction _transaction;
        public TransactionController(ITransaction transaction)
        {
            _transaction = transaction;
        }
        [HttpGet("ListAll")]
        public IActionResult GetAll()
        {
            var customer = _transaction.ListAll();
            if (customer == null)
            {
                return BadRequest("Hiç işlem kaydı bulunamadı.");
            }
            return Ok(customer);
        }
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {

            var result = _transaction.Delete(id);
            if (result == null)
            {
                return BadRequest("Silinecek işlem kaydı bulunamadı.");
            }
            return Ok(result);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] TransactionCreateDto TransactionCreateDto)
        {
            if (TransactionCreateDto  == null)
            {
                return BadRequest("Geçersiz müşteri verisi.");
            }

            var result = _transaction.Create(TransactionCreateDto);
            if (result == null)
            {
                return BadRequest("İşlem oluşturulamadı.");
            }
            return Ok(result);
        }
        [HttpGet("GetByName")]
        public IActionResult GetByName(string name)
        {
            var result = _transaction.GetByName(name);
            if (result == null)
            {
                return BadRequest("Belirtilen isimde işlem bulunamadı.");
            }
            return Ok(result);
        }

       
        [HttpGet("GetTransactionByCustomerId")]

        public IActionResult GetTransactionByCustomerId (int CustomerId)
        {
            var result = _transaction.GetTransactionByCustomerId(CustomerId);
            if (!result.IsSuccess)
            {
                return NotFound("İşlem Bulunamadı");
            }
            return Ok(result);
        }
        [HttpPut("Update")]
        public IActionResult Update([FromBody] TransactionUpdateDto transactionUpdateDto)
        {
            if (transactionUpdateDto == null)
            {
                return BadRequest("Müşteri bilgileri boş olamaz");
            }
            var result = _transaction.Update(transactionUpdateDto);
            if (!result.IsSuccess)
            {
                return BadRequest("Müşteri güncellenemedi");
            }
            return Ok(result); 
        }


    }
}
