namespace LoadData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contatos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdMedico = c.Int(nullable: false),
                        TpContato = c.String(),
                        NrContato = c.String(),
                        Permite = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Especialidades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdMedico = c.Int(nullable: false),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Medicos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Sexo = c.String(),
                        NrCartao = c.String(),
                        SenhaSimers = c.String(),
                        Cpf = c.String(),
                        DataNascimento = c.String(),
                        Rg = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Medicos");
            DropTable("dbo.Especialidades");
            DropTable("dbo.Contatos");
        }
    }
}
