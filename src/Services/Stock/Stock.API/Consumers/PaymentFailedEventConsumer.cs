using AutoMapper;
using MassTransit;
using MediatR;
using Shared.Events;
using Stock.Application.Stocks.Commands.UpdateStock;
using System.Threading.Tasks;

namespace Stock.API.Consumers
{
    public class PaymentFailedEventConsumer : IConsumer<PaymentFailedEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PaymentFailedEventConsumer(IMediator mediator,
            IMapper mapper)
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }

        public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            await this._mediator.Send(this._mapper.Map<UpdateStockCommand>(context.Message));
        }
    }
}
