using HotelManagement.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DAL.Context
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Habitacion> Habitaciones { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Build> Builds { get; set; }
        public DbSet<Piso> Pisos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Empleado>().ToTable("Empleado");
            modelBuilder.Entity<Habitacion>().ToTable("Habitaciones");
            modelBuilder.Entity<Reserva>().ToTable("Reservas");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Build>().ToTable("Build");
            modelBuilder.Entity<Piso>().ToTable("Piso");

            // Las propiedades se llaman "IdX" (no "Id" ni "XId"), por lo que EF Core
            // no las detecta como clave primaria por convención: hay que declararlas.
            modelBuilder.Entity<Cliente>().HasKey(c => c.IdCliente);
            modelBuilder.Entity<Empleado>().HasKey(e => e.IdEmpleado);
            modelBuilder.Entity<Habitacion>().HasKey(h => h.IdHabitacion);
            modelBuilder.Entity<Reserva>().HasKey(r => r.IdReserva);
            modelBuilder.Entity<Usuario>().HasKey(u => u.IdUsuario);
            modelBuilder.Entity<Build>().HasKey(b => b.IdBuild);
            modelBuilder.Entity<Piso>().HasKey(p => p.IdPiso);

            // Precisión explícita para los montos (evita truncamientos en SQL Server).
            modelBuilder.Entity<Empleado>().Property(e => e.Salario).HasPrecision(18, 2);
            modelBuilder.Entity<Habitacion>().Property(h => h.Precio).HasPrecision(18, 2);
            modelBuilder.Entity<Habitacion>().Property(h => h.PrecioNoche).HasPrecision(18, 2);
            modelBuilder.Entity<Reserva>().Property(r => r.Total).HasPrecision(18, 2);
        }
    }
}
