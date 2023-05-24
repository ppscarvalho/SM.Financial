using AutoMapper;
using MediatR;
using SM.Financial.Core.Application.Commands.BillToPay;
using SM.Financial.Core.Application.Models;
using SM.Financial.Core.Application.Queries.BillToPay;
using SM.MQ.Models;
using SM.MQ.Models.BillToPlay;
using SM.MQ.Operators;
using SM.Resource.Communication.Mediator;
using SM.Resource.Messagens.CommonMessage.Notifications;
using SM.Util.Extensions;

namespace SM.Financial.Core.Application.Consumers
{
    public class RPCConsumerBillToPay : Consumer<RequestIn>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;
        private readonly IMediator _mediatorQuery;
        private readonly DomainNotificationHandler _notifications;

        public RPCConsumerBillToPay(
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
                case "GetBillToPayById":
                    await GetBillToPayById(context);
                    break;

                case "GetAllBillToPay":
                    await GetAllBillToPay(context);
                    break;

                case "AddBillToPay":
                    await AddBillToPay(context);
                    break;

                case "UpdateBillToPay":
                    await UpdateBillToPay(context);
                    break;

                default:
                    await GetAllBillToPay(context);
                    break;
            }
        }


        private async Task GetBillToPayById(ConsumerContext<RequestIn> context)
        {
            var id = Guid.Parse(context.Message.Result);
            var query = new GetBillToPayByIdQuery(id);
            var result = _mapper.Map<ResponseBillToPayOut>(await _mediatorQuery.Send(query));
            await context.RespondAsync(result);
        }
        private async Task GetAllBillToPay(ConsumerContext<RequestIn> context)
        {
            var result = _mapper.Map<IEnumerable<ResponseBillToPayOut>>(await _mediatorQuery.Send(new GetAllBillToPayQuery()));
            await context.RespondAsync(result.ToArray());
        }

        private async Task AddBillToPay(ConsumerContext<RequestIn> context)
        {
            var billToPayModel = context.Message.Result.DeserializeObject<BillToPayModel>();

            var command = _mapper.Map<AddBillToPayCommand>(billToPayModel);
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

        private async Task UpdateBillToPay(ConsumerContext<RequestIn> context)
        {
            var categoriaModel = context.Message.Result.DeserializeObject<BillToPayModel>();

            var command = _mapper.Map<UpdateBillToPayCommand>(categoriaModel);
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
