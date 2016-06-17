namespace GaveteiroLanches.Dominio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PessoaMap : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auditoria",
                c => new
                    {
                        AuditoriaId = c.Int(nullable: false, identity: true),
                        Usuario = c.String(maxLength: 100, unicode: false),
                        DataHora = c.DateTime(nullable: false),
                        Tipo = c.String(maxLength: 100, unicode: false),
                        Entidade = c.String(maxLength: 100, unicode: false),
                        EntidadeId = c.String(maxLength: 100, unicode: false),
                        Propriedade = c.String(maxLength: 100, unicode: false),
                        ValorOriginal = c.String(maxLength: 100, unicode: false),
                        ValorNovo = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.AuditoriaId);
            
            CreateTable(
                "dbo.Pessoa",
                c => new
                    {
                        PessoaId = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 100, unicode: false),
                        Email = c.String(maxLength: 100, unicode: false),
                        DataHoraCad = c.DateTime(nullable: false),
                        UsuarioCad = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.PessoaId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pessoa");
            DropTable("dbo.Auditoria");
        }
    }
}
