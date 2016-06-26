namespace GaveteiroLanches.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FornecedorTelefone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fornecedor", "Telefone", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Fornecedor", "Nome", c => c.String(nullable: false, maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Fornecedor", "Nome", c => c.String(maxLength: 100, unicode: false));
            DropColumn("dbo.Fornecedor", "Telefone");
        }
    }
}
