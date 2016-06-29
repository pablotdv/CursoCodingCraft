namespace GaveteiroLanches.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovimentacaoValorTotal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movimentacao", "ValorTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movimentacao", "ValorTotal");
        }
    }
}
