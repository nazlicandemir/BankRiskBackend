using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Azure;
using BankRiskTracking.Business.Services;
using BankRiskTracking.DataAccess.Repository;
using BankRiskTracking.Entities.DTOs;
using BankRiskTracking.Entities.Entities;
using BankRiskTracking.Entities.Interfaces;
using BankRiskTracking.Entities.Response;

//using AutoMapper;


namespace BankRiskTracking.Business.Services
{
    public class TransactionServices : ITransaction
    {
        private readonly IGenericRepository<Transaction> _transactionRepository;
        //private readonly IMapper _mapper;
        public TransactionServices(IGenericRepository<Transaction> transactionRepository) //IMapper mapper)
        {
            _transactionRepository = transactionRepository;
           // _mapper = mapper;
        }
        public IResponse<TransactionCreateDto> Create(TransactionCreateDto dto)
        {
            try
            {
                if (dto == null)
                    return ResponseGeneric<TransactionCreateDto>.Error("Geçersiz işlem verisi.");

                if (dto.CustomerId <= 0)
                    return ResponseGeneric<TransactionCreateDto>.Error("CustomerId geçersiz.");

                var entity = new Transaction
                {
                    CustomerId = dto.CustomerId,
                    Amount = dto.Amount,
                    TransactionDate = dto.TransactionDate == default ? DateTime.Now : dto.TransactionDate,
                    TrancationType = dto.TrancationType,
                    Title = string.IsNullOrWhiteSpace(dto.Title) ? "İşlem" : dto.Title,
                    Description = dto.Description
                };

                _transactionRepository.Create(entity);

                var resultDto = new TransactionCreateDto
                {
                    CustomerId = entity.CustomerId,
                    Amount = entity.Amount,
                    TransactionDate = entity.TransactionDate,
                    TrancationType = entity.TrancationType,
                    Title = entity.Title,
                    Description = entity.Description
                };

                return ResponseGeneric<TransactionCreateDto>.Success(resultDto, "İşlem başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<TransactionCreateDto>.Error($"İşlem oluşturulurken hata oluştu: {ex.Message}");
            }
        }


        public IResponse<TransactionQueryDto> Delete(int id)
        {
            try
            {
                var transaction = _transactionRepository.GetByIdAsync(id).Result;
                if (transaction == null)
                    return ResponseGeneric<TransactionQueryDto>.Error("Silinecek işlem bulunamadı.");

                _transactionRepository.Delete(transaction);

                var dto = new TransactionQueryDto
                {
                    Id = transaction.Id,                    
                    CustomerId = transaction.CustomerId,    
                    Amount = transaction.Amount,
                    TransactionDate = transaction.TransactionDate,
                    TrancationType = transaction.TrancationType,
                    Title = transaction.Title,
                    Description = transaction.Description
                };

                return ResponseGeneric<TransactionQueryDto>.Success(dto, "İşlem başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<TransactionQueryDto>.Error($"Silme sırasında hata oluştu: {ex.Message}");
            }
        }


        public IResponse<TransactionQueryDto> Get(int id)
        {
            try
            {
                var t = _transactionRepository.GetByIdAsync(id).Result;
                if (t == null)
                    return ResponseGeneric<TransactionQueryDto>.Error("İşlem bulunamadı.");

                var dto = new TransactionQueryDto
                {
                    Id = t.Id,                            // ✅
                    CustomerId = t.CustomerId,            // ✅
                    Amount = t.Amount,
                    TransactionDate = t.TransactionDate,
                    TrancationType = t.TrancationType,
                    Title = t.Title,
                    Description = t.Description
                };

                return ResponseGeneric<TransactionQueryDto>.Success(dto, "İşlem başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<TransactionQueryDto>.Error($"Get işleminde hata oluştu: {ex.Message}");
            }
        }


        public IResponse<TransactionQueryDto> GetByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return ResponseGeneric<TransactionQueryDto>.Error("İsim boş olamaz.");

                var t = _transactionRepository
                    .GetAll()
                    .FirstOrDefault(x => x.Customer.FullName == name); 

                if (t == null)
                    return ResponseGeneric<TransactionQueryDto>.Error("Belirtilen isimde işlem bulunamadı.");

                var dto = new TransactionQueryDto
                {
                    Id = t.Id,                       
                    CustomerId = t.CustomerId,       
                    Amount = t.Amount,
                    TransactionDate = t.TransactionDate,
                    TrancationType = t.TrancationType,
                    Title = t.Title,
                    Description = t.Description
                };

                return ResponseGeneric<TransactionQueryDto>.Success(dto, "İşlem başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<TransactionQueryDto>.Error($"GetByName işleminde hata oluştu: {ex.Message}");
            }
        }


        public IResponse<IEnumerable<TransactionQueryDto>> ListAll()
        {
            try
            {
                var list = _transactionRepository.GetAll().ToList();

                if (list == null || list.Count == 0)
                    return ResponseGeneric<IEnumerable<TransactionQueryDto>>.Error("Hiç işlem kaydı bulunamadı.");

                var dtos = list.Select(t => new TransactionQueryDto
                {
                    Id = t.Id,                           
                    CustomerId = t.CustomerId,          
                    Amount = t.Amount,
                    TransactionDate = t.TransactionDate,
                    TrancationType = t.TrancationType,
                    Title = t.Title,
                    Description = t.Description
                }).ToList();

                return ResponseGeneric<IEnumerable<TransactionQueryDto>>.Success(dtos, "Tüm işlemler başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<IEnumerable<TransactionQueryDto>>.Error($"ListAll işleminde hata oluştu: {ex.Message}");
            }
        }

      

        public IResponse<IEnumerable<TransactionQueryDto>> GetTransactionByCustomerId(int CustomerId)
        {
            try
            {
                var list = _transactionRepository.GetAll().Where(x => x.CustomerId == CustomerId).ToList();

                var dtos = list.Select(t => new TransactionQueryDto
                {
                    Id = t.Id,
                    CustomerId = t.CustomerId,
                    Amount = t.Amount,
                    TransactionDate = t.TransactionDate,    
                    TrancationType = t.TrancationType,     
                    Title = t.Title,                        
                    Description = t.Description
                }).ToList();

                if (dtos.Count == 0)                        
                    return ResponseGeneric<IEnumerable<TransactionQueryDto>>.Error("Müşteri işlemleri bulunamadı.");

                return ResponseGeneric<IEnumerable<TransactionQueryDto>>.Success(dtos, "Müşteri işlemleri başarıyla döndü.");
            }
            catch
            {
                return ResponseGeneric<IEnumerable<TransactionQueryDto>>.Error("Bir hata oluştu.");
            }
        }


        public IResponse<TransactionUpdateDto> Update(TransactionUpdateDto dto)
        {
            try
            {
                var entity = _transactionRepository.GetByIdAsync(dto.Id).Result;
                if (entity == null)
                    return ResponseGeneric<TransactionUpdateDto>.Error("Transaction bulunamadı");

                // PATCH mantığı: sadece dolu gelen alanları uygula
                if (!string.IsNullOrWhiteSpace(dto.Title))
                    entity.Title = dto.Title;

                if (!string.IsNullOrWhiteSpace(dto.Description))
                    entity.Description = dto.Description;

                if (!string.IsNullOrWhiteSpace(dto.TrancationType))
                    entity.TrancationType = dto.TrancationType;

                if (dto.Amount.HasValue)
                    entity.Amount = dto.Amount.Value;

                if (dto.TransactionDate.HasValue)
                    entity.TransactionDate = dto.TransactionDate.Value;

                if (dto.CustomerId != 0)
                    entity.CustomerId = dto.CustomerId; // ✅ doğru hedef

                _transactionRepository.Update(entity);
                _transactionRepository.SaveChange();     // ✅ repo’nda varsa çağır

                // Son haliyle dön (istenirse)
                var result = new TransactionUpdateDto
                {
                    Id = entity.Id,
                    CustomerId = entity.CustomerId,
                    Amount = entity.Amount,
                    TransactionDate = entity.TransactionDate,
                    TrancationType = entity.TrancationType,
                    Title = entity.Title,
                    Description = entity.Description
                };

                return ResponseGeneric<TransactionUpdateDto>.Success(result, "İşlem başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<TransactionUpdateDto>.Error($"Bir hata oluştu: {ex.Message}");
            }
        }

    }
}


       
