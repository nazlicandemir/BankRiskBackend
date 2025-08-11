using BankRiskTracking.Entities.DTOs;
using BankRiskTracking.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;


namespace BankRiskTracking.Business.Mapping
{
    public class MapProfile : Profile
    {   
        public MapProfile()
        {
            CreateMap<Customer, CustomerCreateDto>().ReverseMap();
            CreateMap<Customer, CustomerQueryDto>().ReverseMap();

            CreateMap<RiskHistory, RiskHistroyCreateDto>().ReverseMap();
            CreateMap<RiskHistory, RiskHistoryQueryDto>().ReverseMap();

            CreateMap<Transaction, TransactionCreateDto>().ReverseMap();
            CreateMap<Transaction, TransactionQueryDto>().ReverseMap();
        }

    }
}