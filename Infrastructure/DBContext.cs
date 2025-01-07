using Domain.Models;
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
        public DbSet<User> User { get; set; }
        public DBContext() { }

        public DBContext(DbContextOptions<DBContext> options)
        : base(options)
        {
        }
    }
}
