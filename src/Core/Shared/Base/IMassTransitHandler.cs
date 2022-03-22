using System;
using System.Threading.Tasks;

namespace Shared.Base
{
    public interface IMassTransitHandler
    {
        Task Publish(IDomainEvent @event, Type type);

        Task Send(string queueName, IDomainEvent @event);
    }
}
