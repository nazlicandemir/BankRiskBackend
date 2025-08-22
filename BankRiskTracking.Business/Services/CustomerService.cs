//using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankRiskTracking.DataAccess.Repository;
using BankRiskTracking.Entities.DTOs;
using BankRiskTracking.Entities.Entities;
using BankRiskTracking.Entities.Interfaces;
using BankRiskTracking.Entities.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;



namespace BankRiskTracking.Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        //private readonly IMapper _mapper;

        public CustomerService(IGenericRepository<Customer> customerRepository) //IMapper mapper)
        {
            _customerRepository = customerRepository;
        }

        public IResponse<Customer> Create(CustomerCreateDto customer)
        {
            try
            {
                if (customer == null)
                    return ResponseGeneric<Customer>.Error("Geçersiz müşteri verisi.");

                if (string.IsNullOrWhiteSpace(customer.FullName))
                    return ResponseGeneric<Customer>.Error("Ad-soyad boş olamaz.");

                if (string.IsNullOrWhiteSpace(customer.IdentityNumber))
                    return ResponseGeneric<Customer>.Error("Kimlik numarası boş olamaz.");

                if (customer.TotalDebt < 0)
                    return ResponseGeneric<Customer>.Error("Toplam borç negatif olamaz.");

                // Aynı kimlik numarası var mı?
                bool exists = _customerRepository.GetAll()
                    .Any(c => c.IdentityNumber == customer.IdentityNumber);
                if (exists)
                    return ResponseGeneric<Customer>.Error("Bu kimlik numarasıyla kayıt zaten var.");

                
                var newCustomer = new Customer
                {
                    FullName = customer.FullName.Trim(),
                    IdentityNumber = customer.IdentityNumber.Trim(),
                    TotalDebt = customer.TotalDebt,
                    CreditScore = customer.CreditScore,
                    RiskLevel = customer.RiskLevel,
                    Notes = customer.Notes,
                    LastEvaluated = customer.LastEvaluated,


                };

                _customerRepository.Create(newCustomer);

                // İstersen entity yerine bir QueryDto döndürebilirsin; şimdilik entity döndürüyorum
                return ResponseGeneric<Customer>.Success(newCustomer, "Müşteri başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<Customer>.Error($"Müşteri oluşturulurken hata oluştu: {ex.Message}");
            }
        }


        public IResponse<Customer> Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return ResponseGeneric<Customer>.Error("Geçersiz Id.");

                // id ile doğrudan getiriyoruz
                var customer = _customerRepository.GetByIdAsync(id).Result;
                if (customer == null)
                    return ResponseGeneric<Customer>.Error("Silinecek müşteri bulunamadı.");

                // Silip ardından kaydediyoruz
                _customerRepository.Delete(customer);
                _customerRepository.SaveChange();

                return ResponseGeneric<Customer>.Success(customer, "Müşteri başarıyla silindi.");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return ResponseGeneric<Customer>.Error($"Eşzamanlılık hatası: {ex.Message}");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<Customer>.Error($"Silme işleminde beklenmeyen hata: {ex.Message}");
            }
        }



        public IResponse<Customer> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IResponse<CustomerQueryDto> GetById(int id)
        {
            try
            {
                var customer = _customerRepository.GetByIdAsync(id).Result;
                

                if (customer == null)
                {
                    return ResponseGeneric<CustomerQueryDto>.Success(null, "Müşteri bulunamadi");

                }
                //var dto = _mapper.Map<CustomerQueryDto>(customer);

                var dto = new CustomerQueryDto { FullName = customer.FullName };

                return ResponseGeneric<CustomerQueryDto>.Success(dto , "Müşteri başarıyla getirildi");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<CustomerQueryDto>.Success(null, $"GetById işleminde hata oluştu: {ex.Message}");
            }
        }

        public Task<IResponse<CustomerQueryDto>> GetByName(string name)
        {
            try
            {
                var customer = _customerRepository.GetAll()
                    .FirstOrDefault(x => x.FullName == name);

                if (customer == null)
                {
                    IResponse<CustomerQueryDto> errorResponse =
                        ResponseGeneric<CustomerQueryDto>.Error("Müşteri bulunamadı");
                    return Task.FromResult(errorResponse);
                }

                var customerQueryDto = new CustomerQueryDto
                {
                    FullName = customer.FullName
                };

                IResponse<CustomerQueryDto> successResponse =
                    ResponseGeneric<CustomerQueryDto>.Success(customerQueryDto, "Müşteriler başarıyla getirildi");

                return Task.FromResult(successResponse);
            }
            catch (Exception ex)
            {
                IResponse<CustomerQueryDto> response = ResponseGeneric<CustomerQueryDto>.Error($"GetByName işleminde hata oluştu: {ex.Message}");
                return Task.FromResult(response);

            }
        }

        public IResponse<IEnumerable<CustomerQueryDto>> ListAll()
        {
            try
            {
                var allCustomer = _customerRepository.GetAll().ToList();

                // var customerQueryDto = _mapper.Map<IEnumerable<CustomerQueryDto>>(allCustomer);

                // Listeyi elle maplemek için bir liste oluştur
                var customerQueryDtoList = new List<CustomerQueryDto>();

                // allCustomer listesindeki her bir Customer nesnesini CustomerQueryDto'ya dönüştür
                foreach (var customer in allCustomer)
                {
                    var customerQueryDto = new CustomerQueryDto
                    {
                        FullName = customer.FullName 
                    };
                    customerQueryDtoList.Add(customerQueryDto);
                }

                if (allCustomer == null || allCustomer.Count == 0)
                {
                    return ResponseGeneric<IEnumerable<CustomerQueryDto>>.Error("Hiç müşteri kaydı bulunamadı.");
                }
                return ResponseGeneric<IEnumerable<CustomerQueryDto>>.Success(customerQueryDtoList, "Müşteri listesi başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<IEnumerable<CustomerQueryDto>>.Error($"ListAll işleminde hata oluştu: {ex.Message}");
            }
        }

        public IResponse<CustomerUpdateDto> Update(CustomerUpdateDto customerUpdateDto)
        {
            try
            {
               
                var entity = _customerRepository.GetByIdAsync(customerUpdateDto.Id).Result;
                if (entity == null)
                    return ResponseGeneric<CustomerUpdateDto>.Error("Müşteri bulunamadı.");

              
                if (customerUpdateDto.FullName != null)
                    entity.FullName = customerUpdateDto.FullName;

                if (customerUpdateDto.IdentityNumber != null)
                    entity.IdentityNumber = customerUpdateDto.IdentityNumber;

                if (customerUpdateDto.TotalDebt != 0)
                    entity.TotalDebt = customerUpdateDto.TotalDebt;

                if (customerUpdateDto.CreditScore != 0)
                    entity.CreditScore = customerUpdateDto.CreditScore;

                if (customerUpdateDto.RiskLevel != 0)
                    entity.RiskLevel = customerUpdateDto.RiskLevel;

                if (customerUpdateDto.Notes != null)
                    entity.Notes = customerUpdateDto.Notes;

                if (customerUpdateDto.LastEvaluated != default)
                    entity.LastEvaluated = customerUpdateDto.LastEvaluated;

                // tamamen senkron çağrılar
                _customerRepository.Update(entity);
                _customerRepository.SaveChange();

                return ResponseGeneric<CustomerUpdateDto>.Success(customerUpdateDto, "Müşteri başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<CustomerUpdateDto>.Error($"Update işleminde hata: {ex.Message}");
            }
        }

    }


}

