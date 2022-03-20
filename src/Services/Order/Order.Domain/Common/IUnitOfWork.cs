using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Common
{
    public interface IUnitOfWork
    {
        IOrderRepository OrderRepository { get; }

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
