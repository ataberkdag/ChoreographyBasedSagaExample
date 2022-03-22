using AutoMapper;
using Shared.Events;
using Shared.Messages;
using Stock.Application.Stocks.Commands.ReduceStock;
using Stock.Application.Stocks.Queries.Models;

namespace Stock.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ReduceStockCommand, OrderCreatedEvent>()
                .ReverseMap();
            CreateMap<PaymentDto, PaymentMessage>()
                .ReverseMap();
            CreateMap<OrderItemDto, OrderItemMessage>()
                .ReverseMap();
            CreateMap<ReduceStockCommand, StockReservedEvent>()
                .ReverseMap();

            CreateMap<Domain.Entities.Stock, StockDto>()
                .ReverseMap();
        }
    }
}
