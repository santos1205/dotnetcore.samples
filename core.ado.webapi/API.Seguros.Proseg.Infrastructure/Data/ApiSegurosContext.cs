using API.Seguros.Proseg.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace API.Seguros.Proseg.Infrastructure.Data
{
    public partial class ApiSegurosContext : DbContext
    {
        public ApiSegurosContext()
        {
        }

        public ApiSegurosContext(DbContextOptions<ApiSegurosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Log> Log { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("data source=sqlhomolo.prosegnet.com.br;initial catalog=ApiSeguros;user id=proseg;password=b123;MultipleActiveResultSets=True;App=EntityFramework");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Log>(entity =>
            {
                entity.Property(e => e.LogDate)
                    .HasColumnName("log_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.LogEndpoint)
                    .IsRequired()
                    .HasColumnName("log_endpoint")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LogLoggedUser)
                    .IsRequired()
                    .HasColumnName("log_loggedUser")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LogParam)
                    .HasColumnName("log_param")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
        }
    }
}
