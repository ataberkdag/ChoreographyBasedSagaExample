using AutoMapper;
using MassTransit;
using MediatR;
using Payment.Application.Payments.Commands.CreatePaymentProcess;
using Shared.Events;
using System.Threading.Tasks;

namespace Payment.API.Consumers
{
    public class StockReservedEventConsumer : IConsumer<StockReservedEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public StockReservedEventConsumer(IMediator mediator, IMapper mapper)
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }

        public async Task Consume(ConsumeContext<StockReservedEvent> context)
        {
            await this._mediator.Send(this._mapper.Map<CreatePaymentProcessCommand>(context.Message));
        }
    }
}
