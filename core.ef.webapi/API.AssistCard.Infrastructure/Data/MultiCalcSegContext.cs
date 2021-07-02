using API.Viagem.Domain;
using API.Viagem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Viagem.Infrastructure
{
    public class MultCalcSegContext : DbContext
    {
        public MultCalcSegContext()
        {
        }

        public MultCalcSegContext(DbContextOptions<MultCalcSegContext> options) : base(options)
        {

        }


        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<TblViagemCotacoes> TblViagemCotacoes { get; set; }
        public virtual DbSet<TblViagemCotacoesResultados> TblViagemCotacoesResultados { get; set; }
        public virtual DbSet<TblViagemCotacoesResultadosPassageiros> TblViagemCotacoesResultadosPassageiros { get; set; }
        public virtual DbSet<TblViagemCotacoesResultadosProdutos> TblViagemCotacoesResultadosProdutos { get; set; }
        public virtual DbSet<TblViagemEmissoes> TblViagemEmissoes { get; set; }
        public virtual DbSet<TblViagemEmissoesRetorno> TblViagemEmissoesRetorno { get; set; }
        public virtual DbSet<TblViagemPassageiros> TblViagemPassageiros { get; set; }
        public virtual DbSet<TblViagemPassageirosCotacoes> TblViagemPassageirosCotacoes { get; set; }
        public virtual DbSet<TblViagemVouchers> TblViagemVouchers { get; set; }
        public virtual DbSet<TblViagemNacionalidade> TblViagemNacionalidade { get; set; }
        public virtual DbSet<TblViagemDestino> TblViagemDestino { get; set; }
        public virtual DbSet<TblViagemFormaPagamento> TblViagemFormaPagamento { get; set; }
        public virtual DbSet<TblViagemOrigemParceiro> TblViagemOrigemParceiro { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=192.168.3.20;Database=MultCalcSeg;User Id=proseg;Password=b123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<TblViagemCotacoes>(entity =>
            {
                entity.HasKey(e => e.CotIdCotacao);

                entity.ToTable("tblViagemCotacoes");

                entity.Property(e => e.CotIdCotacao).HasColumnName("cot_IdCotacao");

                entity.Property(e => e.CotDesIdDestino).HasColumnName("cot_des_IdDestino");

                entity.Property(e => e.CotDtCotacao).HasColumnName("cot_DtCotacao");

                entity.Property(e => e.CotDtPartida)
                    .HasColumnName("cot_DtPartida")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.CotDtRetorno)
                    .HasColumnName("cot_DtRetorno")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.CotFpgIdFormaPagamento).HasColumnName("cot_fpg_IdFormaPagamento");

                entity.Property(e => e.CotOrpIdOrigemParceiro).HasColumnName("cot_orp_IdOrigemParceiro");

                entity.Property(e => e.CotIdCorretor).HasColumnName("cot_IdCorretor");

                entity.Property(e => e.CotIdEstipulante).HasColumnName("cot_IdEstipulante");

                entity.Property(e => e.CotIdPdv).HasColumnName("cot_IdPdv");

                entity.Property(e => e.CotNomeUsuarioEmissao)
                    .IsRequired()
                    .HasColumnName("cot_NomeUsuarioEmissao");

            });

            modelBuilder.Entity<TblViagemCotacoesResultados>(entity =>
            {
                entity.HasKey(e => e.RecIdResultado);

                entity.ToTable("tblViagemCotacoesResultados");

                entity.HasIndex(e => e.RecCotIdCotacao)
                    .HasName("IX_tblViagemCotacoesResultados")
                    .IsUnique();

                entity.Property(e => e.RecIdResultado).HasColumnName("rec_IdResultado");

                entity.Property(e => e.RecCotIdCotacao).HasColumnName("rec_cot_IdCotacao");

                entity.Property(e => e.RecDtResultado).HasColumnName("rec_DtResultado");

                entity.HasOne(d => d.RecCotIdCotacaoNavigation)
                    .WithOne(p => p.TblViagemCotacoesResultados)
                    .HasForeignKey<TblViagemCotacoesResultados>(d => d.RecCotIdCotacao)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_tblViagemCotacoesResultados_tblViagemCotacoes");
            });

            modelBuilder.Entity<TblViagemCotacoesResultadosPassageiros>(entity =>
            {
                entity.HasKey(e => e.RpsIdResultadoPassageiro)
                    .HasName("PK_tblViagemCotacoesPassageiros");

                entity.ToTable("tblViagemCotacoesResultadosPassageiros");

                entity.HasIndex(e => new { e.RpsRprIdResultadoProduto, e.RpsPsgIdPassageiro })
                    .HasName("IX_tblViagemCotacoesResultadosPassageiros")
                    .IsUnique();

                entity.Property(e => e.RpsIdResultadoPassageiro).HasColumnName("rps_IdResultadoPassageiro");

                entity.Property(e => e.RpsPsgIdPassageiro).HasColumnName("rps_psg_IdPassageiro");

                entity.Property(e => e.RpsRprIdResultadoProduto).HasColumnName("rps_rpr_IdResultadoProduto");

                entity.Property(e => e.RpsValorNetoUnitario)
                    .HasColumnName("rps_ValorNetoUnitario")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RpsValorUnitAssistencia)
                    .HasColumnName("rps_ValorUnitAssistencia")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RpsValorUnitSeguro)
                    .HasColumnName("rps_ValorUnitSeguro")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RpsValorUnitario)
                    .HasColumnName("rps_ValorUnitario")
                    .HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.RpsPsgIdPassageiroNavigation)
                    .WithMany(p => p.TblViagemCotacoesResultadosPassageiros)
                    .HasForeignKey(d => d.RpsPsgIdPassageiro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblViagemCotacoesResultadosPassageiros_tblViagemPassageiros");

                entity.HasOne(d => d.RpsRprIdResultadoProdutoNavigation)
                    .WithMany(p => p.TblViagemCotacoesResultadosPassageiros)
                    .HasForeignKey(d => d.RpsRprIdResultadoProduto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblViagemCotacoesResultadosPassageiros_tblViagemCotacoesResultadosProdutos");
            });

            modelBuilder.Entity<TblViagemCotacoesResultadosProdutos>(entity =>
            {
                entity.HasKey(e => e.RprIdResultadoProduto);

                entity.ToTable("tblViagemCotacoesResultadosProdutos");

                entity.Property(e => e.RprIdResultadoProduto).HasColumnName("rpr_IdResultadoProduto");

                entity.Property(e => e.RprBancoDias).HasColumnName("rpr_BancoDias");

                entity.Property(e => e.RprCambio)
                    .HasColumnName("rpr_Cambio")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RprCodigo)
                    .IsRequired()
                    .HasColumnName("rpr_Codigo");

                entity.Property(e => e.RprModalidade)
                    .IsRequired()
                    .HasColumnName("rpr_Modalidade");

                entity.Property(e => e.RprMoeda).HasColumnName("rpr_Moeda");

                entity.Property(e => e.RprNomeProduto)
                    .IsRequired()
                    .HasColumnName("rpr_NomeProduto");

                entity.Property(e => e.RprRecIdResultado).HasColumnName("rpr_rec_IdResultado");

                entity.Property(e => e.RprSaldoBancoDias).HasColumnName("rpr_SaldoBancoDias");

                entity.Property(e => e.RprTarifa).HasColumnName("rpr_Tarifa");

                entity.Property(e => e.RprTarifaBruta).HasColumnName("rpr_TarifaBruta");

                entity.Property(e => e.RprTotalPassageiros).HasColumnName("rpr_TotalPassageiros");

                entity.Property(e => e.RprValorTotal)
                    .HasColumnName("rpr_ValorTotal")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RprValorNetoTotal)
                    .HasColumnName("rpr_ValorNetoTotal")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RprValorTaxaGateway)
                    .HasColumnName("rpr_ValorTaxaGateway")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RprValorTotalOrigem)
                    .HasColumnName("rpr_ValorTotalOrigem")
                    .HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.RprRecIdResultadoNavigation)
                    .WithMany(p => p.TblViagemCotacoesResultadosProdutos)
                    .HasForeignKey(d => d.RprRecIdResultado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblViagemCotacoesResultadosProdutos_tblViagemCotacoesResultados");
            });

            modelBuilder.Entity<TblViagemCotacoesResultadosProdutoParcelamento>(entity =>
{
    entity.HasKey(e => e.RppIdParcelamentoProduto);

    entity.ToTable("tblViagemCotacoesResultadosProdutoParcelamento");

    entity.Property(e => e.RppIdParcelamentoProduto).HasColumnName("rpp_IdParcelamentoProduto");

    entity.Property(e => e.RppBandeiraCartao)
        .IsRequired()
        .HasColumnName("rpp_BandeiraCartao")
        .HasMaxLength(2)
        .IsUnicode(false);

    entity.Property(e => e.RppFactorAcrecimo)
        .IsRequired()
        .HasColumnName("rpp_FactorAcrecimo")
        .HasMaxLength(50)
        .IsUnicode(false);

    entity.Property(e => e.RppNumeroParcelas).HasColumnName("rpp_NumeroParcelas");

    entity.Property(e => e.RppRprIdResultadoProduto).HasColumnName("rpp_rpr_IdResultadoProduto");

    entity.Property(e => e.RppValorTotalParcela)
        .HasColumnName("rpp_ValorTotalParcela")
        .HasColumnType("numeric(18, 2)");

    entity.HasOne(d => d.RppRprIdResultadoProdutoNavigation)
        .WithMany(p => p.TblViagemCotacoesResultadosProdutoParcelamento)
        .HasForeignKey(d => d.RppRprIdResultadoProduto)
        .OnDelete(DeleteBehavior.ClientSetNull)
        .HasConstraintName("FK_tblViagemCotacoesResultadosProdutoParcelamento_tblViagemCotacoesResultadosProdutos");
});

            modelBuilder.Entity<TblViagemEmissoes>(entity =>
            {
                entity.HasKey(e => e.EmiIdEmissao);

                entity.ToTable("tblViagemEmissoes");

                entity.HasIndex(e => e.EmiCotIdCotacao)
                    .HasName("IX_tblViagemEmissoes")
                    .IsUnique();

                entity.HasIndex(e => e.EmiRprIdResultadoProduto)
                    .HasName("IX_tblViagemEmissoes_1");

                entity.Property(e => e.EmiIdEmissao).HasColumnName("emi_IdEmissao");

                entity.Property(e => e.EmiCotIdCotacao).HasColumnName("emi_cot_IdCotacao");

                entity.Property(e => e.EmiRprIdResultadoProduto).HasColumnName("emi_rpr_IdResultadoProduto");

                entity.Property(e => e.EmiOrpIdOrigemParceiro).HasColumnName("emi_orp_IdOrigemParceiro");

                entity.Property(e => e.EmiPagamentoComCartao).HasColumnName("emi_PagamentoComCartao");

                entity.Property(e => e.EmiBandeiraCartao).HasColumnName("emi_BandeiraCartao");

                entity.Property(e => e.EmiDocumentoCartao)
                   .IsRequired()
                   .HasColumnName("emi_DocumentoCartao")
                   .HasMaxLength(16);

                entity.Property(e => e.EmiNumeroCartao)
                    .IsRequired()
                    .HasColumnName("emi_NumeroCartao")
                    .HasMaxLength(16);

                entity.Property(e => e.EmiValidadeCartao)
                    .IsRequired()
                    .HasColumnName("emi_ValidadeCartao")
                    .HasMaxLength(6);

                entity.Property(e => e.EmiParcelas).HasColumnName("emi_Parcelas");

                entity.Property(e => e.EmiPlanoFamiliar).HasColumnName("emi_PlanoFamiliar");

                entity.Property(e => e.EmiNomeCartao)
                    .IsRequired()
                    .HasColumnName("emi_NomeCartao");

                entity.Property(e => e.EmiNomeUsuarioEmissao)
                    .IsRequired()
                    .HasColumnName("emi_NomeUsuarioEmissao");

                entity.Property(e => e.EmiStatus).HasColumnName("emi_Status");

                entity.Property(e => e.EmiBlnTransmitidoMultiSeguro).HasColumnName("emi_blnTransmitidoMultiSeguro");

                entity.Property(e => e.EmiDtEmissao)
                    .HasColumnName("emi_dtEmissao")
                    .HasColumnType("smalldatetime");

                entity.HasOne(d => d.EmiCotIdCotacaoNavigation)
                    .WithOne(p => p.TblViagemEmissoes)
                    .HasForeignKey<TblViagemEmissoes>(d => d.EmiCotIdCotacao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblViagemEmissoes_tblViagemCotacoes");
            });

            modelBuilder.Entity<TblViagemEmissoesRetorno>(entity =>
            {
                entity.HasKey(e => e.EmrIdEmissaoRetorno);

                entity.ToTable("tblViagemEmissoesRetorno");

                entity.HasIndex(e => e.EmrEmiIdEmissao)
                    .HasName("IX_tblViagemEmissoesRetorno")
                    .IsUnique();

                entity.Property(e => e.EmrIdEmissaoRetorno).HasColumnName("emr_IdEmissaoRetorno");

                entity.Property(e => e.EmrBlnSucesso).HasColumnName("emr_blnSucesso");

                entity.Property(e => e.EmrDtEmissaoRetorno).HasColumnName("emr_dtEmissaoRetorno");

                entity.Property(e => e.EmrEmiIdEmissao).HasColumnName("emr_emi_IdEmissao");

                entity.Property(e => e.EmrGrupoVoucher).HasColumnName("emr_GrupoVoucher");

                entity.Property(e => e.EmrTotalDias).HasColumnName("emr_TotalDias");

                entity.HasOne(d => d.EmrEmiIdEmissaoNavigation)
                    .WithOne(p => p.TblViagemEmissoesRetorno)
                    .HasForeignKey<TblViagemEmissoesRetorno>(d => d.EmrEmiIdEmissao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblViagemEmissoesRetorno_tblViagemEmissoes");
            });

            modelBuilder.Entity<TblViagemPassageiros>(entity =>
            {
                entity.HasKey(e => e.PsgIdPassageiro);

                entity.ToTable("tblViagemPassageiros");

                entity.HasIndex(e => e.PsgCpf)
                    .HasName("IX_tblViagemPassageiros")
                    .IsUnique();

                entity.Property(e => e.PsgIdPassageiro).HasColumnName("psg_IdPassageiro");

                entity.Property(e => e.PsgBairro).HasColumnName("psg_Bairro");

                entity.Property(e => e.PsgBlnAtivo).HasColumnName("psg_blnAtivo");

                entity.Property(e => e.PsgCidade).HasColumnName("psg_Cidade");

                entity.Property(e => e.PsgCodigoPostal)
                    .HasColumnName("psg_CodigoPostal")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.PsgComplemento).HasColumnName("psg_Complemento");

                entity.Property(e => e.PsgCpf)
                    .IsRequired()
                    .HasColumnName("psg_Cpf")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.PsgDadoAdicional1).HasColumnName("psg_DadoAdicional1");

                entity.Property(e => e.PsgDadoAdicional2).HasColumnName("psg_DadoAdicional2");

                entity.Property(e => e.PsgDtNascimento)
                    .HasColumnName("psg_DtNascimento")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.PsgEmail)
                    .IsRequired()
                    .HasColumnName("psg_Email");

                entity.Property(e => e.PsgEndereco).HasColumnName("psg_Endereco");

                entity.Property(e => e.PsgEstado)
                    .HasColumnName("psg_Estado")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.PsgGenero)
                    .HasColumnName("psg_Genero")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.PsgNacIdNacionalidade).HasColumnName("psg_nac_IdNacionalidade");

                entity.Property(e => e.PsgNome)
                    .IsRequired()
                    .HasColumnName("psg_Nome");

                entity.Property(e => e.PsgNomeContato).HasColumnName("psg_NomeContato");

                entity.Property(e => e.PsgNumeroDocumento).HasColumnName("psg_NumeroDocumento");

                entity.Property(e => e.PsgNumeroEndereco).HasColumnName("psg_NumeroEndereco");

                entity.Property(e => e.PsgPaisDomicilio).HasColumnName("psg_PaisDomicilio");

                entity.Property(e => e.PsgSobrenome)
                    .IsRequired()
                    .HasColumnName("psg_Sobrenome");

                entity.Property(e => e.PsgTelefone)
                    .IsRequired()
                    .HasColumnName("psg_Telefone");

                entity.Property(e => e.PsgTelefoneContato).HasColumnName("psg_TelefoneContato");

                entity.Property(e => e.PsgTipoDocumento).HasColumnName("psg_TipoDocumento");
            });

            modelBuilder.Entity<TblViagemPassageirosCotacoes>(entity =>
            {
                entity.HasKey(e => e.PctIdPassageiroCotacao);

                entity.ToTable("tblViagemPassageiros_Cotacoes");

                entity.HasIndex(e => new { e.PctCotIdCotacao, e.PctPsgIdPassageiro })
                    .HasName("IX_tblViagemPassageiros_Cotacoes")
                    .IsUnique();

                entity.Property(e => e.PctIdPassageiroCotacao).HasColumnName("pct_IdPassageiroCotacao");

                entity.Property(e => e.PctCotIdCotacao).HasColumnName("pct_cot_IdCotacao");

                entity.Property(e => e.PctPsgIdPassageiro).HasColumnName("pct_psg_IdPassageiro");

                entity.Property(e => e.PctBolPassageiroPrincipal).HasColumnName("pct_BolPassageiroPrincipal");

                entity.HasOne(d => d.PctCotIdCotacaoNavigation)
                    .WithMany(p => p.TblViagemPassageirosCotacoes)
                    .HasForeignKey(d => d.PctCotIdCotacao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblViagemPassageiros_Cotacoes_tblViagemCotacoes");

                entity.HasOne(d => d.PctPsgIdPassageiroNavigation)
                    .WithMany(p => p.TblViagemPassageirosCotacoes)
                    .HasForeignKey(d => d.PctPsgIdPassageiro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblViagemPassageiros_Cotacoes_tblViagemPassageiros");
            });

            modelBuilder.Entity<TblViagemVouchers>(entity =>
            {
                entity.HasKey(e => e.VchIdVoucher);

                entity.ToTable("tblViagemVouchers");

                entity.Property(e => e.VchIdVoucher).HasColumnName("vch_IdVoucher");

                entity.Property(e => e.VchBlnAtivo).HasColumnName("vch_BlnAtivo");

                entity.Property(e => e.VchCambio)
                    .HasColumnName("vch_Cambio")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.VchAcrescimoFinanceiro)
                   .HasColumnName("vch_AcrescimoFinanceiro")
                   .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.VchValorTaxaGateway)
                   .HasColumnName("vch_ValorTaxaGateway")
                   .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.VchCodigo).HasColumnName("vch_Codigo");

                entity.Property(e => e.VchCodigoApolice)
                    .IsRequired()
                    .HasColumnName("vch_CodigoApolice");

                entity.Property(e => e.VchEmrIdEmissaoRetorno).HasColumnName("vch_emr_IdEmissaoRetorno");

                entity.Property(e => e.VchPsgIdPassageiro).HasColumnName("vch_psg_IdPassageiro");

                entity.Property(e => e.VchEstadoVoucher).HasColumnName("vch_EstadoVoucher");

                entity.Property(e => e.VchMoeda).HasColumnName("vch_Moeda");

                entity.Property(e => e.VchNomeCliente)
                    .IsRequired()
                    .HasColumnName("vch_NomeCliente");

                entity.Property(e => e.VchNumero)
                    .IsRequired()
                    .HasColumnName("vch_Numero");

                entity.Property(e => e.VchNumeroDocumento)
                    .HasColumnName("vch_NumeroDocumento")
                    .HasMaxLength(11);

                entity.Property(e => e.VchPremioSeguroTotal)
                    .HasColumnName("vch_PremioSeguroTotal")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.VchTipoDocumento).HasColumnName("vch_TipoDocumento");

                entity.Property(e => e.VchValor)
                    .HasColumnName("vch_Valor")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.VchValorAssistencia)
                    .HasColumnName("vch_ValorAssistencia")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.VchValorIof)
                    .HasColumnName("vch_ValorIOF")
                    .HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.VchEmrIdEmissaoRetornoNavigation)
                    .WithMany(p => p.TblViagemVouchers)
                    .HasForeignKey(d => d.VchEmrIdEmissaoRetorno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblViagemVouchers_tblViagemEmissoesRetorno");

                entity.HasOne(d => d.VchPsgIdPassageiroNavigation)
                    .WithMany(p => p.TblViagemVouchers)
                    .HasForeignKey(d => d.VchPsgIdPassageiro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblViagemVouchers_tblViagemPassageiros");
            });

            modelBuilder.Entity<TblViagemNacionalidade>(entity =>
            {
                entity.HasKey(e => e.NacIdNacionalidade);

                entity.Property(e => e.NacIdNacionalidade).HasColumnName("nac_IdNacionalidade");

                entity.Property(e => e.NacBlnAtivo).HasColumnName("nac_BlnAtivo");

                entity.Property(e => e.NacDescricao)
                    .IsRequired()
                    .HasColumnName("nac_Descricao")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblViagemDestino>(entity =>
            {
                entity.HasKey(e => e.DesIdDestino);

                entity.ToTable("tblViagemDestino");

                entity.Property(e => e.DesIdDestino).HasColumnName("des_IdDestino");

                entity.Property(e => e.DesBlnAtivo).HasColumnName("des_BlnAtivo");

                entity.Property(e => e.DesDescricaoDestino)
                    .IsRequired()
                    .HasColumnName("des_DescricaoDestino")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblViagemFormaPagamento>(entity =>
            {
                entity.HasKey(e => e.FpgIdFormaPagamento);

                entity.ToTable("tblViagemFormaPagamento");

                entity.Property(e => e.FpgIdFormaPagamento).HasColumnName("fpg_IdFormaPagamento");

                entity.Property(e => e.FpgBlnAtivo).HasColumnName("fpg_blnAtivo");

                entity.Property(e => e.FpgDescricao)
                    .IsRequired()
                    .HasColumnName("fpg_Descricao")
                    .IsUnicode(false);
            });


            modelBuilder.Entity<TblViagemOrigemParceiro>(entity =>
            {
                entity.HasKey(e => e.OrpIdOrigemParceiro);

                entity.ToTable("tblViagemOrigemParceiro");

                entity.Property(e => e.OrpIdOrigemParceiro).HasColumnName("orp_IdOrigemParceiro");

                entity.Property(e => e.OrpBlnAtivo).HasColumnName("orp_blnAtivo");

                entity.Property(e => e.OrpDescricao)
                    .IsRequired()
                    .HasColumnName("orp_Descricao")
                    .IsUnicode(false);
            });
        }
    }
}
