using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Exercicio05WebAPI.Helpers
{
    public static class FileUtilityExtension
    {
        public static Stream ReadStream(this FileInfo fileInfo)
        {
            int bufferSize = 1048575; // 1MB
            return new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize); ;
        }
    }
}