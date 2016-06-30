namespace GaveteiroLanches.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComboMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Combo",
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
                "dbo.ComboProduto",
                c => new
                    {
                        ComboProdutoId = c.Int(nullable: false, identity: true),
                        ComboId = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        ValorUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataHoraCad = c.DateTime(nullable: false),
                        UsuarioCad = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.ComboProdutoId)
                .ForeignKey("dbo.Combo", t => t.ComboId)
                .ForeignKey("dbo.Produto", t => t.ProdutoId)
                .Index(t => t.ComboId)
                .Index(t => t.ProdutoId);
            
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
                        UsuarioCad = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.MovimentacaoComboId)
                .ForeignKey("dbo.Combo", t => t.ComboId)
                .ForeignKey("dbo.Movimentacao", t => t.MovimentacaoId)
                .Index(t => t.MovimentacaoId)
                .Index(t => t.ComboId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ComboProduto", "ProdutoId", "dbo.Produto");
            DropForeignKey("dbo.MovimentacaoCombo", "MovimentacaoId", "dbo.Movimentacao");
            DropForeignKey("dbo.MovimentacaoCombo", "ComboId", "dbo.Combo");
            DropForeignKey("dbo.ComboProduto", "ComboId", "dbo.Combo");
            DropIndex("dbo.MovimentacaoCombo", new[] { "ComboId" });
            DropIndex("dbo.MovimentacaoCombo", new[] { "MovimentacaoId" });
            DropIndex("dbo.ComboProduto", new[] { "ProdutoId" });
            DropIndex("dbo.ComboProduto", new[] { "ComboId" });
            DropTable("dbo.MovimentacaoCombo");
            DropTable("dbo.ComboProduto");
            DropTable("dbo.Combo");
        }
    }
}
