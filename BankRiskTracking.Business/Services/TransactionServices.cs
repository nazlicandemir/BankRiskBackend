using BankRiskTracking.DataAccess.Repository;
using BankRiskTracking.Entities.Entities;
using BankRiskTracking.Entities.Interfaces;
using BankRiskTracking.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BankRiskTracking.Entities.DTOs;
using AutoMapper;


namespace BankRiskTracking.Business.Services
{
    public class TransactionServices : ITransaction
    {
        private readonly IGenericRepository<Transaction> _transactionRepository;
        private readonly IMapper _mapper;
        public TransactionServices(IGenericRepository<Transaction> transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }
        public IResponse<TransactionCreateDto> Create(TransactionCreateDto TransactionCreateDto)
        {
            try
            {
                if (TransactionCreateDto == null)
                {
                    return ResponseGeneric<TransactionCreateDto>.Error("Geçersiz müşteri verisi.");
                }
                var transactionCustomerDto = new Transaction
                {
                    TransactionId = 0,
                    Amount = 1000,
                    TransactionDate = DateTime.Now,
                    TrancationType = "Yükleme",
                    Title = "Otomatik İşlem",
                    Description = "Sistem tarafından oluşturuldu."

                };
                _transactionRepository.Create(transactionCustomerDto);
                return ResponseGeneric<TransactionCreateDto>.Success(null, "İşlem başarıyla oluşturuldu.");
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
                var Transaction = _transactionRepository.GetByIdAsync(id).Result;
                if (Transaction == null)
                {
                    return ResponseGeneric<TransactionQueryDto>.Error("Silinecek işlem bulunamadı.");
                }
                return ResponseGeneric<TransactionQueryDto>.Success(null, "İşlem başarıyla silindi.");
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
                var Transaction = _transactionRepository.GetByIdAsync(id).Result;

                var TransactionDto = _mapper.Map<TransactionQueryDto>(Transaction);



                if (TransactionDto == null)
                {
                    return ResponseGeneric<TransactionQueryDto>.Error("İşlem bulunamadı.");
                }
                return ResponseGeneric<TransactionQueryDto>.Success(null, "İşlem başarıyla getirildi.");
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
                var Transaction = _transactionRepository.GetAll().FirstOrDefault(x => x.Title == name);
                var TransactionDtos = _mapper.Map<TransactionQueryDto>(Transaction);
                if (TransactionDtos == null)
                {
                    return ResponseGeneric<TransactionQueryDto>.Error("Belirtilen isimde işlem bulunamadı.");
                }
                return ResponseGeneric<TransactionQueryDto>.Success(null, "İşlem başarıyla getirildi.");
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
                var transactionList = _transactionRepository.GetAll().ToList();
                var transactionDtos = _mapper.Map<IEnumerable<TransactionQueryDto>>(transactionList);


                if (transactionDtos  == null)
                {
                    return ResponseGeneric<IEnumerable<TransactionQueryDto>>.Error("Hiç işlem kaydı bulunamadı.");
                }
                return ResponseGeneric<IEnumerable<TransactionQueryDto>>.Success(null, "Tüm işlemler başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<IEnumerable<TransactionQueryDto>>.Error($"ListAll işleminde hata oluştu: {ex.Message}");
            }
            
        }

        public Task<IResponse<Transaction>> Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
