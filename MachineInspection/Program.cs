using MachineInspection.Application.Facade;
using MachineInspection.Application.IHelper;
using MachineInspection.Application.Service;
using MachineInspection.Domain.IRepositories;
using MachineInspection.Infrastructure.Data;
using MachineInspection.Infrastructure.Helper;
using MachineInspection.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);


// Ambil connection string dari appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<DatabaseContext>(provider => new DatabaseContext(connectionString));

// Daftarkan repository sebagai scoped
builder.Services.AddScoped<IMachineRepository, MachineRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IBusinessUnitRepository, BusinessUnitRepository>();
builder.Services.AddScoped<IInspectionItemRepository, InspectionItemRepository>();
builder.Services.AddScoped<IMachineInspectionRepository, MachineInspectionRepository>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICookieAuthHelper, CookieAuthHelper>();
builder.Services.AddScoped<ICurrentUserHelper, CurrentUserHelper>();

builder.Services.AddScoped<MachineService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<BusinessUnitService>();
builder.Services.AddScoped<InspectionItemService>();
builder.Services.AddScoped<MachineInspectionService>();

builder.Services.AddScoped<MachineFacade>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/auth/index";  // Arahkan ke halaman login jika belum login
        options.AccessDeniedPath = "/auth/denied"; // Arahkan ke halaman akses ditolak
    });

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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
