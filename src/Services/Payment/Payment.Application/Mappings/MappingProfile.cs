using AutoMapper;
using Payment.Application.Payments.Commands.CreatePaymentProcess;
using Shared.Events;
using Shared.Messages;

namespace Payment.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreatePaymentProcessCommand, StockReservedEvent>()
                .ReverseMap();

            CreateMap<PaymentDto, PaymentMessage>()
                .ReverseMap();

            CreateMap<CreatePaymentProcessCommand, PaymentSucceededEvent>()
                .ReverseMap();

            CreateMap<CreatePaymentProcessCommand, PaymentFailedEvent>()
                .ReverseMap();

            CreateMap<OrderItemDto, OrderItemMessage>()
                .ReverseMap();
        }
    }
}
