using AutoMapper;
using Core.Application.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Application.Payments.Commands.CreatePaymentProcess
{
    public class CreatePaymentProcessCommandHandler : IRequestHandler<CreatePaymentProcessCommand>
    {
        private readonly ILogger<CreatePaymentProcessCommandHandler> _logger;
        private readonly IMassTransitHandler _massTransitHandler;
        private readonly IMapper _mapper;

        public CreatePaymentProcessCommandHandler(ILogger<CreatePaymentProcessCommandHandler> logger, 
            IMassTransitHandler massTransitHandler,
            IMapper mapper)
        {
            this._logger = logger;
            this._massTransitHandler = massTransitHandler;
            this._mapper = mapper;
        }

        public Task<Unit> Handle(CreatePaymentProcessCommand request, CancellationToken cancellationToken)
        {
            var balance = 100;

            if (balance > request.Payment.TotalPrice)
            {
                this._logger.LogInformation($"OrderId: {request.OrderId} | {request.Payment.TotalPrice} USD was Withdrawn");

                this._massTransitHandler.Publish(this._mapper.Map<PaymentSucceededEvent>(request), typeof(PaymentSucceededEvent));
            }
            else
            {
                this._logger.LogInformation($"OrderId: {request.OrderId} | Insufficient Balance | {request.Payment.TotalPrice} USD");

                var @event = this._mapper.Map<PaymentFailedEvent>(request);
                @event.Message = "Insufficient Balance";

                this._massTransitHandler.Publish(@event, typeof(PaymentFailedEvent));
            }

            return Task.FromResult(Unit.Value);
        }
    }
}
