using CRUD_2.Models.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CRUD_2.Data
{
    public class EmpleadoDbContext : DbContext
    {
        public EmpleadoDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Empleado> Empleados { get; set; }
    }
}
