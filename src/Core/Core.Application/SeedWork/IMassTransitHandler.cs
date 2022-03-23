using Shared.Base;
using System;
using System.Threading.Tasks;

namespace Core.Application.SeedWork
{
    public interface IMassTransitHandler
    {
        Task Publish(IDomainEvent @event, Type type);

        Task Send(string queueName, IDomainEvent @event, Type type);
    }
}
