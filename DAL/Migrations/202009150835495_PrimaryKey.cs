namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrimaryKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuditTrail", "PrimaryKey", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AuditTrail", "PrimaryKey");
        }
    }
}
