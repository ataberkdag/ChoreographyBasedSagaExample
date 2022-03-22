using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.Base;
using Shared.Events;
using Stock.Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Stock.Application.Stocks.Commands.ReduceStock
{
    public class ReduceStockCommandHandler : IRequestHandler<ReduceStockCommand>
    {
        private readonly IMassTransitHandler _massTransitHandler;
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ReduceStockCommandHandler> _logger;
        private readonly IStockUnitOfWork _unitOfWork;
        public ReduceStockCommandHandler(IMassTransitHandler massTransitHandler,
            IStockRepository stockRepository,
            IMapper mapper,
            ILogger<ReduceStockCommandHandler> logger,
            IStockUnitOfWork unitOfWork)
        {
            this._massTransitHandler = massTransitHandler;
            this._stockRepository = stockRepository;
            this._mapper = mapper;
            this._logger = logger;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ReduceStockCommand request, CancellationToken cancellationToken)
        {
            bool result = true;

            foreach (var orderItem in request.OrderItems)
            {
                if (!await this._stockRepository.AnyWithQuantity(orderItem.ProductId, orderItem.Quantity))
                {
                    result = false;
                    break;
                }
            }

            if (!result)
            {
                await this._massTransitHandler.Publish(new StockNotReservedEvent
                {
                    OrderId = request.OrderId,
                    Message = "Not Enough Stock!"
                },
                typeof(StockNotReservedEvent));

                return Unit.Value;
            }

            foreach (var orderItem in request.OrderItems)
            {
                var stock = await this._stockRepository.GetByProductId(productId: orderItem.ProductId);
                if (stock != null)
                    stock.Quantity -= orderItem.Quantity;
            }

            await this._unitOfWork.SaveChangesAsync();

            this._logger.LogInformation($"Stock reserved for {request.BuyerId}");

            var @event = this._mapper.Map<StockReservedEvent>(request);

            await this._massTransitHandler.Send(RabbitMQConsts.StockReservedEventQueueName, @event);

            return Unit.Value;
        }
    }
}
