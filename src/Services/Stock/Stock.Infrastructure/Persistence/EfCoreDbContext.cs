using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Infrastructure.Persistence
{
    public class EfCoreDbContext : DbContext
    {
        public EfCoreDbContext(DbContextOptions opt) : base(opt)
        {

        }

        public DbSet<Domain.Entities.Stock> Stocks { get; set; }
    }
}
