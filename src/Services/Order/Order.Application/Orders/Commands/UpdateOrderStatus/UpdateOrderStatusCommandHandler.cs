using MediatR;
using Microsoft.Extensions.Logging;
using Order.Domain.Common;
using Order.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand>
    {
        private readonly IOrderUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateOrderStatusCommandHandler> _logger;

        public UpdateOrderStatusCommandHandler(IOrderUnitOfWork unitOfWork,
            ILogger<UpdateOrderStatusCommandHandler> logger)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
        }

        public async Task<Unit> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = this._unitOfWork.OrderRepository.GetById(request.OrderId);

            if (order == null)
                throw new Exception("Order is null");

            order.Status = (OrderStatus)request.Status;

            if (!string.IsNullOrWhiteSpace(request.FailMessage))
                order.Message = request.FailMessage;

            await this._unitOfWork.SaveChangesAsync();

            this._logger.LogInformation($"OrderId: {request.OrderId} | Status Changed | Current Status: {order.Status}");

            return Unit.Value;
        }
    }
}
