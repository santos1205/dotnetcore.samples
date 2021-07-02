using Microsoft.EntityFrameworkCore;
using API.Seguros.Proseg.Domain.Entidades;

namespace API.Seguros.Proseg.Infrastructure.Data
{
    public class ApiMultiCalculoContext : DbContext
    {
        public ApiMultiCalculoContext(DbContextOptions<ApiMultiCalculoContext> options) : base(options)
        {

        }

        public DbSet<Servico> Servico { get; set; }
        public DbSet<StatusAcesso> StatusAcesso { get; set; }
        public DbSet<LogApi> LogApi { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<UsuarioMultiCalculo> UsuarioMultiCalculo { get; set; }
        public DbSet<ValoresPadraoApi> ValoresPadraoApis { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //MultiSeguros
            modelBuilder.Entity<ValoresPadraoApi>(entity =>
            {
                entity.HasKey(e => e.IdValorPadrao);

                entity.ToTable("ValoresPadraoApi");

                entity.Property(e => e.IdValorPadrao).HasColumnName("vlp_IdValorPadrao");

                entity.Property(e => e.JsonValorPadrao).HasColumnName("vlp_JsonValorPadrao");

                entity.Property(e => e.BlnAtivo).HasColumnName("vlp_BlnAtivo");

            });
        }


    }
}
