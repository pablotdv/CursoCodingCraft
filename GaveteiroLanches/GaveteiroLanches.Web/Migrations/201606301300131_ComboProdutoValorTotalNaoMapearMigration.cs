namespace GaveteiroLanches.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComboProdutoValorTotalNaoMapearMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ComboProduto", "ValorTotal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ComboProduto", "ValorTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
