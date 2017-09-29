namespace RelogioPonto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adiconarenum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Relogios", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Relogios", "Status");
        }
    }
}
