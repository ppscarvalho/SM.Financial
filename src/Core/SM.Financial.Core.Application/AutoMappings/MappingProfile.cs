using AutoMapper;
using SM.Financial.Core.Application.Commands.BillToPay;
using SM.Financial.Core.Application.Models;
using SM.Financial.Core.Domain.Entities;
using SM.MQ.Models.BillToPlay;

namespace SM.Financial.Core.Application.AutoMappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add BillToPay Command
            CreateMap<BillToPayModel, AddBillToPayCommand>().ReverseMap();
            CreateMap<AddBillToPayCommand, BillToPay>().ReverseMap();

            // Update BillToPay Command
            CreateMap<BillToPayModel, UpdateBillToPayCommand>().ReverseMap();
            CreateMap<UpdateBillToPayCommand, BillToPay>().ReverseMap();

            CreateMap<BillToPay, BillToPayModel>().ReverseMap();
            CreateMap<BillToPayModel, ResponseBillToPayOut>().ReverseMap();
        }
    }
}
