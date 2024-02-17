using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models.Model;

namespace PruebaTecnica.DataAcces
{
    public class PruebaDbContext: IdentityDbContext
    {
        public PruebaDbContext(DbContextOptions<PruebaDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserLogin>(entityTypeBuilder =>
                  entityTypeBuilder.ToTable("UserLogin")
                  .Property(u => u.Id)
                  .HasColumnName("UserLoginId")
              );

            builder.Entity<IdentityUser>(entityTypeBuilder =>
                entityTypeBuilder.ToTable("UserLogin")
            );

            builder.Entity<IdentityRole>(entityTypeBuilder =>
                entityTypeBuilder.ToTable("Roles")
            );

            builder.Entity<IdentityUserClaim<string>>(entityTypeBuilder =>
                entityTypeBuilder.ToTable("UserLoginClaim")
                .Property(u => u.UserId)
                .HasColumnName("UserLoginId")
            );

            builder.Entity<IdentityUserRole<string>>(entityTypeBuilder =>
                entityTypeBuilder.ToTable("UserLoginRole")
                .Property(u => u.UserId)
                .HasColumnName("UserLoginId")
            );

            builder.Entity<IdentityUserLogin<string>>(entityTypeBuilder =>
                entityTypeBuilder.ToTable("UserLoginLogin")
                .Property(u => u.UserId)
                .HasColumnName("UserLoginId")
            );

            builder.Entity<IdentityRoleClaim<string>>(entityTypeBuilder =>
                entityTypeBuilder.ToTable("RolesClaim")
            );

            builder.Entity<IdentityUserToken<string>>(entityTypeBuilder =>
                entityTypeBuilder.ToTable("UserLoginToken")
            );
            builder.Entity<IdentityUserToken<string>>(entityTypeBuilder =>
                entityTypeBuilder.ToTable("UserLoginToken")
                .Property(u => u.UserId)
                .HasColumnName("UserLoginId")
            );
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UserLogin> UserLogin { get; set; }
    }
}
