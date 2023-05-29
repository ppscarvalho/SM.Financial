using AutoMapper;
using MediatR;
using SM.Financial.Core.Application.Commands.AccountReceivable;
using SM.Financial.Core.Application.Interfaces.Repositories.Domain;
using SM.Financial.Core.Application.Models;
using SM.Financial.Core.Domain.Entities;
using SM.Resource.Communication.Mediator;
using SM.Resource.Messagens;
using SM.Resource.Messagens.CommonMessage.Notifications;
using SM.Resource.Util;

namespace SM.Financial.Core.Application.Handlers
{
    public class AccountReceivableCommandHandler :
        IRequestHandler<AddAccountReceivableCommand, DefaultResult>,
        IRequestHandler<UpdateAccountReceivableCommand, DefaultResult>
    {
        private readonly IAccountReceivableRepository _AccountReceivableRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;

        public AccountReceivableCommandHandler(IAccountReceivableRepository AccountReceivableRepository, IMediatorHandler mediatorHandler, IMapper mapper)
        {
            _AccountReceivableRepository = AccountReceivableRepository;
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
        }

        public async Task<DefaultResult> Handle(AddAccountReceivableCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return new DefaultResult { Result = "Error", Success = false };

            var biilToPay = _mapper.Map<AccountReceivable>(request);
            var entity = _mapper.Map<AccountReceivableModel>(await _AccountReceivableRepository.AddAccountReceivable(biilToPay));

            var result = await _AccountReceivableRepository.UnitOfWork.Commit();

            return new DefaultResult { Result = entity, Success = result, Message = result ? "OK" : "Error" };
        }

        public async Task<DefaultResult> Handle(UpdateAccountReceivableCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return new DefaultResult { Result = "Error", Success = false };

            var biilToPay = _mapper.Map<AccountReceivable>(request);
            var entity = _mapper.Map<AccountReceivableModel>(await _AccountReceivableRepository.UpdateAccountReceivable(biilToPay));

            var result = await _AccountReceivableRepository.UnitOfWork.Commit();

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
