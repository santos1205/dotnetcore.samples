namespace LoadData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblMedicosUpd : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Medicos", "NrCartao");
            DropColumn("dbo.Medicos", "SenhaSimers");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Medicos", "SenhaSimers", c => c.String());
            AddColumn("dbo.Medicos", "NrCartao", c => c.String());
        }
    }
}
