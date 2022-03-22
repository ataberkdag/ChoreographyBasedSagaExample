using AutoMapper;
using MassTransit;
using MediatR;
using Shared.Events;
using Stock.Application.Stocks.Commands.ReduceStock;
using System.Threading.Tasks;

namespace Stock.API.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public OrderCreatedEventConsumer(IMediator mediator,
            IMapper mapper)
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            await _mediator.Send(this._mapper.Map<ReduceStockCommand>(context.Message));
        }
    }
}
