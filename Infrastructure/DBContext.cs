using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DependencyInjection
{
    public class DBContext : DbContext
    {
        public DbSet<Product> Product { get; set; }

        public DBContext(DbContextOptions<DBContext> options)
        : base(options)
        {
        }
    }
}
