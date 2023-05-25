using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SM.Financial.Apresentation.Api.Controllers.BaseController;
using SM.Financial.Core.Application.Commands.BillToPay;
using SM.Financial.Core.Application.Models;
using SM.Financial.Core.Application.Queries.BillToPay;
using SM.Resource.Communication.Mediator;
using SM.Resource.Messagens.CommonMessage.Notifications;
using SM.Resource.Util;

namespace SM.Financial.Apresentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillToPayController : ControllerConfiguration
    {
        private readonly ILogger<BillToPayController> _logger;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;
        private readonly IMediator _mediatorQuery;

        public BillToPayController(
            ILogger<BillToPayController> logger,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler,
            IMapper mapper,
            IMediator mediatorQuery) : base(notifications, mediatorHandler)
        {
            _logger = logger;
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
            _mediatorQuery = mediatorQuery;
        }

        [HttpPost]
        [Route("GetBillToPayById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BillToPayModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBillToPayById([FromBody] GetBillToPayByIdQuery query)
        {
            _logger.LogInformation("Obter todas as categorias");
            var result = await _mediatorQuery.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllBillToPay")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BillToPayModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBillToPay()
        {
            _logger.LogInformation("Obter todas as categorias");
            var result = await _mediatorQuery.Send(new GetAllBillToPayQuery());
            return Ok(result);
        }

        [HttpPost]
        [Route("AddBillToPay")]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResult>> AddBillToPay([FromBody] BillToPayModel BillToPayModel)
        {
            var cmd = _mapper.Map<AddBillToPayCommand>(BillToPayModel);
            var result = await _mediatorHandler.SendCommand(cmd);

            if (ValidOperation())
                return Ok(result);
            else
                return BadRequest(GetMessageError());
        }

        [HttpPost]
        [Route("UpdateBillToPay")]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResult>> UpdateBillToPay([FromBody] BillToPayModel BillToPayModel)
        {
            var cmd = _mapper.Map<UpdateBillToPayCommand>(BillToPayModel);
            var result = await _mediatorHandler.SendCommand(cmd);

            if (ValidOperation())
                return Ok(result);
            else
                return BadRequest(GetMessageError());
        }
    }
}
