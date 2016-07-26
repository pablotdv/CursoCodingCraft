using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace Exercicio10Cep.Models
{
    public class ApplicationRole : IdentityRole<Guid, ApplicationUserRole>
    {
        public ApplicationRole()
        {
            Id = Guid.NewGuid();
        }
    }
}