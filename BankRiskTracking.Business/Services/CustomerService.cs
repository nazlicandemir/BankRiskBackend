using BankRiskTracking.DataAccess.Repository;
using BankRiskTracking.Entities.Entities;
using BankRiskTracking.Entities.Interfaces;
using BankRiskTracking.Entities.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankRiskTracking.Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository<Customer> _customerRepository;

        public CustomerService(IGenericRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Task<IResponse<Customer>> Create(Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return Task.FromResult<IResponse<Customer>>(
                        ResponseGeneric<Customer>.Error("Müşteri bilgileri boş olamaz"));
                }

                _customerRepository.Create(customer);
                return Task.FromResult<IResponse<Customer>>(
                    ResponseGeneric<Customer>.Success(customer, "Müşteri bilgileri başarıyla oluşturuldu"));
            }
            catch
            {
                return Task.FromResult<IResponse<Customer>>(ResponseGeneric<Customer>.Error("Müşteri bilgileri boş olamaz"));
            }
        }

        public Task<IResponse<Customer>> Delete(int id)
        {
            try
            {
                // Önce entity var mı onu bul
                var customer = _customerRepository.GetByIdAsync(id).Result;

                if (customer == null)
                {
                    return Task.FromResult<IResponse<Customer>>(
                        ResponseGeneric<Customer>.Error("Müşteri bulunmadı"));
                }

                _customerRepository.Delete(customer);

                return Task.FromResult<IResponse<Customer>>(
                    ResponseGeneric<Customer>.Success(customer, "Müşteri başarıyla silindi"));
            }
            catch
            {
                return Task.FromResult<IResponse<Customer>>(ResponseGeneric<Customer>.Error("Müşteri bulunmadı"));
            }
        }

        public IResponse<Customer> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IResponse<Customer> GetById(int id)
        {
            try
            {
                var customer = _customerRepository.GetByIdAsync(id).Result;
                if (customer == null)
                {
                    return ResponseGeneric<Customer>.Success(null, "Müşteri bulunamadi");

                }
                return ResponseGeneric<Customer>.Success(customer, "Müşteri başarıyla bulundu");
            }
            catch
            {
                return ResponseGeneric<Customer>.Success(null, "Müşteri bulunamadi");
            }
        }

        public Task<IResponse<Customer>> GetByName(string name)
        {
            try
            {
                var customer = _customerRepository.GetAll()
                                           .Where(x => x.FullName == name)
                                           .FirstOrDefault();

                if (customer == null)
                {
                    IResponse<Customer> errorResponse = ResponseGeneric<Customer>.Error("Müşteri bulunamadı");

                    return Task.FromResult(errorResponse);
                }

                IResponse<Customer> successResponse = ResponseGeneric<Customer>.Success(customer, "Müşteriler başarıyla bulundu");

                return Task.FromResult(successResponse);
            }
            catch
            {
                IResponse<Customer> response = ResponseGeneric<Customer>.Error("Müşteri bulunamadı");
                return Task.FromResult(response);

            }
        }

        public IResponse<IEnumerable<Customer>> ListAll()
        {
            try
            {
                var allCustomer = _customerRepository.GetAll().ToList();

                if (allCustomer == null || allCustomer.Count == 0)
                {
                    return ResponseGeneric<IEnumerable<Customer>>.Error("Müşteriler bulunamadı");
                }
                return ResponseGeneric<IEnumerable<Customer>>.Success(allCustomer, "Müşteriler bulunamadı");
            }
            catch
            {
                return ResponseGeneric<IEnumerable<Customer>>.Error("Müşteriler bulunamadı");
            }
        }

        public Task<IResponse<Customer>> Update(Customer customer)
        {
            throw new System.NotImplementedException();
        }
    }
}
