namespace Exercicio06Dapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ex06.Dados",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CodigoPais = c.String(maxLength: 128),
                        CodigoIndicador = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ex06.Indicadores", t => t.CodigoIndicador)
                .ForeignKey("ex06.Pais", t => t.CodigoPais)
                .Index(t => t.CodigoPais)
                .Index(t => t.CodigoIndicador);
            
            CreateTable(
                "ex06.Indicadores",
                c => new
                    {
                        Codigo = c.String(nullable: false, maxLength: 128),
                        Nome = c.String(),
                        Nota = c.String(),
                        Organizacao = c.String(),
                    })
                .PrimaryKey(t => t.Codigo);
            
            CreateTable(
                "ex06.Pais",
                c => new
                    {
                        Codigo = c.String(nullable: false, maxLength: 128),
                        Regiao = c.String(),
                        GrupoEconomico = c.String(),
                        Notas = c.String(),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Codigo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("ex06.Dados", "CodigoPais", "ex06.Pais");
            DropForeignKey("ex06.Dados", "CodigoIndicador", "ex06.Indicadores");
            DropIndex("ex06.Dados", new[] { "CodigoIndicador" });
            DropIndex("ex06.Dados", new[] { "CodigoPais" });
            DropTable("ex06.Pais");
            DropTable("ex06.Indicadores");
            DropTable("ex06.Dados");
        }
    }
}
