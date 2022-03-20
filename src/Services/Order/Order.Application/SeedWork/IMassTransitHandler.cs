using Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.SeedWork
{
    public interface IMassTransitHandler
    {
        Task Publish(IDomainEvent @event);
    }
}
