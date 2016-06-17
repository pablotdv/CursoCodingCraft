using GaveteiroLanches.Entidades;
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

namespace GaveteiroLanches.Dominio
{
    public class GaveteiroLanchesContext : DbContext
    {
        public DbSet<Auditoria> Auditoria { get; set; }

        public DbSet<Pessoa> Pessoa { get; set; }

        public GaveteiroLanchesContext()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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

            //adiciona os mapeamentos
            modelBuilder.Configurations.Add(new Mapeamento.AuditoriaMap());
            modelBuilder.Configurations.Add(new Mapeamento.PessoaMap());

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

        private List<Auditoria> GetAuditRecordsForChangeEntity(DbEntityEntry entry)
        {
            var currentTime = DateTime.Now;

            List<Auditoria> result = new List<Auditoria>();

            TableAttribute tableAttr = entry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;

            string tableName = ObjectContext.GetObjectType(entry.Entity.GetType()).Name;
            
            if (entry.Entity.GetType().GetProperties().FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() > 0) != null)
            {
                var keys = entry.Entity.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() > 0);
                string recordId = "";

                foreach (var keyValue in keys)
                {
                    if (recordId != "")
                        recordId += "|";
                    if (entry.State == EntityState.Added)
                        recordId += keyValue.Name + "=" + entry.CurrentValues[keyValue.Name];
                    else recordId += keyValue.Name + "=" + entry.OriginalValues[keyValue.Name];
                }

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
                                Entidade = tableName,
                                EntidadeId = recordId,
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
                                    Entidade = tableName,
                                    EntidadeId = recordId,
                                    Propriedade = propertyName,
                                    ValorOriginal = entry.OriginalValues[propertyName] == null ? null : entry.OriginalValues[propertyName].ToString(),
                                    ValorNovo = entry.CurrentValues[propertyName] == null ? null : entry.CurrentValues[propertyName].ToString()
                                });
                            }
                        }
                    }
                }
            }
            return result;
        }

    }
}
