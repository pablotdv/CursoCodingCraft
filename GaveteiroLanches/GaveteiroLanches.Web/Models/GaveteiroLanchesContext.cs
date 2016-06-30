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
            
            //define que todas as colunas string serão varchar
            modelBuilder.Properties<string>()
                   .Configure(p => p.HasColumnType("varchar"));

            //define que todas as colunas string terão 100 caracteres
            modelBuilder.Properties<string>()
                  .Configure(p => p.HasMaxLength(100));

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync()
        {
            try
            {
                Auditar();

                return base.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                string exceptionsMessage = ErrosValidacao(ex);

                throw new DbEntityValidationException(exceptionsMessage, ex.EntityValidationErrors);
            }

        }

        private string ErrosValidacao(DbEntityValidationException ex)
        {
            var errorMessages = ex.EntityValidationErrors
                                .SelectMany(x => x.ValidationErrors)
                                .Select(x => x.ErrorMessage);

            var fullErrorMessage = string.Join("; ", errorMessages);

            var exceptionsMessage = string.Concat(ex.Message, "Os erros de validações são: ", fullErrorMessage);

            return exceptionsMessage;
        }

        public override int SaveChanges()
        {
            try
            {
                Auditar();

                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                string exceptionsMessage = ErrosValidacao(ex);

                throw new DbEntityValidationException(exceptionsMessage, ex.EntityValidationErrors);
            }
        }

        private void Auditar()
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

                if (auditorias != null)
                    this.Auditoria.AddRange(auditorias);
            }
        }

        private string GetKeyValue(DbEntityEntry entry)
        {
            var key = entry.Entity
                .GetType()
                .GetProperties()
                .Where(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() > 0)
                .FirstOrDefault();

            if (key == null)
                return null;

            switch (entry.State)
            {
                case EntityState.Modified: return entry.CurrentValues[key.Name].ToString();
                case EntityState.Deleted: return entry.OriginalValues[key.Name].ToString();
                default: return "";
            }
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
                            Entidade = ObjectContext.GetObjectType(entry.Entity.GetType()).Name,
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
                                Entidade = ObjectContext.GetObjectType(entry.Entity.GetType()).Name,
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
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Combo> Combo { get; set; }
        public DbSet<ComboProduto> ComboProduto { get; set; }
    }
}
