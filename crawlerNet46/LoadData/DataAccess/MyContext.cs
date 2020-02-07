using System.Data.Entity;

namespace LoadData.DataAccess
{
    class MyContext : DbContext
    {
        static MyContext()
        {
            Database.SetInitializer<MyContext>(null);
        }

        public MyContext()
            : base("MyContext")
        { }

        public DbSet<Medico> Medicos { get; set; }

        public DbSet<Especialidade> Especialidades { get; set; }

        public DbSet<Contato> Contatos { get; set; }

                protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
