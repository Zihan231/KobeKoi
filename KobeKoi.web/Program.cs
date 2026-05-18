using KobeKoi.BLL;
using KobeKoi.BLL.Service.Admin;
using KobeKoi.BLL.Service.Auth;
using KobeKoi.BLL.Service.Events;
using KobeKoi.BLL.Service.Users;
using KobeKoi.DAL.EF;
using KobeKoi.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

//Session
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(30);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<KobeKoiContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("KobeKoiContext")));
//register automapper
builder.Services.AddAutoMapper(typeof(MapperConfig));

//register services
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AdminService>();
//Register Repos
builder.Services.AddScoped<EventRepo>();
builder.Services.AddScoped<UserRepo>();
builder.Services.AddScoped<VenueRepo>();
builder.Services.AddScoped<BookingRepo>();




// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
