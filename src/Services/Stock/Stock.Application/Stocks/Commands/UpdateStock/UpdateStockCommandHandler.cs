using MediatR;
using Microsoft.Extensions.Logging;
using Stock.Domain.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Stock.Application.Stocks.Commands.UpdateStock
{
    public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand>
    {
        private readonly IStockUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateStockCommandHandler> _logger;
        public UpdateStockCommandHandler(IStockUnitOfWork unitOfWork,
            ILogger<UpdateStockCommandHandler> logger)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
        }

        public async Task<Unit> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
        {
            if (request.OrderItems == null || request.OrderItems.Count == 0)
                return Unit.Value;

            foreach (var orderItem in request.OrderItems)
            {
                var product = await this._unitOfWork.StockRepository.GetByProductId(orderItem.ProductId);

                this._logger.LogInformation($"ProductId: {product.ProductId} | {orderItem.Quantity} item(s) Added");
                product.Quantity += orderItem.Quantity;
            }

            this._logger.LogInformation($"OrderId: {request.OrderId} | Order Canceled");

            await this._unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
