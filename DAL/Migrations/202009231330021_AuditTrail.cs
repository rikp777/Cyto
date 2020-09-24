namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuditTrail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditTrailChangeLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ColumnName = c.String(),
                        ValueBefore = c.String(),
                        ValueAfter = c.String(),
                        AuditTrailEntity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AuditTrail", t => t.AuditTrailEntity_Id)
                .Index(t => t.AuditTrailEntity_Id);
            
            CreateTable(
                "dbo.AuditTrail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Identifier = c.String(),
                        PrimaryKey = c.String(),
                        CreatedAt = c.String(),
                        RequestBaseUrl = c.String(),
                        RequestMethod = c.String(),
                        RequestMethodColor = c.String(),
                        IpAddress = c.String(),
                        User_Id = c.Int(),
                        Company_Id = c.Int(),
                        Permission_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .ForeignKey("dbo.Company", t => t.Company_Id)
                .ForeignKey("dbo.Permission", t => t.Permission_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Company_Id)
                .Index(t => t.Permission_Id);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Description = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Company_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.Company_Id)
                .Index(t => t.Company_Id);
            
            CreateTable(
                "dbo.Experiment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Project_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Project", t => t.Project_Id)
                .Index(t => t.Project_Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Email = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Description = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Permission",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserCompany",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CompanyId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Company", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.UserProject",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ProjectId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.RolePermission",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        PermissionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.PermissionId })
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Permission", t => t.PermissionId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.PermissionId);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AuditTrail", "Permission_Id", "dbo.Permission");
            DropForeignKey("dbo.AuditTrail", "Company_Id", "dbo.Company");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.RolePermission", "PermissionId", "dbo.Permission");
            DropForeignKey("dbo.RolePermission", "RoleId", "dbo.Role");
            DropForeignKey("dbo.UserProject", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.UserProject", "UserId", "dbo.User");
            DropForeignKey("dbo.UserCompany", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.UserCompany", "UserId", "dbo.User");
            DropForeignKey("dbo.AuditTrail", "User_Id", "dbo.User");
            DropForeignKey("dbo.Experiment", "Project_Id", "dbo.Project");
            DropForeignKey("dbo.Project", "Company_Id", "dbo.Company");
            DropForeignKey("dbo.AuditTrailChangeLog", "AuditTrailEntity_Id", "dbo.AuditTrail");
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.RolePermission", new[] { "PermissionId" });
            DropIndex("dbo.RolePermission", new[] { "RoleId" });
            DropIndex("dbo.UserProject", new[] { "ProjectId" });
            DropIndex("dbo.UserProject", new[] { "UserId" });
            DropIndex("dbo.UserCompany", new[] { "CompanyId" });
            DropIndex("dbo.UserCompany", new[] { "UserId" });
            DropIndex("dbo.User", new[] { "Id" });
            DropIndex("dbo.Experiment", new[] { "Project_Id" });
            DropIndex("dbo.Project", new[] { "Company_Id" });
            DropIndex("dbo.AuditTrail", new[] { "Permission_Id" });
            DropIndex("dbo.AuditTrail", new[] { "Company_Id" });
            DropIndex("dbo.AuditTrail", new[] { "User_Id" });
            DropIndex("dbo.AuditTrailChangeLog", new[] { "AuditTrailEntity_Id" });
            DropTable("dbo.UserRole");
            DropTable("dbo.RolePermission");
            DropTable("dbo.UserProject");
            DropTable("dbo.UserCompany");
            DropTable("dbo.Permission");
            DropTable("dbo.Role");
            DropTable("dbo.User");
            DropTable("dbo.Experiment");
            DropTable("dbo.Project");
            DropTable("dbo.Company");
            DropTable("dbo.AuditTrail");
            DropTable("dbo.AuditTrailChangeLog");
        }
    }
}
