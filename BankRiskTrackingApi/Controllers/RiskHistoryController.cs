using BankRiskTracking.Business.Services;
using BankRiskTracking.Entities.Entities;
using BankRiskTracking.Entities.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankRiskTracking.Entities.DTOs;
using AutoMapper;






namespace BankRiskTrackingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiskHistoryController : ControllerBase
    {
        private readonly IRiskHistoryService _riskHistoryService;  // Tek bir servis kullanılacak
        private readonly IMapper _mapper;

        public RiskHistoryController(IRiskHistoryService riskHistoryService, IMapper mapper)
        {
            _riskHistoryService = riskHistoryService; // Bu zaten doğru şekilde enjekte ediliyor
            _mapper = mapper;
        }


        
        [HttpGet("ListAll")]
        public IActionResult GetAll()
        {
            var riskHistory = _riskHistoryService.ListAll();
            if (riskHistory == null)
            {
                return BadRequest("Hiç risk geçmişi kaydı bulunamadı.");
            }

            //ok 200 doner
            //BadRequest 400 doner
            //NotFound 404 doner    
            return Ok(riskHistory);
        }
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {

            var result = _riskHistoryService.Delete(id);
            if (result == null)
            {
                return BadRequest("Silinecek risk geçmişi bulunamadı.");
            }
            return Ok(result);
        }

        [HttpPost("Create")]
        
        public IActionResult Create([FromBody] RiskHistroyCreateDto riskHistory)
        {
            if (riskHistory == null)
                return BadRequest("Geçersiz risk geçmişi verisi.");

            // DTO'yu doğrudan servise gönder
            var result = _riskHistoryService.Create(riskHistory);

            if (result == null)
                return BadRequest("Risk geçmişi oluşturulamadı.");

            return Ok(result);
        }

        [HttpGet("GetByName")]  
        public IActionResult GetByName(string name)
        {
            var result = _riskHistoryService.GetByName(name);

            if (result == null)
            {
                return BadRequest("Belirtilen isme sahip risk geçmişi bulunamadı.");
            }
            return Ok(result);

        }



    }
}
