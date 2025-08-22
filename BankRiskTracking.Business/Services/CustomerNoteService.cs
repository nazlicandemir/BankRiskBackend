using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankRiskTracking.DataAccess.Repository;
using BankRiskTracking.Entities.Entities;
using BankRiskTracking.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using BankRiskTracking.Entities.Response;
using BankRiskTracking.Entities.DTOs;
namespace BankRiskTracking.Business.Services

{
    public class CustomerNoteService : ICustomerNoteService
    {
        //DI ile Generic Repositoryi aliyoruz 
        private readonly IGenericRepository<CustomerNote> _CustomerNoteRepository;
        private readonly IGenericRepository<Customer> _customerRepository;
        public  CustomerNoteService(IGenericRepository<CustomerNote> CustomerNoteRepository, IGenericRepository<Customer> customerRepository)
        {
            _CustomerNoteRepository = CustomerNoteRepository;
            _customerRepository = customerRepository;
        }

        public IResponse<CustomerNoteQueryDto> Create(CustomerNoteCreateDto customerNote)
        {
            try
            {
                Console.WriteLine($"[Create] Gelen CustomerId = {customerNote?.CustomerId}");

                if (customerNote == null)
                    return ResponseGeneric<CustomerNoteQueryDto>.Error("CustomerNote nesnesi boş olamaz.");

                if (customerNote.CustomerId <= 0)
                    return ResponseGeneric<CustomerNoteQueryDto>.Error($"Geçersiz CustomerId (<=0). Gelen: {customerNote.CustomerId}");

                // Customer tablosunda var mi?
                var customerExists = _customerRepository.GetAll().Any(c => c.Id == customerNote.CustomerId);
                if (!customerExists)
                    return ResponseGeneric<CustomerNoteQueryDto>.Error($"Geçersiz CustomerId (DB'de yok). Gelen: {customerNote.CustomerId}");

                var name = customerNote.Name?.Trim();
                var noteText = customerNote.NoteText?.Trim();

                if (string.IsNullOrWhiteSpace(name))
                    return ResponseGeneric<CustomerNoteQueryDto>.Error("Name boş olamaz.");
                if (string.IsNullOrWhiteSpace(noteText))
                    return ResponseGeneric<CustomerNoteQueryDto>.Error("NoteText boş olamaz.");

                var createdUtc = customerNote.CreatedDate == default
                    ? DateTime.UtcNow
                    : (customerNote.CreatedDate.Kind == DateTimeKind.Unspecified
                        ? DateTime.SpecifyKind(customerNote.CreatedDate, DateTimeKind.Utc)
                        : customerNote.CreatedDate.ToUniversalTime());

                var entity = new CustomerNote
                {
                    Name = name,
                    CustomerId = customerNote.CustomerId,
                    NoteText = noteText,
                    CreatedDate = createdUtc
                };

                _CustomerNoteRepository.Create(entity);
                _CustomerNoteRepository.SaveChange();

                var result = new CustomerNoteQueryDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    CustomerId = entity.CustomerId,
                    NoteText = entity.NoteText,
                    CreatedDate = entity.CreatedDate
                };

                return ResponseGeneric<CustomerNoteQueryDto>.Success(result, "CustomerNote başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<CustomerNoteQueryDto>.Error($"Create işlemi sırasında hata oluştu: {ex.Message}");
            }
        }



        // DELETE
        public IResponse<CustomerNoteQueryDto> Delete(int id)
        {
            try
            {
                var entity = _CustomerNoteRepository.GetByIdAsync(id).Result;
                if (entity == null)
                    return ResponseGeneric<CustomerNoteQueryDto>.Error("Silinecek not bulunamadı.");

                _CustomerNoteRepository.Delete(entity);
                _CustomerNoteRepository.SaveChange();

                var dto = new CustomerNoteQueryDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    CustomerId = entity.CustomerId,
                    NoteText = entity.NoteText,
                    CreatedDate = entity.CreatedDate   
                };

                return ResponseGeneric<CustomerNoteQueryDto>.Success(dto, "Not başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<CustomerNoteQueryDto>.Error($"Delete işlemi sırasında hata oluştu: {ex.Message}");
            }
        }

        
        public IResponse<CustomerNoteQueryDto> GetById(int id)
        {
            try
            {
                var entity = _CustomerNoteRepository.GetByIdAsync(id).Result;
                if (entity == null)
                    return ResponseGeneric<CustomerNoteQueryDto>.Error("Not bulunamadı.");

                var dto = new CustomerNoteQueryDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    CustomerId = entity.CustomerId,
                    NoteText = entity.NoteText,
                    CreatedDate = entity.CreatedDate
                };

                return ResponseGeneric<CustomerNoteQueryDto>.Success(dto, "Not başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<CustomerNoteQueryDto>.Error($"GetById hatası: {ex.Message}");
            }
        }

