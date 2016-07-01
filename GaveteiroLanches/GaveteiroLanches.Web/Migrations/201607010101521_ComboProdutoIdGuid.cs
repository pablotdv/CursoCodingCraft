namespace GaveteiroLanches.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComboProdutoIdGuid : DbMigration
    {
        public override void Up()
        {
            Sql(@"delete dbo.MovimentacaoCombo");
            Sql(@"delete dbo.ComboProduto");
            Sql(@"delete dbo.Combo");
            DropPrimaryKey("dbo.ComboProduto");

            DropColumn("dbo.ComboProduto", "ComboProdutoId");
            AddColumn("dbo.ComboProduto", "ComboProdutoId", c => c.Guid(nullable: false));
                        
            AddPrimaryKey("dbo.ComboProduto", "ComboProdutoId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ComboProduto");
            DropColumn("dbo.ComboProduto", "ComboProdutoId");
            AddColumn("dbo.ComboProduto", "ComboProdutoId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ComboProduto", "ComboProdutoId");
        }
    }
}
