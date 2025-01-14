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
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<MenuQuanTri> MenuQuanTris { get; set; }
        public DbSet<QuyenSuDung> QuyenSuDungs { get; set; }
        public DbSet<VaiTro> VaiTros { get; set; }
        public DbSet<PhanQuyenNguoiDung> PhanQuyenNguoiDungs { get; set; }
        public DbSet<VaiTroNguoiDung> VaiTroNguoiDungs { get; set; }
        public DbSet<VaiTroMenu> VaiTroMenus { get; set; }
        public DbSet<VaiTroQuyen> VaiTroQuyens { get; set; }
        public DbSet<MenuNguoiDung> MenuNguoiDungs { get; set; }
        public DbSet<NguoiDungSubscription> NguoiDungSubscriptions { get; set; }
        public DBContext() { }

        public DBContext(DbContextOptions<DBContext> options)
        : base(options)
        {
        }
    }
}
