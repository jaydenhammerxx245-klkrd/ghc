using HotelManagement.BL.Interfaces;
using HotelManagement.BL.Services;
using HotelManagement.DAL.Context;
using HotelManagement.BL.Repositorios;
using HotelManagement.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
 
var builder = WebApplication.CreateBuilder(args);

                     
builder.Services.AddControllersWithViews();

                // DbContext
// "DatabaseProvider" = "Sqlite" usa un archivo local (no requiere instalar nada);
// cualquier otro valor usa SQL Server con "DefaultConnection".
var dbProvider = builder.Configuration["DatabaseProvider"] ?? "SqlServer";
builder.Services.AddDbContext<HotelDbContext>(options =>
{
    if (dbProvider.Equals("Sqlite", StringComparison.OrdinalIgnoreCase))
        options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"));
    else
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// DAL: única parte de la app que conoce qué implementación concreta (EF) usa cada contrato.
builder.Services.AddScoped<IEmpleadoRepositorio, EmpleadoRP>();
builder.Services.AddScoped<IPisoRepositorio, PisoRP>();
builder.Services.AddScoped<IBuildRepositorio, BuildRP>();
builder.Services.AddScoped<IHabitacionRepositorio, HabitacionRP>();
builder.Services.AddScoped<IReservaRepositorio, ReservaRP>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRP>();
builder.Services.AddScoped<IClienteRepositorio, ClienteRP>();

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

// El proyecto no tiene migraciones: crea la base de datos y sus tablas si no existen.
using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<HotelDbContext>().Database.EnsureCreated();
}


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
