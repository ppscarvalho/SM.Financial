using AutoMapper;
using MediatR;
using SM.Financial.Core.Application.Commands.BillToPay;
using SM.Financial.Core.Application.Interfaces.Repositories.Domain;
using SM.Financial.Core.Application.Models;
using SM.Financial.Core.Domain.Entities;
using SM.Resource.Communication.Mediator;
using SM.Resource.Messagens;
using SM.Resource.Messagens.CommonMessage.Notifications;
using SM.Resource.Util;

namespace SM.Financial.Core.Application.Handlers
{
    public class BillToPayCommandHandler :
        IRequestHandler<AddBillToPayCommand, DefaultResult>,
        IRequestHandler<UpdateBillToPayCommand, DefaultResult>
    {
        private readonly IBillToPayRepository _billToPayRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;

        public BillToPayCommandHandler(IBillToPayRepository billToPayRepository, IMediatorHandler mediatorHandler, IMapper mapper)
        {
            _billToPayRepository = billToPayRepository;
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
        }

        public async Task<DefaultResult> Handle(AddBillToPayCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return new DefaultResult { Result = "Error", Success = false };

            var biilToPay = _mapper.Map<BillToPay>(request);
            var entity = _mapper.Map<BillToPayModel>(await _billToPayRepository.AddBillToPay(biilToPay));

            var result = await _billToPayRepository.UnitOfWork.Commit();

            return new DefaultResult { Result = entity, Success = result, Message = result ? "OK" : "Error" };
        }

        public async Task<DefaultResult> Handle(UpdateBillToPayCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return new DefaultResult { Result = "Error", Success = false };

            var biilToPay = _mapper.Map<BillToPay>(request);
            var entity = _mapper.Map<BillToPayModel>(await _billToPayRepository.UpdateBillToPay(biilToPay));

            var result = await _billToPayRepository.UnitOfWork.Commit();

            return new DefaultResult { Result = entity, Success = result, Message = result ? "OK" : "Error" };
        }

        private bool ValidateCommand(CommandHandler message)
        {
            if (message.IsValid()) return true;

            foreach (var error in message.ValidationResult.Errors)
                _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, error.ErrorMessage));

            return false;
        }
    }
}
