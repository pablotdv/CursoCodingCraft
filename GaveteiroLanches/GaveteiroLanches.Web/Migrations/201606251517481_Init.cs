namespace GaveteiroLanches.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auditoria",
                c => new
                    {
                        AuditoriaId = c.Int(nullable: false, identity: true),
                        Usuario = c.String(maxLength: 100, unicode: false),
                        DataHora = c.DateTime(nullable: false),
                        Tipo = c.String(maxLength: 100, unicode: false),
                        Entidade = c.String(maxLength: 100, unicode: false),
                        EntidadeId = c.String(maxLength: 100, unicode: false),
                        Propriedade = c.String(maxLength: 100, unicode: false),
                        ValorOriginal = c.String(maxLength: 100, unicode: false),
                        ValorNovo = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.AuditoriaId);
            
            CreateTable(
                "dbo.Movimentacao",
                c => new
                    {
                        MovimentacaoId = c.Int(nullable: false, identity: true),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataHora = c.DateTime(nullable: false),
                        DataHoraCad = c.DateTime(nullable: false),
                        UsuarioCad = c.String(maxLength: 100, unicode: false),
                        FornecedorId = c.Int(),
                        Usuario = c.String(maxLength: 100, unicode: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.MovimentacaoId)
                .ForeignKey("dbo.Fornecedor", t => t.FornecedorId)
                .Index(t => t.FornecedorId);
            
            CreateTable(
                "dbo.Fornecedor",
                c => new
                    {
                        FornecedorId = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 100, unicode: false),
                        Email = c.String(maxLength: 100, unicode: false),
                        DataHoraCad = c.DateTime(nullable: false),
                        UsuarioCad = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.FornecedorId);
            
            CreateTable(
                "dbo.MovimentacaoProduto",
                c => new
                    {
                        MovimentacaoProdutoId = c.Int(nullable: false, identity: true),
                        MovimentacaoId = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        ValorUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.MovimentacaoProdutoId)
                .ForeignKey("dbo.Movimentacao", t => t.MovimentacaoId)
                .ForeignKey("dbo.Produto", t => t.ProdutoId)
                .Index(t => t.MovimentacaoId)
                .Index(t => t.ProdutoId);
            
            CreateTable(
                "dbo.Produto",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 100, unicode: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Categoria = c.String(maxLength: 100, unicode: false),
                        Quantidade = c.Int(nullable: false),
                        DataHoraCad = c.DateTime(nullable: false),
                        UsuarioCad = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.ProdutoId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 100, unicode: false),
                        Name = c.String(nullable: false, maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 100, unicode: false),
                        RoleId = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 100, unicode: false),
                        Email = c.String(maxLength: 256, unicode: false),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(maxLength: 100, unicode: false),
                        SecurityStamp = c.String(maxLength: 100, unicode: false),
                        PhoneNumber = c.String(maxLength: 100, unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 100, unicode: false),
                        ClaimType = c.String(maxLength: 100, unicode: false),
                        ClaimValue = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 100, unicode: false),
                        ProviderKey = c.String(nullable: false, maxLength: 100, unicode: false),
                        UserId = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.MovimentacaoProduto", "ProdutoId", "dbo.Produto");
            DropForeignKey("dbo.MovimentacaoProduto", "MovimentacaoId", "dbo.Movimentacao");
            DropForeignKey("dbo.Movimentacao", "FornecedorId", "dbo.Fornecedor");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.MovimentacaoProduto", new[] { "ProdutoId" });
            DropIndex("dbo.MovimentacaoProduto", new[] { "MovimentacaoId" });
            DropIndex("dbo.Movimentacao", new[] { "FornecedorId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Produto");
            DropTable("dbo.MovimentacaoProduto");
            DropTable("dbo.Fornecedor");
            DropTable("dbo.Movimentacao");
            DropTable("dbo.Auditoria");
        }
    }
}
