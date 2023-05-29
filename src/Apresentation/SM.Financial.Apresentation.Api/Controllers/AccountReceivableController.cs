using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SM.Financial.Apresentation.Api.Controllers.BaseController;
using SM.Financial.Core.Application.Commands.AccountReceivable;
using SM.Financial.Core.Application.Models;
using SM.Financial.Core.Application.Queries.AccountReceivable;
using SM.Resource.Communication.Mediator;
using SM.Resource.Messagens.CommonMessage.Notifications;
using SM.Resource.Util;

namespace SM.Financial.Apresentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountReceivableController : ControllerConfiguration
    {
        private readonly ILogger<AccountReceivableController> _logger;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;
        private readonly IMediator _mediatorQuery;

        public AccountReceivableController(
            ILogger<AccountReceivableController> logger,
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
        [Route("GetAccountReceivableById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AccountReceivableModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccountReceivableById([FromBody] GetAccountReceivableByIdQuery query)
        {
            _logger.LogInformation("Obter todas as categorias");
            var result = await _mediatorQuery.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllAccountReceivable")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AccountReceivableModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAccountReceivable()
        {
            _logger.LogInformation("Obter todas as categorias");
            var result = await _mediatorQuery.Send(new GetAllAccountReceivableQuery());
            return Ok(result);
        }

        [HttpPost]
        [Route("AddAccountReceivable")]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResult>> AddAccountReceivable([FromBody] AccountReceivableModel AccountReceivableModel)
        {
            var cmd = _mapper.Map<AddAccountReceivableCommand>(AccountReceivableModel);
            var result = await _mediatorHandler.SendCommand(cmd);

            if (ValidOperation())
                return Ok(result);
            else
                return BadRequest(GetMessageError());
        }

        [HttpPost]
        [Route("UpdateAccountReceivable")]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResult>> UpdateAccountReceivable([FromBody] AccountReceivableModel AccountReceivableModel)
        {
            var cmd = _mapper.Map<UpdateAccountReceivableCommand>(AccountReceivableModel);
            var result = await _mediatorHandler.SendCommand(cmd);

            if (ValidOperation())
                return Ok(result);
            else
                return BadRequest(GetMessageError());
        }
    }
}
