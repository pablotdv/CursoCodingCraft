using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exercicio05WebAPI.Models
{
    public class FileResponseMessage : Message
    {
        #region Public Properties

        public bool IsExists { get; set; }

        #endregion
    }
}