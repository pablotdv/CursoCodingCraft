namespace Exercicio05WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ex05.Arquivo",
                c => new
                    {
                        ArquivoId = c.Guid(nullable: false),
                        Nome = c.String(nullable: false),
                        MimeType = c.String(nullable: false),
                        DiretorioId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ArquivoId)
                .ForeignKey("ex05.Diretorio", t => t.DiretorioId, cascadeDelete: true)
                .Index(t => t.DiretorioId);
            
            CreateTable(
                "ex05.Diretorio",
                c => new
                    {
                        DiretorioId = c.Guid(nullable: false),
                        Nome = c.String(nullable: false),
                        DiretorioPaiId = c.Guid(nullable: false),
                        UsuarioId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DiretorioId)
                .ForeignKey("ex05.Diretorio", t => t.DiretorioPaiId)
                .ForeignKey("ex05.AspNetUsers", t => t.UsuarioId)
                .Index(t => t.DiretorioPaiId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "ex05.AspNetUsers",
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
                "ex05.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ex05.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "ex05.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("ex05.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "ex05.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("ex05.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("ex05.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "ex05.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("ex05.AspNetUserRoles", "RoleId", "ex05.AspNetRoles");
            DropForeignKey("ex05.Diretorio", "UsuarioId", "ex05.AspNetUsers");
            DropForeignKey("ex05.AspNetUserRoles", "UserId", "ex05.AspNetUsers");
            DropForeignKey("ex05.AspNetUserLogins", "UserId", "ex05.AspNetUsers");
            DropForeignKey("ex05.AspNetUserClaims", "UserId", "ex05.AspNetUsers");
            DropForeignKey("ex05.Diretorio", "DiretorioPaiId", "ex05.Diretorio");
            DropForeignKey("ex05.Arquivo", "DiretorioId", "ex05.Diretorio");
            DropIndex("ex05.AspNetRoles", "RoleNameIndex");
            DropIndex("ex05.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("ex05.AspNetUserRoles", new[] { "UserId" });
            DropIndex("ex05.AspNetUserLogins", new[] { "UserId" });
            DropIndex("ex05.AspNetUserClaims", new[] { "UserId" });
            DropIndex("ex05.AspNetUsers", "UserNameIndex");
            DropIndex("ex05.Diretorio", new[] { "UsuarioId" });
            DropIndex("ex05.Diretorio", new[] { "DiretorioPaiId" });
            DropIndex("ex05.Arquivo", new[] { "DiretorioId" });
            DropTable("ex05.AspNetRoles");
            DropTable("ex05.AspNetUserRoles");
            DropTable("ex05.AspNetUserLogins");
            DropTable("ex05.AspNetUserClaims");
            DropTable("ex05.AspNetUsers");
            DropTable("ex05.Diretorio");
            DropTable("ex05.Arquivo");
        }
    }
}
