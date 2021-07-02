using Microsoft.EntityFrameworkCore;
using API.Seguros.Proseg.Domain.Entidades;

namespace API.Seguros.Proseg.Infrastructure.Data
{
    public class MultCalcSegContext : DbContext
    {
        public MultCalcSegContext(DbContextOptions<MultCalcSegContext> options) : base(options)
        {

        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<VeiculosBradesco> VeiculosBradesco { get; set; }
        public DbSet<VeiculosPlaca> VeiculosPlaca { get; set; }
        public DbSet<AtividadePrincipal> AtividadePrincipal { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Resultados> Resultados { get; set; }


    }
}

