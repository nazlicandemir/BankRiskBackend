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

namespace BankRiskTracking.Business.Services
{
    public class TransactionServices : ITransaction
    {
        private readonly IGenericRepository<Transaction> _transactionRepository;
        public TransactionServices(IGenericRepository<Transaction> transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public IResponse<Transaction> Create(Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return ResponseGeneric<Transaction>.Error("Müşteri bilgileri boş bırakılamaz");
                }
                var transactionCustomer = new Transaction
                {
                    CustomerId = customer.Id,
                    Amount = 1000,
                    TransactionDate = DateTime.Now

                };
                _transactionRepository.Create(transactionCustomer);
                return ResponseGeneric<Transaction>.Success(transactionCustomer, "Başarı ile oluşturuldu");
            }
            catch
            {
                return ResponseGeneric<Transaction>.Error("Müşteri bilgileri boş bırakılamaz");
            }
        }

        public IResponse<Transaction> Delete(int id)
        {
            try
            {
                var Transaction = _transactionRepository.GetByIdAsync(id).Result;
                if (Transaction == null)
                {
                    return ResponseGeneric<Transaction>.Error("Müşteri Bulunamadı");
                }
                return ResponseGeneric<Transaction>.Success(Transaction, "Müşteri başarıyla bulundu");
            }
            catch
            {
                return ResponseGeneric<Transaction>.Error("Müşteri Bulunamadı");
            }
        }

        public IResponse<Transaction> Get(int id)
        {
            try
            {
                var Transaction = _transactionRepository.GetByIdAsync(id).Result;
                if (Transaction == null)
                {
                    return ResponseGeneric<Transaction>.Error("Müşteri bulunamadı");
                }
                return ResponseGeneric<Transaction>.Success(Transaction, "Müşteri başarıyla bulundu");
            }
            catch
            {
                return ResponseGeneric<Transaction>.Error("Müşteri bulunamadı");
            }
        }

        public IResponse<Transaction> GetByName(string name)
        {
            try
            {
                var Transaction = _transactionRepository.GetAll().FirstOrDefault(x => x.Title == name);
                if (Transaction == null)
                {
                    return ResponseGeneric<Transaction>.Error("Müşteri bulunamadı");
                }
                return ResponseGeneric<Transaction>.Success(Transaction, "Müşteri başarıyla bulundu");
            }
            catch
            {
                return ResponseGeneric<Transaction>.Error("Müşteri bulunamadı");
            }
        }

        public IResponse<IEnumerable<Transaction>> ListAll()
        {
            try
            {
                var transactionList = _transactionRepository.GetAll().ToList();
                if (transactionList == null)
                {
                    return ResponseGeneric<IEnumerable<Transaction>>.Error("Müşteri Bulunamadı");
                }
                return ResponseGeneric<IEnumerable<Transaction>>.Success(transactionList, "Müşteri başarıyla bulundu");
            }
            catch
            {
                return ResponseGeneric<IEnumerable<Transaction>>.Error("Müşteri Bulunamadı");
            }
            
        }

        public Task<IResponse<Transaction>> Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
