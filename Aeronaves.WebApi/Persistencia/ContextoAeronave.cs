using Aeronaves.WebApi.Modelo;
using Microsoft.EntityFrameworkCore;

namespace Aeronaves.WebApi.Persistencia
{
    public class ContextoAeronave : DbContext
    {
        //creando instancia a la BD
        public ContextoAeronave(DbContextOptions<ContextoAeronave> options) : base(options) { }
     
        //convertir a tipo entidad, para la migracion con c# a SQL creara las nuevas tablas        
        public DbSet<Aeronave> Aeronave { get; set; }
        public DbSet<AeronaveAsientos> AeronaveAsientos { get; set; }

    }
}
