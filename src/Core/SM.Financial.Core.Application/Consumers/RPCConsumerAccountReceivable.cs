using AutoMapper;
using MediatR;
using SM.Financial.Core.Application.Commands.AccountReceivable;
using SM.Financial.Core.Application.Models;
using SM.Financial.Core.Application.Queries.AccountReceivable;
using SM.MQ.Models;
using SM.MQ.Models.AccountReceivable;
using SM.MQ.Operators;
using SM.Resource.Communication.Mediator;
using SM.Resource.Messagens.CommonMessage.Notifications;
using SM.Util.Extensions;

namespace SM.Financial.Core.Application.Consumers
{
    public class RPCConsumerAccountReceivable : Consumer<RequestIn>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;
        private readonly IMediator _mediatorQuery;
        private readonly DomainNotificationHandler _notifications;

        public RPCConsumerAccountReceivable(
            IMapper mapper,
            IMediatorHandler mediatorHandler,
            INotificationHandler<DomainNotification> notifications,
            IMediator mediatorQuery)
        {
            _mapper = mapper;
            _mediatorHandler = mediatorHandler;
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorQuery = mediatorQuery;
        }

        public override async Task ConsumeContex(ConsumerContext<RequestIn> context)
        {
            switch (context.Message.Queue)
            {
                case "GetAccountReceivableById":
                    await GetAccountReceivableById(context);
                    break;

                case "GetAllAccountReceivable":
                    await GetAllAccountReceivable(context);
                    break;

                case "AddAccountReceivable":
                    await AddAccountReceivable(context);
                    break;

                case "UpdateAccountReceivable":
                    await UpdateAccountReceivable(context);
                    break;

                default:
                    await GetAllAccountReceivable(context);
                    break;
            }
        }

        private async Task GetAccountReceivableById(ConsumerContext<RequestIn> context)
        {
            var id = Guid.Parse(context.Message.Result);
            var query = new GetAccountReceivableByIdQuery(id);
            var result = _mapper.Map<ResponseAccountReceivableOut>(await _mediatorQuery.Send(query));
            await context.RespondAsync(result);
        }
        private async Task GetAllAccountReceivable(ConsumerContext<RequestIn> context)
        {
            var result = _mapper.Map<IEnumerable<ResponseAccountReceivableOut>>(await _mediatorQuery.Send(new GetAllAccountReceivableQuery()));
            await context.RespondAsync(result.ToArray());
        }

        private async Task AddAccountReceivable(ConsumerContext<RequestIn> context)
        {
            var AccountReceivableModel = context.Message.Result.DeserializeObject<AccountReceivableModel>();

            var command = _mapper.Map<AddAccountReceivableCommand>(AccountReceivableModel);
            var result = await _mediatorHandler.SendCommand(command);

            if (result.Success)
            {
                await context.RespondAsync(new ResponseOut { Success = result.Success });
            }
            else if (!_notifications.ExistNotification())
            {
                await context.RespondAsync(new ResponseOut { Success = result.Success });
            }
        }

        private async Task UpdateAccountReceivable(ConsumerContext<RequestIn> context)
        {
            var categoriaModel = context.Message.Result.DeserializeObject<AccountReceivableModel>();

            var command = _mapper.Map<UpdateAccountReceivableCommand>(categoriaModel);
            var result = await _mediatorHandler.SendCommand(command);

            if (result.Success)
            {
                await context.RespondAsync(new ResponseOut { Success = result.Success });
            }
            else if (!_notifications.ExistNotification())
            {
                await context.RespondAsync(new ResponseOut { Success = result.Success });
            }
        }
    }
}
