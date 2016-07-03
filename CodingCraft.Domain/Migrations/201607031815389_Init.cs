namespace CodingCraft.Domain.Migrations
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
                        Usuario = c.String(),
                        DataHora = c.DateTime(nullable: false),
                        Tipo = c.String(),
                        Entidade = c.String(),
                        EntidadeId = c.String(),
                        Propriedade = c.String(),
                        ValorOriginal = c.String(),
                        ValorNovo = c.String(),
                    })
                .PrimaryKey(t => t.AuditoriaId);
            
            CreateTable(
                "dbo.Combo",
                c => new
                    {
                        ComboId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        ValorTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataHoraCad = c.DateTime(nullable: false),
                        UsuarioCad = c.String(),
                    })
                .PrimaryKey(t => t.ComboId);
            
            CreateTable(
                "dbo.ComboProduto",
                c => new
                    {
                        ComboProdutoId = c.Guid(nullable: false),
                        ComboId = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        ValorUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataHoraCad = c.DateTime(nullable: false),
                        UsuarioCad = c.String(),
                    })
                .PrimaryKey(t => t.ComboProdutoId)
                .ForeignKey("dbo.Combo", t => t.ComboId)
                .ForeignKey("dbo.Produto", t => t.ProdutoId)
                .Index(t => t.ComboId)
                .Index(t => t.ProdutoId);
            
            CreateTable(
                "dbo.Produto",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Categoria = c.String(),
                        Quantidade = c.Int(nullable: false),
                        DataHoraCad = c.DateTime(nullable: false),
                        UsuarioCad = c.String(),
                    })
                .PrimaryKey(t => t.ProdutoId);
            
            CreateTable(
                "dbo.MovimentacaoProduto",
                c => new
                    {
                        MovimentacaoProdutoId = c.Int(nullable: false, identity: true),
                        MovimentacaoId = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        ValorUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataHoraCad = c.DateTime(nullable: false),
                        UsuarioCad = c.String(),
                    })
                .PrimaryKey(t => t.MovimentacaoProdutoId)
                .ForeignKey("dbo.Movimentacao", t => t.MovimentacaoId)
                .ForeignKey("dbo.Produto", t => t.ProdutoId)
                .Index(t => t.MovimentacaoId)
                .Index(t => t.ProdutoId);
            
            CreateTable(
                "dbo.Movimentacao",
                c => new
                    {
                        MovimentacaoId = c.Int(nullable: false, identity: true),
                        ValorTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataHora = c.DateTime(nullable: false),
                        DataFinalizacao = c.DateTime(),
                        DataHoraCad = c.DateTime(nullable: false),
                        UsuarioCad = c.String(),
                        FornecedorId = c.Int(),
                        Usuario = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.MovimentacaoId)
                .ForeignKey("dbo.Fornecedor", t => t.FornecedorId)
                .Index(t => t.FornecedorId);
            
            CreateTable(
                "dbo.MovimentacaoCombo",
                c => new
                    {
                        MovimentacaoComboId = c.Int(nullable: false, identity: true),
                        MovimentacaoId = c.Int(nullable: false),
                        ComboId = c.Int(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        ValorUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataHoraCad = c.DateTime(nullable: false),
                        UsuarioCad = c.String(),
                    })
                .PrimaryKey(t => t.MovimentacaoComboId)
                .ForeignKey("dbo.Combo", t => t.ComboId)
                .ForeignKey("dbo.Movimentacao", t => t.MovimentacaoId)
                .Index(t => t.MovimentacaoId)
                .Index(t => t.ComboId);
            
            CreateTable(
                "dbo.Fornecedor",
                c => new
                    {
                        FornecedorId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Email = c.String(),
                        Telefone = c.String(),
                        DataHoraCad = c.DateTime(nullable: false),
                        UsuarioCad = c.String(),
                    })
                .PrimaryKey(t => t.FornecedorId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
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
                        Id = c.Long(nullable: false, identity: true),
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
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Long(nullable: false),
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
            DropForeignKey("dbo.ComboProduto", "ProdutoId", "dbo.Produto");
            DropForeignKey("dbo.MovimentacaoProduto", "ProdutoId", "dbo.Produto");
            DropForeignKey("dbo.Movimentacao", "FornecedorId", "dbo.Fornecedor");
            DropForeignKey("dbo.MovimentacaoProduto", "MovimentacaoId", "dbo.Movimentacao");
            DropForeignKey("dbo.MovimentacaoCombo", "MovimentacaoId", "dbo.Movimentacao");
            DropForeignKey("dbo.MovimentacaoCombo", "ComboId", "dbo.Combo");
            DropForeignKey("dbo.ComboProduto", "ComboId", "dbo.Combo");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.MovimentacaoCombo", new[] { "ComboId" });
            DropIndex("dbo.MovimentacaoCombo", new[] { "MovimentacaoId" });
            DropIndex("dbo.Movimentacao", new[] { "FornecedorId" });
            DropIndex("dbo.MovimentacaoProduto", new[] { "ProdutoId" });
            DropIndex("dbo.MovimentacaoProduto", new[] { "MovimentacaoId" });
            DropIndex("dbo.ComboProduto", new[] { "ProdutoId" });
            DropIndex("dbo.ComboProduto", new[] { "ComboId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Fornecedor");
            DropTable("dbo.MovimentacaoCombo");
            DropTable("dbo.Movimentacao");
            DropTable("dbo.MovimentacaoProduto");
            DropTable("dbo.Produto");
            DropTable("dbo.ComboProduto");
            DropTable("dbo.Combo");
            DropTable("dbo.Auditoria");
        }
    }
}
