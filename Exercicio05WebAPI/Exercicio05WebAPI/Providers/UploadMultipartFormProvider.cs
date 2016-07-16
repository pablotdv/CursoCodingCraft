using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Exercicio05WebAPI.Providers
{
    public class UploadMultipartFormProvider : MultipartFormDataStreamProvider
    {
        public UploadMultipartFormProvider(string rootPath) : base(rootPath) { }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            if (headers != null &&
                headers.ContentDisposition != null)
            {
                string fileName = headers
                    .ContentDisposition
                    .FileName.TrimEnd('"').TrimStart('"');

                return Guid.NewGuid().ToString() + new FileInfo(fileName).Extension;
            }

            return base.GetLocalFileName(headers);
        }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            return base.GetStream(parent, headers);
        }
    }
}