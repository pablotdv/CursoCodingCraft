namespace GaveteiroLanches.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovimentacaoDataFinalizacao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movimentacao", "DataFinalizacao", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movimentacao", "DataFinalizacao");
        }
    }
}
