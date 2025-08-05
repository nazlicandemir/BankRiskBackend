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
    public  class RiskHistoryService : IRiskHistoryService
    {
        private readonly IGenericRepository<RiskHistory> _riskHistoryRepository;

        public RiskHistoryService(IGenericRepository<RiskHistory> riskHistoryRepository)
        {
            _riskHistoryRepository = riskHistoryRepository;
        }


        public IResponse<RiskHistory> Create(Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return ResponseGeneric<RiskHistory>.Error("Müşteri bilgileri boş bırakılamaz");
                }
                var riskHistoryCustomer = new RiskHistory
                {
                    CustomerIdi = customer.Id,
                    RiskLevel = customer.riskLevel,
                    EvaluatedDate = DateTime.Now
                };

                _riskHistoryRepository.Create(riskHistoryCustomer);
                return ResponseGeneric<RiskHistory>.Success(riskHistoryCustomer, "Başarı ile oluşturuldu");
            }
            catch
            {
                return ResponseGeneric<RiskHistory>.Error("Müşteri bilgileri boş bırakılamaz");
            }
        }

        public IResponse<RiskHistory> Delete(int id)
        {
            try
            {
                var RiskHistory = _riskHistoryRepository.GetByIdAsync(id).Result;
                if (RiskHistory == null)
                {
                    return ResponseGeneric<RiskHistory>.Error("Müşteri bulunamadı");
                }
                _riskHistoryRepository.Delete(RiskHistory);
                return ResponseGeneric<RiskHistory>.Success(RiskHistory, "Müşteri başarıyla silindi");
            }
            catch
            {
                return ResponseGeneric<RiskHistory>.Error("Müşteri bulunamadı");
            }
        }

        public IResponse<RiskHistory> Get(int id)
        {
            try
            {
                var RiskHistory = _riskHistoryRepository.GetByIdAsync(id).Result;
                if (RiskHistory == null)
                {
                    return ResponseGeneric<RiskHistory>.Success(null, "Müşteri bulunamadı");
                }

                return ResponseGeneric<RiskHistory>.Success(RiskHistory, "Müşteri başarıyla bulundu");
            }
            catch
            {
                return ResponseGeneric<RiskHistory>.Success(null, "Müşteri bulunamadı");
            }
        }

        public IResponse<RiskHistory> GetByName(string name)
        {
            try
            {
                var riskHistory = _riskHistoryRepository.GetAll().FirstOrDefault(x => x.Title == name);
                if (riskHistory == null)
                {
                    return ResponseGeneric<RiskHistory>.Error("Müşteri bulunamadı");
                }
                return ResponseGeneric<RiskHistory>.Success(riskHistory, "Müşteri başarıyla bulundu");
            }
            catch
            {
                return ResponseGeneric<RiskHistory>.Error("Müşteri bulunamadı");
            }
        }

        public IResponse<IEnumerable<RiskHistory>> ListAll()
        {
            try
            {
                var riskHistoryList = _riskHistoryRepository.GetAll().ToList();
                if (riskHistoryList == null)
                {
                    return ResponseGeneric<IEnumerable<RiskHistory>>.Error("Müşteri bulunamadı");
                }
                return ResponseGeneric<IEnumerable<RiskHistory>>.Success(riskHistoryList, " Müşteri başarıyla bulundu");
            }
            catch
            {
                return ResponseGeneric<IEnumerable<RiskHistory>>.Error("Müşteri bulunamadı");
            }
        }


        public IResponse<RiskHistory> Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
