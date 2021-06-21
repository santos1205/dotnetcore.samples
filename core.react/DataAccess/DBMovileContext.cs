using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovileWeb.DataAccess
{
    public class DBMovileContext : DbContext
    {
        public DBMovileContext()
        {
        }

        public DBMovileContext(DbContextOptions<DBMovileContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Lead> Lead { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer("data source=sqldesenv.proseg.com.br;initial catalog=movile_TESTE;user id=proseg;password=b123;MultipleActiveResultSets=True;App=EntityFramework");
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lead>(entity =>
            {
                entity.ToTable("lead");

                entity.Property(e => e.LedEmail)
                    .IsRequired()
                    .HasColumnName("led_email")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.LedNome)
                    .IsRequired()
                    .HasColumnName("led_nome")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });
        }
    }
}
