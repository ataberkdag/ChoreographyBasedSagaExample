using MassTransit;
using MediatR;
using Order.Application.Orders.Commands.UpdateOrderStatus;
using Shared.Events;
using System.Threading.Tasks;

namespace Order.API.Consumers
{
    public class PaymentSucceededEventConsumer : IConsumer<PaymentSucceededEvent>
    {
        private readonly IMediator _mediator;

        public PaymentSucceededEventConsumer(IMediator mediator)
        {
            this._mediator = mediator;
        }

        public async Task Consume(ConsumeContext<PaymentSucceededEvent> context)
        {
            await this._mediator.Send(new UpdateOrderStatusCommand
            {
                OrderId = context.Message.OrderId,
                Status = 1
            });
        }
    }
}
