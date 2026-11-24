using HotelManagement.BL.Interfaces;
using HotelManagement.BL.Service;
using HotelManagement.BL.Services;
using HotelManagement.DAL.Context;
using HotelManagement.DAL.Interfaces;
using HotelManagement.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
 
var builder = WebApplication.CreateBuilder(args);

                     
builder.Services.AddControllersWithViews();

                // DbContext
builder.Services.AddDbContext<HotelDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

                // DAl
builder.Services.AddScoped<IEmpleadoDAL, EmpleadoRP>();
builder.Services.AddScoped<IPisoDAL, PisoRP>();
builder.Services.AddScoped<IBuildDAL, BuildRP>();
builder.Services.AddScoped<IHabitacionDAL, HabitacionRP>();
builder.Services.AddScoped<IReservaDAL, ReservaRP>();
builder.Services.AddScoped<IUsuarioDAL, UsuarioRP>();
builder.Services.AddScoped<IClienteDAL, ClienteRP>();

                // BL
builder.Services.AddScoped<IEmpleadoBL, EmpleadoBL>();
builder.Services.AddScoped<IPisoBL, PisoBL>();
builder.Services.AddScoped<IBuildBL, BuildBL>();
builder.Services.AddScoped<IHabitacionBL, HabitacionBL>();
builder.Services.AddScoped<IReservaBL, ReservaBL>();
builder.Services.AddScoped<IUsuarioBL, UsuarioBL>();
builder.Services.AddScoped<IClienteBL, ClienteBL>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
