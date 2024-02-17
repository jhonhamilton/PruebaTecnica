using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.DataAcces;
using PruebaTecnica.Helpers;
using PruebaTecnica.Models.Model;
using PruebaTecnica.Servicio;
using PruebaTecnica.Servicio.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

const string CONNECTION_NAME = "PruebaTecnica";
var connectionString = builder.Configuration.GetConnectionString(CONNECTION_NAME);
builder.Services.AddDbContext<PruebaDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddJwtTokenServices(builder.Configuration);
builder.Services.AddScoped<IUsuarioServices, UsuarioServices>();
builder.Services.AddScoped<IAccountLogin, Account>();


builder.Services.ConfigureApplicationCookie(options =>
{
    //options.LoginPath = new PathString("/Login/Index");
    options.LoginPath = "/Login/Index";
});

builder.Services.AddIdentity<UserLogin, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequiredLength = 6;
    //options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<PruebaDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddMvcCore();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "admin_route",
    areaName: "Admin",
    pattern: "{area=Admin}/{controller=Usuario}/{action=Index}/{id?}");

app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();

app.Run();
