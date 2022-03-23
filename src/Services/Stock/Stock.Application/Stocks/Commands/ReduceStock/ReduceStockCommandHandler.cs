using AutoMapper;
using Core.Application.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.Events;
using Stock.Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Stock.Application.Stocks.Commands.ReduceStock
{
    public class ReduceStockCommandHandler : IRequestHandler<ReduceStockCommand>
    {
        private readonly IMassTransitHandler _massTransitHandler;
        private readonly IMapper _mapper;
        private readonly ILogger<ReduceStockCommandHandler> _logger;
        private readonly IStockUnitOfWork _unitOfWork;
        public ReduceStockCommandHandler(IMassTransitHandler massTransitHandler,
            IMapper mapper,
            ILogger<ReduceStockCommandHandler> logger,
            IStockUnitOfWork unitOfWork)
        {
            this._massTransitHandler = massTransitHandler;
            this._mapper = mapper;
            this._logger = logger;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ReduceStockCommand request, CancellationToken cancellationToken)
        {
            bool result = true;

            foreach (var orderItem in request.OrderItems)
            {
                if (!await this._unitOfWork.StockRepository.AnyWithQuantity(orderItem.ProductId, orderItem.Quantity))
                {
                    result = false;
                    break;
                }
            }

            if (!result)
            {
                this._logger.LogInformation($"OrderId: {request.OrderId} | Out of Stock");

                await this._massTransitHandler.Publish(new StockNotReservedEvent
                {
                    OrderId = request.OrderId,
                    Message = "Out of Stock"
                },
                typeof(StockNotReservedEvent));

                return Unit.Value;
            }

            foreach (var orderItem in request.OrderItems)
            {
                var stock = await this._unitOfWork.StockRepository.GetByProductId(productId: orderItem.ProductId);
                if (stock != null)
                    stock.Quantity -= orderItem.Quantity;
            }

            await this._unitOfWork.SaveChangesAsync();

            this._logger.LogInformation($"OrderId: {request.OrderId} | Stock Reserved");

            var @event = this._mapper.Map<StockReservedEvent>(request);

            await this._massTransitHandler.Send(RabbitMQConsts.StockReservedEventQueueName, @event, typeof(StockReservedEvent));

            return Unit.Value;
        }
    }
}