        public IResponse<CustomerNoteQueryDto> GetByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return ResponseGeneric<CustomerNoteQueryDto>.Error("İsim boş olamaz.");

                var nn = name.Trim().ToLower(); // Npgsql -> SQL'de lower(...) olarak çevrilir

                var note = _CustomerNoteRepository.GetAll()              // IQueryable olmalı
                    .Where(n => n.Name != null && n.Name.ToLower().Contains(nn)) // kısmi arama
                    .Select(n => new CustomerNoteQueryDto
                    {
                        Id = n.Id,
                        Name = n.Name,
                        CustomerId = n.CustomerId,
                        NoteText = n.NoteText,
                        CreatedDate = n.CreatedDate
                    })
                    .FirstOrDefault(); // IEnumerable yok, tek kayıt

                if (note == null)
                    return ResponseGeneric<CustomerNoteQueryDto>.Error("Kayıt bulunamadı.");

                return ResponseGeneric<CustomerNoteQueryDto>.Success(note, "Kayıt getirildi.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<CustomerNoteQueryDto>.Error($"GetByName hatası: {ex.Message}");
            }
        }




        public IResponse<IEnumerable<CustomerNoteQueryDto>> ListAll()
        {
            try
            {
                var entities = _CustomerNoteRepository.GetAll().ToList();
                if (entities == null || entities.Count == 0)
                    return ResponseGeneric<IEnumerable<CustomerNoteQueryDto>>.Error("Hiç not bulunamadı.");

                var dtoList = entities.Select(e => new CustomerNoteQueryDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    CustomerId = e.CustomerId,
                    NoteText = e.NoteText,
                    CreatedDate = e.CreatedDate
                }).ToList();

                return ResponseGeneric<IEnumerable<CustomerNoteQueryDto>>.Success(dtoList, "Tüm notlar listelendi.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<IEnumerable<CustomerNoteQueryDto>>.Error($"ListAll hatası: {ex.Message}");
            }
        }

        
        public IResponse<CustomerNoteUpdateDto> Update(CustomerNoteUpdateDto dto)
        {
            try
            {
                if (dto == null)
                    return ResponseGeneric<CustomerNoteUpdateDto>.Error("Güncelleme verisi boş olamaz.");

                var entity = _CustomerNoteRepository.GetByIdAsync(dto.Id).Result;
                if (entity == null)
                    return ResponseGeneric<CustomerNoteUpdateDto>.Error("Not bulunamadı.");

                if (!string.IsNullOrWhiteSpace(dto.Name))
                    entity.Name = dto.Name.Trim();

                if (!string.IsNullOrWhiteSpace(dto.NoteText))
                    entity.NoteText = dto.NoteText.Trim();

                if (dto.CustomerId.HasValue && dto.CustomerId.Value > 0)
                {
                    // Customer var mı kontrolü
                    var customer = _customerRepository.GetByIdAsync(dto.CustomerId.Value).Result;
                    if (customer == null)
                        return ResponseGeneric<CustomerNoteUpdateDto>.Error("Geçersiz CustomerId.");
                    entity.CustomerId = dto.CustomerId.Value;
                }

                if (dto.CreatedDate.HasValue)
                    entity.CreatedDate = dto.CreatedDate.Value; // sende 'createdDate' ise ona göre düzelt

                _CustomerNoteRepository.Update(entity);
                _CustomerNoteRepository.SaveChange();

                return ResponseGeneric<CustomerNoteUpdateDto>.Success(dto, "Not başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return ResponseGeneric<CustomerNoteUpdateDto>.Error($"Update hatası: {ex.Message}");
            }
        }

    }
}
