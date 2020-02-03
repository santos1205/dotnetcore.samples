﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using core.migrationsSample.DataAccess;

namespace core.migrationsSample.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20200203172241_initialMigration")]
    partial class initialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("core.migrationsSample.Dominio.Cliente", b =>
                {
                    b.Property<int>("IdCliente")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("IdCliente");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("core.migrationsSample.Dominio.Pedido", b =>
                {
                    b.Property<int>("IdPedido")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired();

                    b.HasKey("IdPedido");

                    b.ToTable("Pedidos");
                });
#pragma warning restore 612, 618
        }
    }
}
