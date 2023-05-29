using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SM.Financial.Core.Application.Models;
using SM.Financial.Core.Application.Queries.AccountReceivable;
using SM.MQ.Models;
using SM.MQ.Models.AccountReceivable;
using SM.MQ.Operators;

namespace SM.Financial.Apresentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RPCAccountReceivableController : ControllerBase
    {
        private readonly IPublish _publish;

        public RPCAccountReceivableController(IPublish publish)
        {
            _publish = publish;
        }

        [HttpPost]
        [Route("GetAccountReceivableById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseAccountReceivableOut), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccountReceivableById([FromBody] GetAccountReceivableByIdQuery query)
        {
            var mapIn = new RequestIn
            {
                Host = "localhost",
                Result = query.Id.ToString(),
                Queue = "GetAccountReceivableById"
            };

            var result = await _publish.DoRPC<RequestIn, ResponseAccountReceivableOut>(mapIn);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllAccountReceivable")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseAccountReceivableOut), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAccountReceivable()
        {
            var mapIn = new RequestIn
            {
                Host = "localhost",
                Queue = "GetAllAccountReceivable"
            };

            var result = await _publish.DoRPC<RequestIn, ResponseAccountReceivableOut[]>(mapIn);
            return Ok(result);
        }

        [HttpPost]
        [Route("AddAccountReceivable")]
        [ProducesResponseType(typeof(ResponseOut), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseOut), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseOut), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseOut>> AddAccountReceivable([FromBody] AccountReceivableModel AccountReceivableModel)
        {
            var mapIn = new RequestIn
            {
                Host = "localhost",
                Result = JsonConvert.SerializeObject(AccountReceivableModel),
                Queue = "AddAccountReceivable"
            };

            var response = await _publish.DoRPC<RequestIn, ResponseOut>(mapIn);
            return response;
        }

        [HttpPost]
        [Route("UpdateAccountReceivable")]
        [ProducesResponseType(typeof(ResponseOut), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseOut), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseOut), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseOut>> UpdateAccountReceivable([FromBody] AccountReceivableModel AccountReceivableModel)
        {
            var mapIn = new RequestIn
            {
                Host = "localhost",
                Result = JsonConvert.SerializeObject(AccountReceivableModel),
                Queue = "UpdateAccountReceivable"
            };

            var response = await _publish.DoRPC<RequestIn, ResponseOut>(mapIn);
            return response;
        }
    }
}
