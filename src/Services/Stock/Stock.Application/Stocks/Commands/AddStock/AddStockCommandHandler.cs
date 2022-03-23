using MediatR;
using Microsoft.Extensions.Logging;
using Stock.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Stock.Application.Stocks.Commands.AddStock
{
    public class AddStockCommandHandler : IRequestHandler<AddStockCommand>
    {
        private readonly IStockUnitOfWork _unitOfWork;
        private readonly ILogger<AddStockCommandHandler> _logger;

        public AddStockCommandHandler(IStockUnitOfWork unitOfWork,
            ILogger<AddStockCommandHandler> logger)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
        }

        public async Task<Unit> Handle(AddStockCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Stock stock = await this._unitOfWork.StockRepository.GetByProductId(request.ProductId);

            if (stock == null)
            {
                stock = new Domain.Entities.Stock
                {
                    Id = request.Id,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                };

                this._unitOfWork.StockRepository.Add(stock);

                this._logger.LogInformation($"Stock created. Id: {stock.Id} Quantity: {stock.Quantity}");
            }
            else
            {
                stock.Quantity += request.Quantity;

                this._logger.LogInformation($"Stock added. Id: {stock.Id} Quantity: {stock.Quantity}");
            }

            await this._unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
