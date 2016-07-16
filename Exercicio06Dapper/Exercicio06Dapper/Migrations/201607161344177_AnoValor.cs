namespace Exercicio06Dapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnoValor : DbMigration
    {
        public override void Up()
        {
            AddColumn("ex06.Dados", "Ano", c => c.Int(nullable: false));
            AddColumn("ex06.Dados", "Valor", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("ex06.Dados", "Valor");
            DropColumn("ex06.Dados", "Ano");
        }
    }
}
