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
                    return (ResponseGeneric<CustomerNote>.Error("CustomerNote nesnesi boş olamaz."));
                }
                _CustomerNoteRepository.Create(customerNote);
                return (ResponseGeneric<CustomerNote>.Success(customerNote, "CustomerNote başarıyla oluşturuldu."));
            }
            catch (Exception ex)
            {
                return (ResponseGeneric<CustomerNote>.Error($"Create işlemi sırasında hata oluştu: {ex.Message}"));
            }
        }

       

        public Task<IResponse<CustomerNote>> Delete(int id)
        {
            try
            {
                var CustomerNote = _CustomerNoteRepository.GetByIdAsync(id).Result;
                if (CustomerNote == null)
                {
                    return Task.FromResult<IResponse<CustomerNote>>(ResponseGeneric<CustomerNote>.Error("Silinecek not bulunamadı."));
                }
                _CustomerNoteRepository.Delete(CustomerNote);
                return Task.FromResult<IResponse<CustomerNote>>(ResponseGeneric<CustomerNote>.Success(CustomerNote, "Not başarıyla silindi."));
                // Delete çağrıldığı anda her koşulda başarılı diye mesaj dönüyoruz ama gerçekten başarılı mı
            }
            catch (Exception ex)
            {
                return Task.FromResult<IResponse<CustomerNote>>(ResponseGeneric<CustomerNote>.Error($"Delete işlemi sırasında hata oluştu: {ex.Message}"));
            }
        }

        public IResponse<CustomerNote> GetById(int id)
        {
            try
            {
                var CustomerNote = _CustomerNoteRepository.GetByIdAsync(id).Result;
                if (CustomerNote == null)
                {
                    return ((ResponseGeneric<CustomerNote>.Success(null, "Not bulunamadı.")));
                }
                return ResponseGeneric<CustomerNote>.Success(CustomerNote, "Not başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return ((ResponseGeneric<CustomerNote>.Success(null, $"GetById hatası: {ex.Message}")));
            }
        }

        public IResponse<CustomerNote> GetByName(string name)
        {
            try
            {
                var CustomerNotes = _CustomerNoteRepository.GetAll().Where(x => x.Name.ToLower().Contains(name.ToLower())).FirstOrDefault();

                if (CustomerNotes == null)
                {
                    return ResponseGeneric<CustomerNote>.Error("İsimle eşleşen not bulunamadı.");
                }
                return ResponseGeneric<CustomerNote>.Success(CustomerNotes, "Not başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<CustomerNote>.Error($"GetByName hatası: {ex.Message}");
            }

        }

        public IResponse<IEnumerable<CustomerNote>> ListAll()
        {
            try
            {
                var CustomerNote = _CustomerNoteRepository.GetAll().ToList();
                if (CustomerNote == null)
                {
                    return ResponseGeneric<IEnumerable<CustomerNote>>.Error("Hiç not bulunamadı");
                }
                return ResponseGeneric<IEnumerable<CustomerNote>>.Success(CustomerNote, "Tüm notlar listelendi.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<IEnumerable<CustomerNote>>.Error($"ListAll hatası: {ex.Message}");
            }
        }

        public Task<IResponse<CustomerNote>> Update(CustomerNote customerNote)
        {
            throw new NotImplementedException();
        }
    }
}
