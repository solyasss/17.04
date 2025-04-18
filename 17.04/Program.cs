using _17._04.Models;
using _17._04.Repository;
using _17._04.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.Name = "GuestBookSession";
});

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<user_context>(o => o.UseSqlServer(connection));

builder.Services.AddRazorPages();
builder.Services.AddScoped<igb_repository, gb_repository>();
builder.Services.AddScoped<ihash_service, hash_service>();

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();
app.MapRazorPages();
app.Run();