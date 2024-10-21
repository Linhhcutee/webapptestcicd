using Microsoft.EntityFrameworkCore;
using QLCuaHangBanSach.Models;

var builder = WebApplication.CreateBuilder(args);

// Thay đổi từ UseSqlServer thành UseMySql
builder.Services.AddDbContext<DBCuaHangBanSachContext>(options =>
	options.UseMySql(builder.Configuration.GetConnectionString("DBCuaHangBanSachContext"),
	new MySqlServerVersion(new Version(6, 0, 1)))); // Đặt phiên bản MySQL tương ứng

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
