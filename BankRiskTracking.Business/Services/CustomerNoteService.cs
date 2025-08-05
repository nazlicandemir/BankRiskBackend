using BankRiskTracking.DataAccess.Repository;
using BankRiskTracking.Entities.Entities;
using BankRiskTracking.Entities.Interfaces;
using BankRiskTracking.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankRiskTracking.Business.Services
{
    public class CustomerNoteService : ICustomerNoteService
    {
        //DI ile Generic Repositoryi aliyoruz 
        private readonly IGenericRepository<CustomerNote> _CustomerNoteRepository;
        public  CustomerNoteService(IGenericRepository<CustomerNote> CustomerNoteRepository)
        {
            _CustomerNoteRepository = CustomerNoteRepository;
        }

        public async Task<IResponse<CustomerNote>> Create(CustomerNote customerNote)
        {
            try
            {
                if (customerNote == null)
                {
                    return (ResponseGeneric<CustomerNote>.Error("Müşteri bilgileri boş olmaz "));
                }
                _CustomerNoteRepository.Create(customerNote);
                return (ResponseGeneric<CustomerNote>.Success(customerNote, "Müşteri başarı ıle oluşturuldu"));
            }
            catch
            {
                return (ResponseGeneric<CustomerNote>.Error("Müşteri bilgileri boş olmaz "));
            }
        }

        public Task<IResponse<CustomerNote>> Create(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse<CustomerNote>> Delete(int id)
        {
            try
            {
                var CustomerNote = _CustomerNoteRepository.GetByIdAsync(id).Result;
                if (CustomerNote == null)
                {
                    return Task.FromResult<IResponse<CustomerNote>>(ResponseGeneric<CustomerNote>.Error("Müşteri bulunamadı"));
                }
                _CustomerNoteRepository.Delete(CustomerNote);
                return Task.FromResult<IResponse<CustomerNote>>(ResponseGeneric<CustomerNote>.Success(CustomerNote, "Müşteri başarı ile silindi"));
                // Delete çağrıldığı anda her koşulda başarılı diye mesaj dönüyoruz ama gerçekten başarılı mı
            }
            catch
            {
                return Task.FromResult<IResponse<CustomerNote>>(ResponseGeneric<CustomerNote>.Error("Müşteri bulunamadı"));
            }
        }

        public IResponse<CustomerNote> GetById(int id)
        {
            try
            {
                var CustomerNote = _CustomerNoteRepository.GetByIdAsync(id).Result;
                if (CustomerNote == null)
                {
                    return ((ResponseGeneric<CustomerNote>.Success(null, "Müşteri bulunamadı")));
                }
                return ResponseGeneric<CustomerNote>.Success(CustomerNote, "Müşteri başarıyla bulundu");
            }
            catch
            {
                return ((ResponseGeneric<CustomerNote>.Success(null, "Müşteri bulunamadı")));
            }
        }

        public IResponse<CustomerNote> GetByName(string name)
        {
            try
            {
                var CustomerNotes = _CustomerNoteRepository.GetAll().Where(x => x.Name.ToLower().Contains(name.ToLower())).FirstOrDefault();

                if (CustomerNotes == null)
                {
                    return ResponseGeneric<CustomerNote>.Error("Müşteri bulunamadı");
                }
                return ResponseGeneric<CustomerNote>.Success(CustomerNotes, "Müşteri bulundu");
            }
            catch
            {
                return ResponseGeneric<CustomerNote>.Error("Müşteri bulunamadı");
            }

        }

        public IResponse<IEnumerable<CustomerNote>> ListAll()
        {
            try
            {
                var CustomerNote = _CustomerNoteRepository.GetAll().ToList();
                if (CustomerNote == null)
                {
                    return ResponseGeneric<IEnumerable<CustomerNote>>.Error("Müşteri bulunmadı");
                }
                return ResponseGeneric<IEnumerable<CustomerNote>>.Success(CustomerNote, "Müşteri bulundu");
            }
            catch
            {
                return ResponseGeneric<IEnumerable<CustomerNote>>.Error("Müşteri bulunmadı");
            }
        }

        public Task<IResponse<CustomerNote>> Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
