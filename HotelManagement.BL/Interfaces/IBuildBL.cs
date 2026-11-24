using System;
using System.Collections.Generic;
using System.Text;

using HotelManagement.Entities;

namespace HotelManagement.BL.Interfaces
{
    public interface IBuildBL
    {
        Task<List<Build>> ObtenerTodosAsync();
        Task<Build?> ObtenerPorIdAsync(int id);
        Task CrearAsync(Build build);
        Task<bool> ActualizarAsync(Build build);
        Task<bool> EliminarAsync(int id);
    }
}