using core.migrationsSample.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace core.migrationsSample.DataAccess
{
    public class MyContext : DbContext
    {

        public MyContext(DbContextOptions<MyContext> options) 
            : base(options)
        { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
    }
}
