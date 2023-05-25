using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SM.Financial.Core.Application.Models;
using SM.Financial.Core.Application.Queries.BillToPay;
using SM.MQ.Models;
using SM.MQ.Models.BillToPlay;
using SM.MQ.Operators;

namespace SM.Financial.Apresentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RPCBillToPayController : ControllerBase
    {
        private readonly IPublish _publish;

        public RPCBillToPayController(IPublish publish)
        {
            _publish = publish;
        }

        [HttpPost]
        [Route("GetBillToPayById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseBillToPayOut), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBillToPayById([FromBody] GetBillToPayByIdQuery query)
        {
            var mapIn = new RequestIn
            {
                Host = "localhost",
                Result = query.Id.ToString(),
                Queue = "GetBillToPayById"
            };

            var result = await _publish.DoRPC<RequestIn, ResponseBillToPayOut>(mapIn);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllBillToPay")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseBillToPayOut), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBillToPay()
        {
            var mapIn = new RequestIn
            {
                Host = "localhost",
                Queue = "GetAllBillToPay"
            };

            var result = await _publish.DoRPC<RequestIn, ResponseBillToPayOut[]>(mapIn);
            return Ok(result);
        }

        [HttpPost]
        [Route("AddBillToPay")]
        [ProducesResponseType(typeof(ResponseOut), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseOut), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseOut), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseOut>> AddBillToPay([FromBody] BillToPayModel BillToPayModel)
        {
            var mapIn = new RequestIn
            {
                Host = "localhost",
                Result = JsonConvert.SerializeObject(BillToPayModel),
                Queue = "AddBillToPay"
            };

            var response = await _publish.DoRPC<RequestIn, ResponseOut>(mapIn);
            return response;
        }

        [HttpPost]
        [Route("UpdateBillToPay")]
        [ProducesResponseType(typeof(ResponseOut), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseOut), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseOut), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseOut>> UpdateBillToPay([FromBody] BillToPayModel BillToPayModel)
        {
            var mapIn = new RequestIn
            {
                Host = "localhost",
                Result = JsonConvert.SerializeObject(BillToPayModel),
                Queue = "UpdateBillToPay"
            };

            var response = await _publish.DoRPC<RequestIn, ResponseOut>(mapIn);
            return response;
        }
    }
}
