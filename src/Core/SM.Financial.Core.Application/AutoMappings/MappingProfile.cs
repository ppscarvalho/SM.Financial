using AutoMapper;
using SM.Financial.Core.Application.Commands.BillToPay;
using SM.Financial.Core.Application.Models;
using SM.Financial.Core.Domain.Entities;

namespace SM.Financial.Core.Application.AutoMappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Add BillToPay Command
            CreateMap<BillToPayModel, AddBillToPayCommand>().ReverseMap();
            CreateMap<AddBillToPayCommand, BillToPayModel>().ReverseMap();

            CreateMap<BillToPay, BillToPayModel>().ReverseMap();
        }
    }
}
