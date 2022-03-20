using MediatR;
using Order.Application.SeedWork;
using Order.Domain.Common;
using Order.Domain.Entities;
using Shared.Events;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMassTransitHandler _massTransitHandler;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IMassTransitHandler massTransitHandler)
        {
            this._unitOfWork = unitOfWork;
            this._massTransitHandler = massTransitHandler;
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

            var orderCreatedEvent = new OrderCreatedEvent(order.Id, request.BuyerId, 
                new PaymentMessage(request.PaymentMethod.CardName, request.PaymentMethod.CardNumber, request.PaymentMethod.Expiration,
                    request.PaymentMethod.Cvv, request.OrderItems.Sum(x => x.Price * x.Quantity)));

            request.OrderItems?.ForEach(x => {
                orderCreatedEvent.AddOrderItem(new OrderItemMessage(x.ProductId, x.Quantity));
            });

            // Publish => Goes to exchange. Send => Directly Goes to Queue
            await this._massTransitHandler.Publish(orderCreatedEvent);

            return Unit.Value;
        }
    }
}
