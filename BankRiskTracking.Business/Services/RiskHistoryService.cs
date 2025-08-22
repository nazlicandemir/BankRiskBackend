//using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankRiskTracking.DataAccess.Repository;
using BankRiskTracking.Entities.DTOs;
using BankRiskTracking.Entities.Entities;
using BankRiskTracking.Entities.Interfaces;
using BankRiskTracking.Entities.Response;
using Braintree;
using Microsoft.Extensions.Logging;




namespace BankRiskTracking.Business.Services
{
    public class RiskHistoryService : IRiskHistoryService
    {
        private readonly IGenericRepository<RiskHistory> _riskHistoryRepository;
        //private readonly IMapper _mapper; 
        private readonly ILogger<RiskHistoryService> _logger;

        public RiskHistoryService(IGenericRepository<RiskHistory> riskHistoryRepository, /*IMapper mapper*/ ILogger<RiskHistoryService> logger)
        {
            _riskHistoryRepository = riskHistoryRepository;
            //_mapper = mapper;
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
                    CustomerId = riskHistory.CustomerId,
                    RiskLevel = riskHistory.RiskLevel,
                    Title = riskHistory.Title,
                    Notes = riskHistory.Notes,
                    EvaluatedDate = DateTime.Now
                };

                //var riskHistoryDto = _mapper.Map<RiskHistroyCreateDto>(riskHistoryCustomer);


                _riskHistoryRepository.Create(riskHistoryCustomer);

                var riskHistoryDto = new RiskHistroyCreateDto
                {
                    CustomerId = riskHistoryCustomer.CustomerId,
                    Title = riskHistoryCustomer.Title,
                    RiskLevel = riskHistoryCustomer.RiskLevel,
                    EvaluatedDate = riskHistoryCustomer.EvaluatedDate,
                    Notes = riskHistoryCustomer.Notes
                };

                return ResponseGeneric<RiskHistroyCreateDto>.Success(riskHistoryDto, "Risk geçmişi başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<RiskHistroyCreateDto>.Error($"Create işleminde hata oluştu: {ex.Message}");
            }
        }

        public IResponse<RiskHistoryQueryDto> Delete(int customerId)
        {
            try
            {
                var RiskHistoryDto = _riskHistoryRepository
                    .GetAll()
                    .FirstOrDefault(x => x.CustomerId == customerId);

                if (RiskHistoryDto == null)
                {
                    return ResponseGeneric<RiskHistoryQueryDto>.Error("Silinecek risk geçmişi kaydı bulunamadı.");
                }
                _riskHistoryRepository.Delete(RiskHistoryDto);
                var riskHistoryDto = new RiskHistoryQueryDto
                {
                    Id =RiskHistoryDto.Id,
                    CustomerId = RiskHistoryDto.CustomerId,  // CustomerId'yi alıyoruz
                    Title = RiskHistoryDto.Title,
                    RiskLevel = RiskHistoryDto.RiskLevel,
                    EvaluatedDate = RiskHistoryDto.EvaluatedDate,
                    Notes = RiskHistoryDto.Notes
                };

                return ResponseGeneric<RiskHistoryQueryDto>.Success(riskHistoryDto, "Risk geçmişi başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<RiskHistoryQueryDto>.Error($"Delete işleminde hata oluştu: {ex.Message}");
            }
        }

       /* public IResponse<RiskHistoryQueryDto> Get(int id)
        {
            try
            {
                var riskHistory = _riskHistoryRepository.GetByIdAsync(id).Result;
                if (riskHistory == null)
                {
                    return ResponseGeneric<RiskHistoryQueryDto>.Error("Risk geçmişi kaydı bulunamadı.");
                }

                var riskHistoryDto = new RiskHistoryQueryDto
                {
                    Id = riskHistory.Id,
                    CustomerIdi = riskHistory.CustomerId,  // CustomerId
                    Title = riskHistory.Title,
                    RiskLevel = riskHistory.RiskLevel,
                    EvaluatedDate = riskHistory.EvaluatedDate,
                    Notes = riskHistory.Notes
                };

                return ResponseGeneric<RiskHistoryQueryDto>.Success(riskHistoryDto, "Risk geçmişi başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<RiskHistoryQueryDto>.Success(null, $"Get işleminde hata oluştu: {ex.Message}");
            }
        }*/

        public IResponse<RiskHistoryQueryDto> GetByCustomerId(int customerId)
        {
            try
            {
                var riskHistory = _riskHistoryRepository
                    .GetAll()
                    .FirstOrDefault(x => x.CustomerId == customerId);

                if (riskHistory == null)
                    return ResponseGeneric<RiskHistoryQueryDto>.Error("Bu müşteri için risk geçmişi bulunamadı.");

                var dto = new RiskHistoryQueryDto
                {
                    Id = riskHistory.Id,
                    CustomerId = riskHistory.CustomerId,
                    Title = riskHistory.Title,
                    RiskLevel = riskHistory.RiskLevel,
                    EvaluatedDate = riskHistory.EvaluatedDate,
                    Notes = riskHistory.Notes
                };

                return ResponseGeneric<RiskHistoryQueryDto>.Success(dto, "Kayıt başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<RiskHistoryQueryDto>.Error($"GetByCustomerId işleminde hata oluştu: {ex.Message}");
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
                var riskHistoryListDtos = riskHistoryListDto.Select(riskHistory => new RiskHistoryQueryDto
                {
                    Id = riskHistory.Id,
                    CustomerId = riskHistory.CustomerId,  // CustomerId
                    Title = riskHistory.Title,
                    RiskLevel = riskHistory.RiskLevel,
                    EvaluatedDate = riskHistory.EvaluatedDate,
                    Notes = riskHistory.Notes
                }).ToList();


                return ResponseGeneric<IEnumerable<RiskHistoryQueryDto>>.Success(riskHistoryListDtos, "Tüm risk geçmişleri başarıyla listelendi.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<IEnumerable<RiskHistoryQueryDto>>.Error($"ListAll işleminde hata oluştu: {ex.Message}");
            }
        }

  

        public IResponse<RiskHistoryUpdateDto> Update(RiskHistoryUpdateDto riskHistoryUpdateDto)
        {

            try
            {
                var entity = _riskHistoryRepository.GetByIdAsync(riskHistoryUpdateDto.Id).Result;
                if (entity == null)
                    return ResponseGeneric<RiskHistoryUpdateDto>.Error("Risk geçmişi bulunamadı.");

                // Elle mapleme - sadece dolu gelen alanlar güncellenir
                if (riskHistoryUpdateDto.CustomerId != 0)
                    entity.CustomerId = riskHistoryUpdateDto.CustomerId;

                if (!string.IsNullOrWhiteSpace(riskHistoryUpdateDto.Title))
                    entity.Title = riskHistoryUpdateDto.Title;

                if (riskHistoryUpdateDto.RiskLevel != default) // enum default değilse
                    entity.RiskLevel = riskHistoryUpdateDto.RiskLevel;

                if (riskHistoryUpdateDto.EvaluatedDate != default)
                    entity.EvaluatedDate = riskHistoryUpdateDto.EvaluatedDate;

                if (riskHistoryUpdateDto.Notes != null)
                    entity.Notes = riskHistoryUpdateDto.Notes;

                

                // Kaydet
                _riskHistoryRepository.Update(entity);
                _riskHistoryRepository.SaveChange();

                return ResponseGeneric<RiskHistoryUpdateDto>.Success(riskHistoryUpdateDto, "Risk geçmişi başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<RiskHistoryUpdateDto>.Error($"Update işleminde hata: {ex.Message}");
            }

        }

    }
}




  

