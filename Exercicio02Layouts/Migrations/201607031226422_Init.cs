namespace Exercicio02ScaffoldLayouts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ex02.Produtoes",
                c => new
                    {
                        ProdutoId = c.Guid(nullable: false),
                        Descricao = c.String(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProdutoGrupoId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ProdutoId)
                .ForeignKey("ex02.ProdutoGrupoes", t => t.ProdutoGrupoId, cascadeDelete: true)
                .Index(t => t.ProdutoGrupoId);
            
            CreateTable(
                "ex02.ProdutoGrupoes",
                c => new
                    {
                        ProdutoGrupoId = c.Guid(nullable: false),
                        Descricao = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProdutoGrupoId);
            
            CreateTable(
                "ex02.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "ex02.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("ex02.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("ex02.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "ex02.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "ex02.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ex02.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "ex02.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("ex02.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("ex02.AspNetUserRoles", "UserId", "ex02.AspNetUsers");
            DropForeignKey("ex02.AspNetUserLogins", "UserId", "ex02.AspNetUsers");
            DropForeignKey("ex02.AspNetUserClaims", "UserId", "ex02.AspNetUsers");
            DropForeignKey("ex02.AspNetUserRoles", "RoleId", "ex02.AspNetRoles");
            DropForeignKey("ex02.Produtoes", "ProdutoGrupoId", "ex02.ProdutoGrupoes");
            DropIndex("ex02.AspNetUserLogins", new[] { "UserId" });
            DropIndex("ex02.AspNetUserClaims", new[] { "UserId" });
            DropIndex("ex02.AspNetUsers", "UserNameIndex");
            DropIndex("ex02.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("ex02.AspNetUserRoles", new[] { "UserId" });
            DropIndex("ex02.AspNetRoles", "RoleNameIndex");
            DropIndex("ex02.Produtoes", new[] { "ProdutoGrupoId" });
            DropTable("ex02.AspNetUserLogins");
            DropTable("ex02.AspNetUserClaims");
            DropTable("ex02.AspNetUsers");
            DropTable("ex02.AspNetUserRoles");
            DropTable("ex02.AspNetRoles");
            DropTable("ex02.ProdutoGrupoes");
            DropTable("ex02.Produtoes");
        }
    }
}
