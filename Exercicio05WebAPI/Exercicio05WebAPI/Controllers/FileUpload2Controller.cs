using Exercicio05WebAPI.Filters;
using Exercicio05WebAPI.Models;
using Exercicio05WebAPI.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Exercicio05WebAPI.Controllers
{
    [Authorize]
    public class FileUpload2Controller : ApiController
    {
        [MimeMultipart]
        public async Task<FileUploadResult> Post()
        {
            var uploadPath = HttpContext.Current.Server.MapPath("~/Uploads");

            var streamProvider = new UploadMultipartFormProvider(uploadPath);

            // Read the MIME multipart asynchronously 
            await Request.Content.ReadAsMultipartAsync(streamProvider);

            string _localFileName = streamProvider
                .FileData.Select(multiPartData => multiPartData.LocalFileName).FirstOrDefault();

            string contentType = MimeMapping.GetMimeMapping(_localFileName);


            return new FileUploadResult
            {
                FileNames = streamProvider.FileData.Select(entry => entry.LocalFileName),
                Names = streamProvider.FileData.Select(entry => entry.Headers.ContentDisposition.FileName),
                ContentTypes = streamProvider.FileData.Select(entry => entry.Headers.ContentType.MediaType),
                Description = streamProvider.FormData["description"],
                CreatedTimestamp = DateTime.UtcNow,
                UpdatedTimestamp = DateTime.UtcNow,
                DownloadLink = "TODO, will implement when file is persisited"
            };

            
        }
    }
}
