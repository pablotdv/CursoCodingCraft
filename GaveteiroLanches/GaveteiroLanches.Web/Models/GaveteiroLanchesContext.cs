using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GaveteiroLanches.Web.Models
{
    public class GaveteiroLanchesContext : IdentityDbContext<ApplicationUser>
    {


        public GaveteiroLanchesContext()
            : base("GaveteiroLanchesContext", throwIfV1Schema: false)
        {
        }

        public static GaveteiroLanchesContext Create()
        {
            return new GaveteiroLanchesContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<GaveteiroLanchesContext>(new CreateDatabaseIfNotExists<GaveteiroLanchesContext>());

            //remove a pluralização das tabelas
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //remove a deleção em cascata
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //remove a deleção em cascata
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //define como PK a coluna que tem o mesmo nome da tabela mais Id
            modelBuilder.Properties()
                   .Where(p => p.Name == p.ReflectedType.Name + "Id")
                   .Configure(p => p.IsKey());

            //define que todas as colunas string serão varchar
            modelBuilder.Properties<string>()
                   .Configure(p => p.HasColumnType("varchar"));

            //define que todas as colunas string terão 100 caracteres
            modelBuilder.Properties<string>()
                  .Configure(p => p.HasMaxLength(100));

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            try
            {
                var states = new List<EntityState>() { EntityState.Added, EntityState.Deleted, EntityState.Modified };

                var entidades = ChangeTracker.Entries().Where(e => e.Entity != null && states.Contains(e.State) && typeof(IEntidade).IsAssignableFrom(e.Entity.GetType()));

                foreach (var entry in entidades)
                {
                    var currentTime = DateTime.Now;

                    if (entry.Property("DataHoraCad") != null)
                    {
                        entry.Property("DataHoraCad").CurrentValue = currentTime;
                    }
                    if (entry.Property("UsuarioCad") != null)
                    {
                        entry.Property("UsuarioCad").CurrentValue = HttpContext.Current != null ? HttpContext.Current.User.Identity.Name : "Usuario";
                    }

                    var auditorias = GetAuditRecordsForChangeEntity(entry);

                    this.Auditoria.AddRange(auditorias);
                }

                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);

                var exceptionsMessage = string.Concat(ex.Message, "Os erros de validações são: ", fullErrorMessage);

                throw new DbEntityValidationException(exceptionsMessage, ex.EntityValidationErrors);
            }
        }

        private string GetKeyValue(DbEntityEntry entry)
        {
            string recordId = "";

            var key = entry.Entity.GetType().GetProperties().Where(p => p.Name == entry.Entity.GetType().Name + "Id").FirstOrDefault();

            if (key != null)
            {
                if (entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
                    recordId += key.Name + "=" + entry.CurrentValues[key.Name];
            }

            return recordId;
        }

        private List<Auditoria> GetAuditRecordsForChangeEntity(DbEntityEntry entry)
        {
            var keyValue = GetKeyValue(entry);

            if (keyValue == null)
                return null;

            var currentTime = DateTime.Now;

            List<Auditoria> result = new List<Auditoria>();

            if (entry.State == EntityState.Deleted)
            {
                foreach (var propertyName in entry.OriginalValues.PropertyNames)
                {
                    string originalValue = "";
                    string columnName = "";
                    try
                    {
                        originalValue = entry.OriginalValues[propertyName] != null ? entry.OriginalValues[propertyName].ToString() : "";
                        columnName = propertyName;
                    }
                    catch
                    { }

                    if (!String.IsNullOrWhiteSpace(originalValue) && !columnName.Equals("DataHoraCad"))
                    {
                        result.Add(new Auditoria()
                        {
                            Usuario = entry.OriginalValues["UsuarioCad"].ToString(),
                            DataHora = currentTime,
                            Tipo = "Deleted",    // Deleted
                            Entidade = entry.GetType().Name,
                            EntidadeId = keyValue,
                            Propriedade = propertyName,
                            ValorOriginal = originalValue,
                            ValorNovo = ""
                        });
                    }
                }
            }
            else if (entry.State == EntityState.Modified)
            {
                DbPropertyValues dbPropertyValues = entry.GetDatabaseValues();

                foreach (string propertyName in entry.OriginalValues.PropertyNames)
                {
                    if (!propertyName.Equals("TimeStamp") && dbPropertyValues[propertyName] != null)
                        entry.OriginalValues[propertyName] = dbPropertyValues[propertyName];

                    var originalValues = entry.OriginalValues[propertyName];

                    var currentValues = entry.CurrentValues[propertyName];

                    if (!object.Equals(originalValues, currentValues))
                    {
                        if (!propertyName.Equals("DataHoraCad"))
                        {
                            result.Add(new Auditoria()
                            {
                                Usuario = entry.CurrentValues["UsuarioCad"].ToString(),
                                DataHora = currentTime,
                                Tipo = "Modified",    // Modified
                                Entidade = entry.GetType().Name,
                                EntidadeId = keyValue,
                                Propriedade = propertyName,
                                ValorOriginal = entry.OriginalValues[propertyName] == null ? null : entry.OriginalValues[propertyName].ToString(),
                                ValorNovo = entry.CurrentValues[propertyName] == null ? null : entry.CurrentValues[propertyName].ToString()
                            });
                        }
                    }
                }
            }

            return result;
        }


        public DbSet<Auditoria> Auditoria { get; set; }
        public DbSet<MovimentacaoEntrada> MovimentacaoEntrada { get; set; }
        public DbSet<MovimentacaoSaida> MovimentacaoSaida { get; set; }
        public DbSet<MovimentacaoProduto> MovimentacaoProduto { get; set; }
        public DbSet<Fornecedor> Pessoa { get; set; }
        public DbSet<Produto> Produto { get; set; }
    }
}
