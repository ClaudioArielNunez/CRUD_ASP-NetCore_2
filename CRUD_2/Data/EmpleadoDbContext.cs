using Microsoft.EntityFrameworkCore;

namespace CRUD_2.Data
{
    public class EmpleadoDbContext : DbContext
    {
        public EmpleadoDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<EmpleadoDbContext> Empleados { get; set; }
    }
}
