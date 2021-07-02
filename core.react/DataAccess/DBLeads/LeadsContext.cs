using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MovileWeb.DataAccess
{
    public partial class LeadsContext : DbContext
    {
        public LeadsContext()
        {
        }

        public LeadsContext(DbContextOptions<LeadsContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Pagamento> Pagamento { get; set; }
        public virtual DbSet<DebitoEmConta> DebitoEmConta { get; set; }
        public virtual DbSet<Parceiro> Parceiro { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<Plano> Plano { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<TipoPagamento> TipoPagamento { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Banco>(entity =>
            {
                entity.HasKey(e => e.BanId);

                entity.Property(e => e.BanId).HasColumnName("ban_id");

                entity.Property(e => e.BanDescricao)
                    .IsRequired()
                    .HasColumnName("ban_descricao")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DebitoEmConta>(entity =>
            {
                entity.HasKey(e => e.DebId);

                entity.ToTable("Debito_em_conta");

                entity.Property(e => e.DebId).HasColumnName("deb_id");

                entity.Property(e => e.DebAgencia)
                    .IsRequired()
                    .HasColumnName("deb_agencia")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DebBanId).HasColumnName("deb_ban_id");

                entity.Property(e => e.DebConta)
                    .IsRequired()
                    .HasColumnName("deb_conta")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DebPedId).HasColumnName("deb_ped_id");

                entity.HasOne(d => d.DebBan)
                    .WithMany(p => p.DebitoEmConta)
                    .HasForeignKey(d => d.DebBanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ban_tpb_Fkey");

                entity.HasOne(d => d.DebPed)
                    .WithMany(p => p.DebitoEmConta)
                    .HasForeignKey(d => d.DebPedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ban_ped_Fkey");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.CliId);

                entity.Property(e => e.CliId).HasColumnName("cli_id");

                entity.Property(e => e.CliCep)
                    .IsRequired()
                    .HasColumnName("cli_cep")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CliCpfCnpj)
                    .IsRequired()
                    .HasColumnName("cli_cpf_cnpj")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CliDate)
                    .HasColumnName("cli_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.CliEmail)
                    .IsRequired()
                    .HasColumnName("cli_email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CliEndBairro)
                    .IsRequired()
                    .HasColumnName("cli_end_bairro")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CliEndCidade)
                    .IsRequired()
                    .HasColumnName("cli_end_cidade")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CliEndComplemento)
                    .HasColumnName("cli_end_complemento")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CliEndLogradouro)
                    .IsRequired()
                    .HasColumnName("cli_end_logradouro")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CliEndNum)
                    .IsRequired()
                    .HasColumnName("cli_end_num")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CliEndUf)
                    .IsRequired()
                    .HasColumnName("cli_end_uf")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CliNome)
                    .IsRequired()
                    .HasColumnName("cli_nome")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CliTelefone)
                    .IsRequired()
                    .HasColumnName("cli_telefone")
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pagamento>(entity =>
            {
                entity.HasKey(e => e.PagId);

                entity.Property(e => e.PagId).HasColumnName("pag_id");

                entity.Property(e => e.PagAtivo).HasColumnName("pag_ativo");

                entity.Property(e => e.PagDescricao)
                    .IsRequired()
                    .HasColumnName("pag_descricao")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PagNumParcelas).HasColumnName("pag_num_parcelas");

                entity.Property(e => e.PagPlaId).HasColumnName("pag_pla_id");

                entity.Property(e => e.PagTipoPagamento).HasColumnName("pag_tipo_pagamento");

                entity.Property(e => e.PagValorParcela)
                    .HasColumnName("pag_valor_parcela")
                    .HasColumnType("money");

                entity.HasOne(d => d.PagPla)
                    .WithMany(p => p.Pagamento)
                    .HasForeignKey(d => d.PagPlaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pag_pla_Fkey");

                entity.HasOne(d => d.PagTipoPagamentoNavigation)
                    .WithMany(p => p.Pagamento)
                    .HasForeignKey(d => d.PagTipoPagamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pag_tipo_Fkey");
            });

            modelBuilder.Entity<Parceiro>(entity =>
            {
                entity.HasKey(e => e.ParId);

                entity.Property(e => e.ParId).HasColumnName("par_id");

                entity.Property(e => e.ParAtivo).HasColumnName("par_ativo");

                entity.Property(e => e.ParDescricao)
                    .IsRequired()
                    .HasColumnName("par_descricao")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.PedId);

                entity.HasIndex(e => new { e.PedCliId, e.PedPagId })
                    .HasName("IX_cliente_pagamento")
                    .IsUnique();

                entity.Property(e => e.PedId).HasColumnName("ped_id");

                entity.Property(e => e.PedCliId).HasColumnName("ped_cli_id");

                entity.Property(e => e.PedData)
                    .HasColumnName("ped_data")
                    .HasColumnType("datetime");

                entity.Property(e => e.PedPagId).HasColumnName("ped_pag_id");

                entity.HasOne(d => d.PedCli)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.PedCliId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ped_cli_Fkey");

                entity.HasOne(d => d.PedPag)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.PedPagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ped_pag_Fkey");
            });

            modelBuilder.Entity<Plano>(entity =>
            {
                entity.HasKey(e => e.PlaId);

                entity.Property(e => e.PlaId).HasColumnName("pla_id");

                entity.Property(e => e.PlaAtivo).HasColumnName("pla_ativo");

                entity.Property(e => e.PlaDescricao)
                    .IsRequired()
                    .HasColumnName("pla_descricao")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PlaProId).HasColumnName("pla_pro_id");

                entity.HasOne(d => d.PlaPro)
                    .WithMany(p => p.Plano)
                    .HasForeignKey(d => d.PlaProId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pla_pro_Fkey");
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.ProId);

                entity.Property(e => e.ProId).HasColumnName("pro_id");

                entity.Property(e => e.ProDescricao)
                    .IsRequired()
                    .HasColumnName("pro_descricao")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProParId).HasColumnName("pro_par_id");

                entity.HasOne(d => d.ProPar)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.ProParId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pro_par_Fkey");
            });

            modelBuilder.Entity<TipoPagamento>(entity =>
            {
                entity.HasKey(e => e.TppId);

                entity.ToTable("Tipo_pagamento");

                entity.Property(e => e.TppId).HasColumnName("tpp_id");

                entity.Property(e => e.TppDescricao)
                    .IsRequired()
                    .HasColumnName("tpp_descricao")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
