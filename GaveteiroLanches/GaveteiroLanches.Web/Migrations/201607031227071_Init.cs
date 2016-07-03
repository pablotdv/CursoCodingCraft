namespace Exercicio01EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ex01.Auditoria",
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
                "ex01.Combo",
                c => new
                    {
                        ComboId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 100, unicode: false),
                        ValorTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataHoraCad = c.DateTime(nullable: false),
                        UsuarioCad = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.ComboId);
            
            CreateTable(
                "ex01.ComboProduto",
                c => new
                    {
                        ComboProdutoId = c.Guid(nullable: false),
                        ComboId = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        ValorUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataHoraCad = c.DateTime(nullable: false),
                        UsuarioCad = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.ComboProdutoId)
                .ForeignKey("ex01.Combo", t => t.ComboId)
                .ForeignKey("ex01.Produto", t => t.ProdutoId)
                .Index(t => t.ComboId)
                .Index(t => t.ProdutoId);
            
            CreateTable(
                "ex01.Produto",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 100, unicode: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Categoria = c.String(maxLength: 100, unicode: false),
                        Quantidade = c.Int(nullable: false),
                        DataHoraCad = c.DateTime(nullable: false),
                        UsuarioCad = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.ProdutoId);
            
            CreateTable(
                "ex01.MovimentacaoProduto",
                c => new
                    {
                        MovimentacaoProdutoId = c.Int(nullable: false, identity: true),
                        MovimentacaoId = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        ValorUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataHoraCad = c.DateTime(nullable: false),
                        UsuarioCad = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.MovimentacaoProdutoId)
                .ForeignKey("ex01.Movimentacao", t => t.MovimentacaoId)
                .ForeignKey("ex01.Produto", t => t.ProdutoId)
                .Index(t => t.MovimentacaoId)
                .Index(t => t.ProdutoId);
            
            CreateTable(
                "ex01.Movimentacao",
                c => new
                    {
                        MovimentacaoId = c.Int(nullable: false, identity: true),
                        ValorTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataHora = c.DateTime(nullable: false),
                        DataFinalizacao = c.DateTime(),
                        DataHoraCad = c.DateTime(nullable: false),
                        UsuarioCad = c.String(maxLength: 100, unicode: false),
                        FornecedorId = c.Int(),
                        Usuario = c.String(maxLength: 100, unicode: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.MovimentacaoId)
                .ForeignKey("ex01.Fornecedor", t => t.FornecedorId)
                .Index(t => t.FornecedorId);
            
            CreateTable(
                "ex01.MovimentacaoCombo",
                c => new
                    {
                        MovimentacaoComboId = c.Int(nullable: false, identity: true),
                        MovimentacaoId = c.Int(nullable: false),
                        ComboId = c.Int(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        ValorUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataHoraCad = c.DateTime(nullable: false),
                        UsuarioCad = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.MovimentacaoComboId)
                .ForeignKey("ex01.Combo", t => t.ComboId)
                .ForeignKey("ex01.Movimentacao", t => t.MovimentacaoId)
                .Index(t => t.MovimentacaoId)
                .Index(t => t.ComboId);
            
            CreateTable(
                "ex01.Fornecedor",
                c => new
                    {
                        FornecedorId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        Email = c.String(maxLength: 100, unicode: false),
                        Telefone = c.String(maxLength: 100, unicode: false),
                        DataHoraCad = c.DateTime(nullable: false),
                        UsuarioCad = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.FornecedorId);
            
            CreateTable(
                "ex01.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 100, unicode: false),
                        Name = c.String(nullable: false, maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "ex01.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 100, unicode: false),
                        RoleId = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("ex01.AspNetRoles", t => t.RoleId)
                .ForeignKey("ex01.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "ex01.AspNetUsers",
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
                "ex01.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 100, unicode: false),
                        ClaimType = c.String(maxLength: 100, unicode: false),
                        ClaimValue = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ex01.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "ex01.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 100, unicode: false),
                        ProviderKey = c.String(nullable: false, maxLength: 100, unicode: false),
                        UserId = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("ex01.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("ex01.AspNetUserRoles", "UserId", "ex01.AspNetUsers");
            DropForeignKey("ex01.AspNetUserLogins", "UserId", "ex01.AspNetUsers");
            DropForeignKey("ex01.AspNetUserClaims", "UserId", "ex01.AspNetUsers");
            DropForeignKey("ex01.AspNetUserRoles", "RoleId", "ex01.AspNetRoles");
            DropForeignKey("ex01.ComboProduto", "ProdutoId", "ex01.Produto");
            DropForeignKey("ex01.MovimentacaoProduto", "ProdutoId", "ex01.Produto");
            DropForeignKey("ex01.Movimentacao", "FornecedorId", "ex01.Fornecedor");
            DropForeignKey("ex01.MovimentacaoProduto", "MovimentacaoId", "ex01.Movimentacao");
            DropForeignKey("ex01.MovimentacaoCombo", "MovimentacaoId", "ex01.Movimentacao");
            DropForeignKey("ex01.MovimentacaoCombo", "ComboId", "ex01.Combo");
            DropForeignKey("ex01.ComboProduto", "ComboId", "ex01.Combo");
            DropIndex("ex01.AspNetUserLogins", new[] { "UserId" });
            DropIndex("ex01.AspNetUserClaims", new[] { "UserId" });
            DropIndex("ex01.AspNetUsers", "UserNameIndex");
            DropIndex("ex01.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("ex01.AspNetUserRoles", new[] { "UserId" });
            DropIndex("ex01.AspNetRoles", "RoleNameIndex");
            DropIndex("ex01.MovimentacaoCombo", new[] { "ComboId" });
            DropIndex("ex01.MovimentacaoCombo", new[] { "MovimentacaoId" });
            DropIndex("ex01.Movimentacao", new[] { "FornecedorId" });
            DropIndex("ex01.MovimentacaoProduto", new[] { "ProdutoId" });
            DropIndex("ex01.MovimentacaoProduto", new[] { "MovimentacaoId" });
            DropIndex("ex01.ComboProduto", new[] { "ProdutoId" });
            DropIndex("ex01.ComboProduto", new[] { "ComboId" });
            DropTable("ex01.AspNetUserLogins");
            DropTable("ex01.AspNetUserClaims");
            DropTable("ex01.AspNetUsers");
            DropTable("ex01.AspNetUserRoles");
            DropTable("ex01.AspNetRoles");
            DropTable("ex01.Fornecedor");
            DropTable("ex01.MovimentacaoCombo");
            DropTable("ex01.Movimentacao");
            DropTable("ex01.MovimentacaoProduto");
            DropTable("ex01.Produto");
            DropTable("ex01.ComboProduto");
            DropTable("ex01.Combo");
            DropTable("ex01.Auditoria");
        }
    }
}
