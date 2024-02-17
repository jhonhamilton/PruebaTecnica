using System.Data.Entity;

namespace PruebaTecnica.ServicioWCF.DAL.Entities
{
    public partial class PruebaDbContext : DbContext
    {
        public PruebaDbContext()
            : base("name=PruebaDbContext")
        {
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
