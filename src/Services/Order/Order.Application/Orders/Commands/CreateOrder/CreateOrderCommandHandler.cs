using Core.Application.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Domain.Common;
using Order.Domain.Entities;
using Shared.Events;
using Shared.Messages;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IOrderUnitOfWork _unitOfWork;
        private readonly IMassTransitHandler _massTransitHandler;
        private readonly ILogger<CreateOrderCommandHandler> _logger;

        public CreateOrderCommandHandler(IOrderUnitOfWork unitOfWork, 
            IMassTransitHandler massTransitHandler,
            ILogger<CreateOrderCommandHandler> logger)
        {
            this._unitOfWork = unitOfWork;
            this._massTransitHandler = massTransitHandler;
            this._logger = logger;
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var address = Address
                .CreateAddress(request?.Address?.Line, request?.Address?.Province, request?.Address.District);

            var order = Domain.Entities.Order
                .CreateOrder(request?.BuyerId, address);

            foreach (var orderItem in request?.OrderItems)
            {
                order.Items.Add(OrderItem.CreateOrderItem(orderItem.ProductId, order, orderItem.Quantity, orderItem.Price));
            }

            this._unitOfWork.OrderRepository.Add(order);

            await this._unitOfWork.SaveChangesAsync();

            this._logger.LogInformation($"OrderId: {order.Id} | Order Created");

            var orderCreatedEvent = new OrderCreatedEvent(order.Id, request.BuyerId, 
                new PaymentMessage(request.PaymentMethod.CardName, request.PaymentMethod.CardNumber, request.PaymentMethod.Expiration,
                    request.PaymentMethod.Cvv, request.OrderItems.Sum(x => x.Price * x.Quantity)));

            request.OrderItems?.ForEach(x => {
                orderCreatedEvent.AddOrderItem(new OrderItemMessage(x.ProductId, x.Quantity));
            });

            // Publish => Goes to exchange. Send => Directly Goes to Queue
            await this._massTransitHandler.Publish(orderCreatedEvent, typeof(OrderCreatedEvent));

            return Unit.Value;
        }
    }
}
