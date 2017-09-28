namespace RelogioPonto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracaoinicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Relogios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Descricao = c.String(),
                        Login = c.String(nullable: false),
                        Senha = c.String(nullable: false),
                        Ip = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Relogios");
        }
    }
}
