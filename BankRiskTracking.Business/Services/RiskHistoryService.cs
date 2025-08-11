using AutoMapper;
using BankRiskTracking.DataAccess.Repository;
using BankRiskTracking.Entities.Entities;
using BankRiskTracking.Entities.Interfaces;
using BankRiskTracking.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankRiskTracking.Entities.DTOs;
using Microsoft.Extensions.Logging;




namespace BankRiskTracking.Business.Services
{
    public  class RiskHistoryService : IRiskHistoryService
    {
        private readonly IGenericRepository<RiskHistory> _riskHistoryRepository;
        private readonly IMapper _mapper; 
        private readonly ILogger<RiskHistoryService> _logger;
       
        public RiskHistoryService(IGenericRepository<RiskHistory> riskHistoryRepository, IMapper mapper, ILogger<RiskHistoryService> logger)
        {
            _riskHistoryRepository = riskHistoryRepository;
            _mapper = mapper;
            _logger = logger;
        }


        public IResponse<RiskHistroyCreateDto> Create(RiskHistroyCreateDto riskHistory)
        {
            try
            {
                if (riskHistory == null)
                {
                    return ResponseGeneric<RiskHistroyCreateDto>.Error("Risk geçmişi verisi boş olamaz.");
                }
                var riskHistoryCustomer = new RiskHistory
                {
                    CustomerId = riskHistory.CustomerIdi,
                    RiskLevel = riskHistory.RiskLevel,
                    Title = riskHistory.Title,
                    Notes = riskHistory.Notes,
                    EvaluatedDate = DateTime.Now
                };

                var riskHistoryDto = _mapper.Map<RiskHistroyCreateDto>(riskHistoryCustomer);

                _riskHistoryRepository.Create(riskHistoryCustomer);
                return ResponseGeneric<RiskHistroyCreateDto>.Success(riskHistoryDto, "Risk geçmişi başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<RiskHistroyCreateDto>.Error($"Create işleminde hata oluştu: {ex.Message}");
            }
        }

        public IResponse<RiskHistoryQueryDto> Delete(int id)
        {
            try
            {
                var RiskHistoryDto = _riskHistoryRepository.GetByIdAsync(id).Result;
                if (RiskHistoryDto == null)
                {
                    return ResponseGeneric<RiskHistoryQueryDto>.Error("Silinecek risk geçmişi kaydı bulunamadı.");
                }
                _riskHistoryRepository.Delete(RiskHistoryDto);
                return ResponseGeneric<RiskHistoryQueryDto>.Success(null, "Risk geçmişi başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<RiskHistoryQueryDto>.Error($"Delete işleminde hata oluştu: {ex.Message}");
            }
        }

        public IResponse<RiskHistoryQueryDto> Get(int id)
        {
            try
            {
                var RiskHistory = _riskHistoryRepository.GetByIdAsync(id).Result;
                if (RiskHistory == null)
                {
                    return ResponseGeneric<RiskHistoryQueryDto>.Success(null, "Risk geçmişi kaydı bulunamadı.");
                }

                return ResponseGeneric<RiskHistoryQueryDto>.Success(null, "Risk geçmişi başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<RiskHistoryQueryDto>.Success(null, $"Get işleminde hata oluştu: {ex.Message}");
            }
        }

        public IResponse<RiskHistoryQueryDto> GetByName(string name)
        {
            try
            {
                var riskHistory = _riskHistoryRepository.GetAll().FirstOrDefault(x => x.Title == name);
                if (riskHistory == null)
                {
                    return ResponseGeneric<RiskHistoryQueryDto>.Error("Belirtilen ada sahip risk geçmişi bulunamadı.");
                }
                return ResponseGeneric<RiskHistoryQueryDto>.Success(null, "Risk geçmişi başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<RiskHistoryQueryDto>.Error($"GetByName işleminde hata oluştu: {ex.Message}");
            }
        }

        public IResponse<IEnumerable<RiskHistoryQueryDto>> ListAll()
        {
            try
            {
                var riskHistoryListDto = _riskHistoryRepository.GetAll().ToList();
                if (riskHistoryListDto == null)
                {
                    return ResponseGeneric<IEnumerable<RiskHistoryQueryDto>>.Error("Hiç risk geçmişi kaydı bulunamadı.");
                }
                return ResponseGeneric<IEnumerable<RiskHistoryQueryDto>>.Success(null, "Tüm risk geçmişleri başarıyla listelendi.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<IEnumerable<RiskHistoryQueryDto>>.Error($"ListAll işleminde hata oluştu: {ex.Message}");
            }
        }

        public IResponse<IEnumerable<RiskHistoryQueryDto>> GetCustomerByCustomerId(int CustomerId)
        {
            try
            {
                var risksInCustomer = _riskHistoryRepository.GetAll().Where(x => x.CustomerId == CustomerId).ToList();
                return null;
            }catch 
            {
                return ResponseGeneric<IEnumerable<RiskHistoryQueryDto>>.Error("Bir hata oluştu ");
            }
        }





        public IResponse<RiskHistory> Update(RiskHistory riskHistory)
        {
            throw new NotImplementedException();
        }
    }
}
