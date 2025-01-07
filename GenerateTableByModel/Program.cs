using Microsoft.EntityFrameworkCore;
using Infrastructure.DependencyInjection;

var optionsBuilder = new DbContextOptionsBuilder<DBContext>();
optionsBuilder.UseSqlServer("Server=.;Database=QuanLyXeDB;Trusted_Connection=True;");

// Khởi tạo DbContext để sử dụng cho việc generate
using var context = new DBContext(optionsBuilder.Options);
