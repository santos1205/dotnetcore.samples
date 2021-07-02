using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MovileWeb.DataAccess
{
    public partial class CEPContext : DbContext
    {
        public CEPContext()
        {
        }

        public CEPContext(DbContextOptions<CEPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EctPais> EctPais { get; set; }
        public virtual DbSet<LogBairro> LogBairro { get; set; }
        public virtual DbSet<LogCpc> LogCpc { get; set; }
        public virtual DbSet<LogFaixaBairro> LogFaixaBairro { get; set; }
        public virtual DbSet<LogFaixaCpc> LogFaixaCpc { get; set; }
        public virtual DbSet<LogFaixaLocalidade> LogFaixaLocalidade { get; set; }
        public virtual DbSet<LogFaixaUf> LogFaixaUf { get; set; }
        public virtual DbSet<LogFaixaUop> LogFaixaUop { get; set; }
        public virtual DbSet<LogGrandeUsuario> LogGrandeUsuario { get; set; }
        public virtual DbSet<LogLocalidade> LogLocalidade { get; set; }
        public virtual DbSet<LogLogradouro> LogLogradouro { get; set; }
        public virtual DbSet<LogNumSec> LogNumSec { get; set; }
        public virtual DbSet<LogTipoLogradouro> LogTipoLogradouro { get; set; }
        public virtual DbSet<LogUnidOper> LogUnidOper { get; set; }
        public virtual DbSet<LogVarBai> LogVarBai { get; set; }
        public virtual DbSet<LogVarLoc> LogVarLoc { get; set; }
        public virtual DbSet<LogVarLog> LogVarLog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EctPais>(entity =>
            {
                entity.HasKey(e => e.PaiSg);

                entity.ToTable("ECT_PAIS");

                entity.Property(e => e.PaiSg)
                    .HasColumnName("PAI_SG")
                    .HasMaxLength(2)
                    .ValueGeneratedNever();

                entity.Property(e => e.PaiAbreviatura)
                    .IsRequired()
                    .HasColumnName("PAI_ABREVIATURA")
                    .HasMaxLength(36);

                entity.Property(e => e.PaiNoFrances)
                    .IsRequired()
                    .HasColumnName("PAI_NO_FRANCES")
                    .HasMaxLength(72);

                entity.Property(e => e.PaiNoIngles)
                    .IsRequired()
                    .HasColumnName("PAI_NO_INGLES")
                    .HasMaxLength(72);

                entity.Property(e => e.PaiNoPortugues)
                    .IsRequired()
                    .HasColumnName("PAI_NO_PORTUGUES")
                    .HasMaxLength(72);

                entity.Property(e => e.PaiSgAlternativa)
                    .IsRequired()
                    .HasColumnName("PAI_SG_ALTERNATIVA")
                    .HasMaxLength(3);
            });

            modelBuilder.Entity<LogBairro>(entity =>
            {
                entity.HasKey(e => e.BaiNu);

                entity.ToTable("LOG_BAIRRO");

                entity.HasIndex(e => e.BaiNu);

                entity.HasIndex(e => e.LocNu);

                entity.Property(e => e.BaiNu)
                    .HasColumnName("BAI_NU")
                    .ValueGeneratedNever();

                entity.Property(e => e.BaiNo)
                    .IsRequired()
                    .HasColumnName("BAI_NO")
                    .HasMaxLength(72);

                entity.Property(e => e.BaiNoAbrev)
                    .HasColumnName("BAI_NO_ABREV")
                    .HasMaxLength(36);

                entity.Property(e => e.LocNu).HasColumnName("LOC_NU");

                entity.Property(e => e.UfeSg)
                    .IsRequired()
                    .HasColumnName("UFE_SG")
                    .HasMaxLength(2);

                entity.HasOne(d => d.LocNuNavigation)
                    .WithMany(p => p.LogBairro)
                    .HasForeignKey(d => d.LocNu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LOG_BAIRRO_LOG_LOCALIDADE");
            });

            modelBuilder.Entity<LogCpc>(entity =>
            {
                entity.HasKey(e => e.CpcNu);

                entity.ToTable("LOG_CPC");

                entity.Property(e => e.CpcNu)
                    .HasColumnName("CPC_NU")
                    .ValueGeneratedNever();

                entity.Property(e => e.Cep)
                    .IsRequired()
                    .HasColumnName("CEP")
                    .HasMaxLength(8);

                entity.Property(e => e.CpcEndereco)
                    .IsRequired()
                    .HasColumnName("CPC_ENDERECO")
                    .HasMaxLength(100);

                entity.Property(e => e.CpcNo)
                    .IsRequired()
                    .HasColumnName("CPC_NO")
                    .HasMaxLength(172);

                entity.Property(e => e.LocNu).HasColumnName("LOC_NU");

                entity.Property(e => e.UfeSg)
                    .IsRequired()
                    .HasColumnName("UFE_SG")
                    .HasMaxLength(2);

                entity.HasOne(d => d.LocNuNavigation)
                    .WithMany(p => p.LogCpc)
                    .HasForeignKey(d => d.LocNu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LOG_CPC_LOG_LOCALIDADE");
            });

            modelBuilder.Entity<LogFaixaBairro>(entity =>
            {
                entity.HasKey(e => new { e.BaiNu, e.FcbCepIni });

                entity.ToTable("LOG_FAIXA_BAIRRO");

                entity.Property(e => e.BaiNu).HasColumnName("BAI_NU");

                entity.Property(e => e.FcbCepIni)
                    .HasColumnName("FCB_CEP_INI")
                    .HasMaxLength(8);

                entity.Property(e => e.FcbCepFim)
                    .IsRequired()
                    .HasColumnName("FCB_CEP_FIM")
                    .HasMaxLength(8);

                entity.HasOne(d => d.BaiNuNavigation)
                    .WithMany(p => p.LogFaixaBairro)
                    .HasForeignKey(d => d.BaiNu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LOG_FAIXA_BAIRRO_LOG_BAIRRO");
            });

            modelBuilder.Entity<LogFaixaCpc>(entity =>
            {
                entity.HasKey(e => new { e.CpcNu, e.CpcInicial });

                entity.ToTable("LOG_FAIXA_CPC");

                entity.Property(e => e.CpcNu).HasColumnName("CPC_NU");

                entity.Property(e => e.CpcInicial)
                    .HasColumnName("CPC_INICIAL")
                    .HasMaxLength(6);

                entity.Property(e => e.CpcFinal)
                    .IsRequired()
                    .HasColumnName("CPC_FINAL")
                    .HasMaxLength(6);

                entity.HasOne(d => d.CpcNuNavigation)
                    .WithMany(p => p.LogFaixaCpc)
                    .HasForeignKey(d => d.CpcNu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LOG_FAIXA_CPC_LOG_CPC");
            });

            modelBuilder.Entity<LogFaixaLocalidade>(entity =>
            {
                entity.HasKey(e => new { e.LocNu, e.LocCepIni });

                entity.ToTable("LOG_FAIXA_LOCALIDADE");

                entity.Property(e => e.LocNu).HasColumnName("LOC_NU");

                entity.Property(e => e.LocCepIni)
                    .HasColumnName("LOC_CEP_INI")
                    .HasMaxLength(8);

                entity.Property(e => e.LocCepFim)
                    .IsRequired()
                    .HasColumnName("LOC_CEP_FIM")
                    .HasMaxLength(8);

                entity.HasOne(d => d.LocNuNavigation)
                    .WithMany(p => p.LogFaixaLocalidade)
                    .HasForeignKey(d => d.LocNu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LOG_FAIXA_LOCALIDADE_LOG_LOCALIDADE");
            });

            modelBuilder.Entity<LogFaixaUf>(entity =>
            {
                entity.HasKey(e => new { e.UfeSg, e.UfeCepIni });

                entity.ToTable("LOG_FAIXA_UF");

                entity.Property(e => e.UfeSg)
                    .HasColumnName("UFE_SG")
                    .HasMaxLength(2);

                entity.Property(e => e.UfeCepIni)
                    .HasColumnName("UFE_CEP_INI")
                    .HasMaxLength(8);

                entity.Property(e => e.UfeCepFim)
                    .IsRequired()
                    .HasColumnName("UFE_CEP_FIM")
                    .HasMaxLength(8);
            });

            modelBuilder.Entity<LogFaixaUop>(entity =>
            {
                entity.HasKey(e => new { e.UopNu, e.FncInicial });

                entity.ToTable("LOG_FAIXA_UOP");

                entity.Property(e => e.UopNu).HasColumnName("UOP_NU");

                entity.Property(e => e.FncInicial).HasColumnName("FNC_INICIAL");

                entity.Property(e => e.FncFinal).HasColumnName("FNC_FINAL");

                entity.HasOne(d => d.UopNuNavigation)
                    .WithMany(p => p.LogFaixaUop)
                    .HasForeignKey(d => d.UopNu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LOG_FAIXA_UOP_LOG_UNID_OPER");
            });

            modelBuilder.Entity<LogGrandeUsuario>(entity =>
            {
                entity.HasKey(e => e.GruNu);

                entity.ToTable("LOG_GRANDE_USUARIO");

                entity.HasIndex(e => e.Cep)
                    .HasName("IX_Cep");

                entity.Property(e => e.GruNu)
                    .HasColumnName("GRU_NU")
                    .ValueGeneratedNever();

                entity.Property(e => e.BaiNu).HasColumnName("BAI_NU");

                entity.Property(e => e.Cep)
                    .IsRequired()
                    .HasColumnName("CEP")
                    .HasMaxLength(8);

                entity.Property(e => e.GruEndereco)
                    .IsRequired()
                    .HasColumnName("GRU_ENDERECO")
                    .HasMaxLength(100);

                entity.Property(e => e.GruNo)
                    .IsRequired()
                    .HasColumnName("GRU_NO")
                    .HasMaxLength(72);

                entity.Property(e => e.GruNoAbrev)
                    .HasColumnName("GRU_NO_ABREV")
                    .HasMaxLength(36);

                entity.Property(e => e.LocNu).HasColumnName("LOC_NU");

                entity.Property(e => e.LogNu).HasColumnName("LOG_NU");

                entity.Property(e => e.UfeSg)
                    .IsRequired()
                    .HasColumnName("UFE_SG")
                    .HasMaxLength(2);

                entity.HasOne(d => d.LocNuNavigation)
                    .WithMany(p => p.LogGrandeUsuario)
                    .HasForeignKey(d => d.LocNu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LOG_GRANDE_USUARIO_LOG_LOGRADOURO");
            });

            modelBuilder.Entity<LogLocalidade>(entity =>
            {
                entity.HasKey(e => e.LocNu);

                entity.ToTable("LOG_LOCALIDADE");

                entity.HasIndex(e => e.UfeSg);

                entity.Property(e => e.LocNu)
                    .HasColumnName("LOC_NU")
                    .ValueGeneratedNever();

                entity.Property(e => e.Cep)
                    .HasColumnName("CEP")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.LocInSit)
                    .IsRequired()
                    .HasColumnName("LOC_IN_SIT")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.LocInTipo)
                    .IsRequired()
                    .HasColumnName("LOC_IN_TIPO")
                    .HasMaxLength(1);

                entity.Property(e => e.LocNo)
                    .HasColumnName("LOC_NO")
                    .HasMaxLength(72);

                entity.Property(e => e.LocNoAbrev)
                    .HasColumnName("LOC_NO_ABREV")
                    .HasMaxLength(36);

                entity.Property(e => e.LocNuSub).HasColumnName("LOC_NU_SUB");

                entity.Property(e => e.MunNu).HasColumnName("MUN_NU");

                entity.Property(e => e.UfeSg)
                    .IsRequired()
                    .HasColumnName("UFE_SG")
                    .HasMaxLength(2);
            });

            modelBuilder.Entity<LogLogradouro>(entity =>
            {
                entity.HasKey(e => e.LogNu);

                entity.ToTable("LOG_LOGRADOURO");

                entity.HasIndex(e => e.Cep);

                entity.Property(e => e.LogNu)
                    .HasColumnName("LOG_NU")
                    .ValueGeneratedNever();

                entity.Property(e => e.BaiNuFim).HasColumnName("BAI_NU_FIM");

                entity.Property(e => e.BaiNuIni).HasColumnName("BAI_NU_INI");

                entity.Property(e => e.Cep)
                    .IsRequired()
                    .HasColumnName("CEP")
                    .HasMaxLength(8);

                entity.Property(e => e.LocNu).HasColumnName("LOC_NU");

                entity.Property(e => e.LogComplemento)
                    .HasColumnName("LOG_COMPLEMENTO")
                    .HasMaxLength(100);

                entity.Property(e => e.LogNo)
                    .IsRequired()
                    .HasColumnName("LOG_NO")
                    .HasMaxLength(100);

                entity.Property(e => e.LogNoAbrev)
                    .HasColumnName("LOG_NO_ABREV")
                    .HasMaxLength(36);

                entity.Property(e => e.LogStaTlo)
                    .HasColumnName("LOG_STA_TLO")
                    .HasMaxLength(1);

                entity.Property(e => e.TloTx)
                    .IsRequired()
                    .HasColumnName("TLO_TX")
                    .HasMaxLength(36);

                entity.Property(e => e.UfeSg)
                    .IsRequired()
                    .HasColumnName("UFE_SG")
                    .HasMaxLength(2);

                entity.HasOne(d => d.LocNuNavigation)
                    .WithMany(p => p.LogLogradouro)
                    .HasForeignKey(d => d.LocNu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LOG_LOGRADOURO_LOG_LOCALIDADE");
            });

            modelBuilder.Entity<LogNumSec>(entity =>
            {
                entity.HasKey(e => e.LogNu);

                entity.ToTable("LOG_NUM_SEC");

                entity.Property(e => e.LogNu)
                    .HasColumnName("LOG_NU")
                    .ValueGeneratedNever();

                entity.Property(e => e.SecInLado)
                    .IsRequired()
                    .HasColumnName("SEC_IN_LADO")
                    .HasMaxLength(1);

                entity.Property(e => e.SecNuFim)
                    .IsRequired()
                    .HasColumnName("SEC_NU_FIM")
                    .HasMaxLength(10);

                entity.Property(e => e.SecNuIni)
                    .IsRequired()
                    .HasColumnName("SEC_NU_INI")
                    .HasMaxLength(10);

                entity.HasOne(d => d.LogNuNavigation)
                    .WithOne(p => p.LogNumSec)
                    .HasForeignKey<LogNumSec>(d => d.LogNu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LOG_NUM_SEC_LOG_LOGRADOURO2");
            });

            modelBuilder.Entity<LogTipoLogradouro>(entity =>
            {
                entity.HasKey(e => e.TloNu);

                entity.ToTable("LOG_TIPO_LOGRADOURO");

                entity.Property(e => e.TloNu).HasColumnName("TLO_NU");

                entity.Property(e => e.TloTx)
                    .IsRequired()
                    .HasColumnName("TLO_TX")
                    .HasMaxLength(36);
            });

            modelBuilder.Entity<LogUnidOper>(entity =>
            {
                entity.HasKey(e => e.UopNu);

                entity.ToTable("LOG_UNID_OPER");

                entity.Property(e => e.UopNu)
                    .HasColumnName("UOP_NU")
                    .ValueGeneratedNever();

                entity.Property(e => e.BaiNu).HasColumnName("BAI_NU");

                entity.Property(e => e.Cep)
                    .IsRequired()
                    .HasColumnName("CEP")
                    .HasMaxLength(8);

                entity.Property(e => e.LocNu).HasColumnName("LOC_NU");

                entity.Property(e => e.LogNu).HasColumnName("LOG_NU");

                entity.Property(e => e.UfeSg)
                    .IsRequired()
                    .HasColumnName("UFE_SG")
                    .HasMaxLength(2);

                entity.Property(e => e.UopEndereco)
                    .IsRequired()
                    .HasColumnName("UOP_ENDERECO")
                    .HasMaxLength(200);

                entity.Property(e => e.UopInCp)
                    .HasColumnName("UOP_IN_CP")
                    .HasMaxLength(1);

                entity.Property(e => e.UopNo)
                    .IsRequired()
                    .HasColumnName("UOP_NO")
                    .HasMaxLength(100);

                entity.Property(e => e.UopNoAbrev)
                    .HasColumnName("UOP_NO_ABREV")
                    .HasMaxLength(36);

                entity.HasOne(d => d.LocNuNavigation)
                    .WithMany(p => p.LogUnidOper)
                    .HasForeignKey(d => d.LocNu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LOG_UNID_OPER_LOG_LOCALIDADE");
            });

            modelBuilder.Entity<LogVarBai>(entity =>
            {
                entity.HasKey(e => new { e.BaiNu, e.VdbNu });

                entity.ToTable("LOG_VAR_BAI");

                entity.Property(e => e.BaiNu).HasColumnName("BAI_NU");

                entity.Property(e => e.VdbNu).HasColumnName("VDB_NU");

                entity.Property(e => e.VdbTx)
                    .IsRequired()
                    .HasColumnName("VDB_TX")
                    .HasMaxLength(72);

                entity.HasOne(d => d.BaiNuNavigation)
                    .WithMany(p => p.LogVarBai)
                    .HasForeignKey(d => d.BaiNu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LOG_VAR_BAI_LOG_BAIRRO");
            });

            modelBuilder.Entity<LogVarLoc>(entity =>
            {
                entity.HasKey(e => new { e.LocNu, e.ValNu });

                entity.ToTable("LOG_VAR_LOC");

                entity.Property(e => e.LocNu).HasColumnName("LOC_NU");

                entity.Property(e => e.ValNu).HasColumnName("VAL_NU");

                entity.Property(e => e.ValTx)
                    .HasColumnName("VAL_TX")
                    .HasMaxLength(72);

                entity.HasOne(d => d.LocNuNavigation)
                    .WithMany(p => p.LogVarLoc)
                    .HasForeignKey(d => d.LocNu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LOG_VAR_LOC_LOG_LOCALIDADE");
            });

            modelBuilder.Entity<LogVarLog>(entity =>
            {
                entity.HasKey(e => new { e.LogNu, e.VloNu });

                entity.ToTable("LOG_VAR_LOG");

                entity.Property(e => e.LogNu).HasColumnName("LOG_NU");

                entity.Property(e => e.VloNu).HasColumnName("VLO_NU");

                entity.Property(e => e.TloTx)
                    .IsRequired()
                    .HasColumnName("TLO_TX")
                    .HasMaxLength(36);

                entity.Property(e => e.VloTx)
                    .IsRequired()
                    .HasColumnName("VLO_TX")
                    .HasMaxLength(150);

                entity.HasOne(d => d.LogNuNavigation)
                    .WithMany(p => p.LogVarLog)
                    .HasForeignKey(d => d.LogNu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LOG_VAR_LOG_LOG_LOGRADOURO");
            });
        }
    }
}
