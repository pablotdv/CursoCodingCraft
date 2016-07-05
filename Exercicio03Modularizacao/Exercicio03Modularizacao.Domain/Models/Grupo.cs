using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace Exercicio03Modularizacao.Domain.Models
{
    public class Grupo : IdentityRole<Guid, UsuarioGrupo>
    {
    }
}