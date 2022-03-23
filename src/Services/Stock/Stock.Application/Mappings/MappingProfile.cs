using AutoMapper;
using Shared.Events;
using Shared.Messages;
using Stock.Application.Stocks.Commands.ReduceStock;
using Stock.Application.Stocks.Commands.UpdateStock;
using Stock.Application.Stocks.Queries.Models;

namespace Stock.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ReduceStockCommand, OrderCreatedEvent>()
                .ReverseMap();
            CreateMap<UpdateStockCommand, PaymentFailedEvent>()
                .ReverseMap();
            CreateMap<ReduceStockCommand, StockReservedEvent>()
                .ReverseMap();

            CreateMap<PaymentDto, PaymentMessage>()
                .ReverseMap();
            CreateMap<OrderItemDto, OrderItemMessage>()
                .ReverseMap();
            CreateMap<Domain.Entities.Stock, StockDto>()
                .ReverseMap();
        }
    }
}
