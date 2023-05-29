using AutoMapper;
using SM.Financial.Core.Application.Commands.AccountReceivable;
using SM.Financial.Core.Application.Commands.BillToPay;
using SM.Financial.Core.Application.Models;
using SM.Financial.Core.Domain.Entities;
using SM.MQ.Models.AccountReceivable;
using SM.MQ.Models.BillToPlay;

namespace SM.Financial.Core.Application.AutoMappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add BillToPay Command
            CreateMap<BillToPayModel, AddBillToPayCommand>().ReverseMap();
            CreateMap<AddBillToPayCommand, BillToPay>().ConstructUsing(b => new BillToPay(
            b.SupplierId,
            b.Description,
            b.DueDate,
            b.Value)).ReverseMap();

            // Update BillToPay Command
            CreateMap<BillToPayModel, UpdateBillToPayCommand>().ReverseMap();
            CreateMap<UpdateBillToPayCommand, BillToPay>().ConstructUsing(b => new BillToPay(
                b.Id,
                b.SupplierId,
                b.Description,
                b.DueDate,
                b.Value,
                b.Status)).ReverseMap();

            CreateMap<BillToPay, BillToPayModel>().ReverseMap();
            CreateMap<BillToPayModel, ResponseBillToPayOut>().ReverseMap();

            // Add AccountReceivable Command
            CreateMap<AccountReceivableModel, AddAccountReceivableCommand>().ReverseMap();
            CreateMap<AddAccountReceivableCommand, AccountReceivable>().ReverseMap();

            // Update AccountReceivable Command
            CreateMap<AccountReceivableModel, UpdateAccountReceivableCommand>().ReverseMap();
            CreateMap<UpdateAccountReceivableCommand, AccountReceivable>().ReverseMap();

            CreateMap<AccountReceivable, AccountReceivableModel>().ReverseMap();
            CreateMap<AccountReceivableModel, ResponseAccountReceivableOut>().ReverseMap();
        }
    }
}
