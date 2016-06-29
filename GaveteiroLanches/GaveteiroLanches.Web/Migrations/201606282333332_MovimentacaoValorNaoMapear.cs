namespace GaveteiroLanches.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovimentacaoValorNaoMapear : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovimentacaoProduto", "DataHoraCad", c => c.DateTime(nullable: false));
            AddColumn("dbo.MovimentacaoProduto", "UsuarioCad", c => c.String(maxLength: 100, unicode: false));
            DropColumn("dbo.Movimentacao", "Valor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movimentacao", "Valor", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.MovimentacaoProduto", "UsuarioCad");
            DropColumn("dbo.MovimentacaoProduto", "DataHoraCad");
        }
    }
}
