using AutoMapper;
using BankRiskTracking.DataAccess.Repository;
using BankRiskTracking.Entities.DTOs;
using BankRiskTracking.Entities.Entities;
using BankRiskTracking.Entities.Interfaces;
using BankRiskTracking.Entities.Response;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace BankRiskTracking.Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(IGenericRepository<Customer> customerRepository,IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public IResponse<Customer>Create(CustomerCreateDto customer)
        {
            try
            {
                if (customer == null)
                {
                    return (ResponseGeneric<Customer>.Error("Geçersiz müşteri verisi."));
                }
                var newCustomer = new Customer
                {
                    FullName = customer.FullName

                };

                _customerRepository.Create(newCustomer);
                return ( ResponseGeneric<Customer>.Success(null, "Müşteri başarıyla oluşturuldu."));
            }
            catch (Exception ex)
            {
                return (ResponseGeneric<Customer>.Error($"Müşteri oluşturulurken hata oluştu: {ex.Message}"));
            }
        }

        public IResponse<Customer> Delete(int id)
        {
            try
            {
                // Önce entity var mı onu bul
                var customer = _customerRepository.GetByIdAsync(id).Result;

                if (customer == null)
                {
                    return 
                        ResponseGeneric<Customer>.Error("Silinecek müşteri bulunamadı.");
                }

                _customerRepository.Delete(customer);

                return 
                    ResponseGeneric<Customer>.Success(customer, "Müşteri başarıyla silindi");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<Customer>.Error($"Silme işlemi sırasında hata oluştu: {ex.Message}");
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
                                           .Where(x => x.FullName == name)
                                           .FirstOrDefault();
                var CustomerQueryDto = _mapper.Map<CustomerQueryDto>(customer);

                if (customer == null)
                {
                    IResponse<CustomerQueryDto> errorResponse = ResponseGeneric<CustomerQueryDto>.Error("Müşteri bulunamadı");

                    return Task.FromResult(errorResponse);
                }

                IResponse<CustomerQueryDto> successResponse = ResponseGeneric<CustomerQueryDto>.Success(CustomerQueryDto, "Müşteriler başarıyla getirildi");

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
                var customerQueryDto = _mapper.Map<IEnumerable<CustomerQueryDto>>(allCustomer);

                if (allCustomer == null || allCustomer.Count == 0)
                {
                    return ResponseGeneric<IEnumerable<CustomerQueryDto>>.Error("Hiç müşteri kaydı bulunamadı.");
                }
                return ResponseGeneric<IEnumerable<CustomerQueryDto>>.Success(customerQueryDto, "Müşteri listesi başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<IEnumerable<CustomerQueryDto>>.Error($"ListAll işleminde hata oluştu: {ex.Message}");
            }
        }

        public Task<IResponse<Customer>> Update(Customer customer)
        {
            throw new System.NotImplementedException();
        }

    }
}
