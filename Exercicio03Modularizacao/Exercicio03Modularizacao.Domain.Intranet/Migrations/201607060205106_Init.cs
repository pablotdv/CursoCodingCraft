namespace Exercicio03Modularizacao.Domain.Intranet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ex03.Comentarios",
                c => new
                    {
                        ComentarioId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        UsuarioId = c.Guid(nullable: false),
                        CursoId = c.Int(nullable: false),
                        DataAprovacao = c.DateTime(),
                        UsuarioAprovacaoId = c.Guid(),
                    })
                .PrimaryKey(t => t.ComentarioId)
                .ForeignKey("ex03.Cursos", t => t.CursoId, cascadeDelete: true)
                .ForeignKey("ex03.AspNetUsers", t => t.UsuarioId, cascadeDelete: true)
                .ForeignKey("ex03.AspNetUsers", t => t.UsuarioAprovacaoId)
                .Index(t => t.UsuarioId)
                .Index(t => t.CursoId)
                .Index(t => t.UsuarioAprovacaoId);
            
            CreateTable(
                "ex03.Cursos",
                c => new
                    {
                        CursoId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        Sobre = c.String(),
                        ConteudoProgramatico = c.String(),
                        Curso_CursoId = c.Int(),
                    })
                .PrimaryKey(t => t.CursoId)
                .ForeignKey("ex03.Cursos", t => t.Curso_CursoId)
                .Index(t => t.Curso_CursoId);
            
            CreateTable(
                "ex03.CursoStatus",
                c => new
                    {
                        CursoStatusId = c.Int(nullable: false, identity: true),
                        CursoId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        UsuarioId = c.Guid(nullable: false),
                        Data = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CursoStatusId)
                .ForeignKey("ex03.Cursos", t => t.CursoId, cascadeDelete: true)
                .ForeignKey("ex03.AspNetUsers", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.CursoId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "ex03.AspNetUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
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
                "ex03.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ex03.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "ex03.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("ex03.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "ex03.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("ex03.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("ex03.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "ex03.AspNetRoles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("ex03.AspNetUserRoles", "RoleId", "ex03.AspNetRoles");
            DropForeignKey("ex03.Comentarios", "UsuarioAprovacaoId", "ex03.AspNetUsers");
            DropForeignKey("ex03.Comentarios", "UsuarioId", "ex03.AspNetUsers");
            DropForeignKey("ex03.Comentarios", "CursoId", "ex03.Cursos");
            DropForeignKey("ex03.CursoStatus", "UsuarioId", "ex03.AspNetUsers");
            DropForeignKey("ex03.AspNetUserRoles", "UserId", "ex03.AspNetUsers");
            DropForeignKey("ex03.AspNetUserLogins", "UserId", "ex03.AspNetUsers");
            DropForeignKey("ex03.AspNetUserClaims", "UserId", "ex03.AspNetUsers");
            DropForeignKey("ex03.CursoStatus", "CursoId", "ex03.Cursos");
            DropForeignKey("ex03.Cursos", "Curso_CursoId", "ex03.Cursos");
            DropIndex("ex03.AspNetRoles", "RoleNameIndex");
            DropIndex("ex03.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("ex03.AspNetUserRoles", new[] { "UserId" });
            DropIndex("ex03.AspNetUserLogins", new[] { "UserId" });
            DropIndex("ex03.AspNetUserClaims", new[] { "UserId" });
            DropIndex("ex03.AspNetUsers", "UserNameIndex");
            DropIndex("ex03.CursoStatus", new[] { "UsuarioId" });
            DropIndex("ex03.CursoStatus", new[] { "CursoId" });
            DropIndex("ex03.Cursos", new[] { "Curso_CursoId" });
            DropIndex("ex03.Comentarios", new[] { "UsuarioAprovacaoId" });
            DropIndex("ex03.Comentarios", new[] { "CursoId" });
            DropIndex("ex03.Comentarios", new[] { "UsuarioId" });
            DropTable("ex03.AspNetRoles");
            DropTable("ex03.AspNetUserRoles");
            DropTable("ex03.AspNetUserLogins");
            DropTable("ex03.AspNetUserClaims");
            DropTable("ex03.AspNetUsers");
            DropTable("ex03.CursoStatus");
            DropTable("ex03.Cursos");
            DropTable("ex03.Comentarios");
        }
    }
}
