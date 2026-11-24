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
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        // Respuesta 400 de validación con título en español (el detalle de cada campo viene de las entidades).
        options.InvalidModelStateResponseFactory = contexto =>
            new Microsoft.AspNetCore.Mvc.BadRequestObjectResult(
                new Microsoft.AspNetCore.Mvc.ValidationProblemDetails(contexto.ModelState)
                {
                    Title = "Uno o más datos no son válidos.",
                    Status = StatusCodes.Status400BadRequest,
                });
    });
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Hotel Crown API", Version = "v1",
        Description = "API REST del Sistema de Gestión de Reservas de Hoteles." });

    var xml = Path.Combine(AppContext.BaseDirectory, $"{typeof(Program).Assembly.GetName().Name}.xml");
    if (File.Exists(xml))
        options.IncludeXmlComments(xml);
});

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
    // En desarrollo la app corre solo por HTTP (perfil "http"); en producción se fuerza HTTPS.
    app.UseHttpsRedirection();
}
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

// Permite que las pruebas de integración levanten la aplicación (WebApplicationFactory).
public partial class Program { }
