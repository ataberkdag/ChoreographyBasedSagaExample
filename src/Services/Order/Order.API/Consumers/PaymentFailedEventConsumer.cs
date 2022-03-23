using MassTransit;
using MediatR;
using Order.Application.Orders.Commands.UpdateOrderStatus;
using Shared.Events;
using System.Threading.Tasks;

namespace Order.API.Consumers
{
    public class PaymentFailedEventConsumer : IConsumer<PaymentFailedEvent>
    {
        private readonly IMediator _mediator;

        public PaymentFailedEventConsumer(IMediator mediator)
        {
            this._mediator = mediator;
        }

        public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            await this._mediator.Send(new UpdateOrderStatusCommand
            {
                OrderId = context.Message.OrderId,
                Status = 2,
                FailMessage = context.Message.Message
            });
        }
    }
}
