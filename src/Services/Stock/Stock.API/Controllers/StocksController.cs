using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stock.Application.Stocks.Commands.AddStock;
using Stock.Application.Stocks.Queries.GetStockByProductId;
using System.Threading.Tasks;

namespace Stock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StocksController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddStock(AddStockCommand command)
        {
            return Ok(await this._mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetStockByProductId([FromQuery] GetStockByProductIdQuery query)
        {
            return Ok(await this._mediator.Send(query));
        }
    }
}
