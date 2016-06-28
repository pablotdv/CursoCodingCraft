namespace GaveteiroLanches.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProdutoCamposObrigatorios : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Produto", "Descricao", c => c.String(nullable: false, maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Produto", "Descricao", c => c.String(maxLength: 100, unicode: false));
        }
    }
}
