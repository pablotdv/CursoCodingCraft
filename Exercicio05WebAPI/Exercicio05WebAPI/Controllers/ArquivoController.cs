using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Exercicio05WebAPI.Models;
using System.Web;
using System.IO;
using System.Diagnostics;
using Exercicio05WebAPI.Providers;
using System.Net.Http.Headers;
using Exercicio05WebAPI.Helpers;

namespace Exercicio05WebAPI.Controllers
{
    [Authorize]
    public class ArquivoController : ApiController
    {
        private string exMessage = "Opps! exception happens";

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Arquivo
        public IQueryable<Arquivo> GetArquivoes()
        {
            return db.Arquivos;
        }

        [AllowAnonymous]
        // GET: api/Arquivo/5
        public async Task<HttpResponseMessage> GetArquivo(Guid id)
        {
            return await Get2(id);

            //return await Get1(id);
        }

        private async Task<HttpResponseMessage> Get2(Guid id)
        {
            Arquivo arquivo = await db.Arquivos.FindAsync(id);
            if (arquivo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            string root = HttpContext.Current.Server.MapPath("~/Uploads");

            var fileName = Directory.GetFiles(root, id.ToString() + "*.*").FirstOrDefault();

            FileInfo fileInfo = new FileInfo(fileName);

            if (!fileInfo.Exists)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Stream fileStream = fileInfo.ReadStream();
            HttpResponseMessage response = new HttpResponseMessage { Content = new StreamContent(fileStream) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(arquivo.MimeType);
            response.Content.Headers.ContentLength = fileInfo.Length;
            return response;
        }

        private async Task<HttpResponseMessage> Get1(Guid id)
        {
            Arquivo arquivo = await db.Arquivos.FindAsync(id);
            if (arquivo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            HttpResponseMessage response = Request.CreateResponse();
            FileMetaData metaData = new FileMetaData();
            metaData.FileResponseMessage.IsExists = false;

            try
            {
                string root = HttpContext.Current.Server.MapPath("~/Uploads");

                var fileName = Directory.GetFiles(root, id.ToString() + "*.*").FirstOrDefault();

                FileInfo fileInfo = new FileInfo(fileName);

                if (!fileInfo.Exists)
                {
                    metaData.FileResponseMessage.IsExists = false;
                    metaData.FileResponseMessage.Content = string.Format("{0} file is not found !", fileName);
                    response = Request.CreateResponse(HttpStatusCode.NotFound, metaData, new MediaTypeHeaderValue("text/json"));
                }
                else
                {
                    response.Headers.AcceptRanges.Add("bytes");
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StreamContent(fileInfo.ReadStream());
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = fileName;
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue(arquivo.MimeType);
                    response.Content.Headers.ContentLength = fileInfo.Length;
                }
            }
            catch (Exception exception)
            {
                // Log exception and return gracefully

                metaData = new FileMetaData();
                metaData.FileResponseMessage.Content = ProcessException(exception);
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, metaData, new MediaTypeHeaderValue("text/json"));
            }

            return response;
        }

        private string ProcessException(Exception exception)
        {
            if (exception == null)
            {
                return exMessage;
            }
            if (!string.IsNullOrWhiteSpace(exception.Message) && !string.IsNullOrWhiteSpace(exception.StackTrace))
            {
                return string.Concat(exMessage, " Exception : - Message : ", exception.Message, " ", "StackTrace : ", exception.StackTrace);
            }
            else if (!string.IsNullOrWhiteSpace(exception.Message) && string.IsNullOrWhiteSpace(exception.StackTrace))
            {
                return string.Concat(exMessage, " Exception : - Message : ", exception.Message);
            }
            else if (string.IsNullOrWhiteSpace(exception.Message) && !string.IsNullOrWhiteSpace(exception.StackTrace))
            {
                return string.Concat(exMessage, " Exception : - StackTrace : ", exception.StackTrace);
            }
            else
            {
                return exMessage;
            }
        }

        // PUT: api/Arquivo/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutArquivo(Guid id, Arquivo arquivo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != arquivo.ArquivoId)
            {
                return BadRequest();
            }

            db.Entry(arquivo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArquivoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Arquivo

        public async Task<HttpResponseMessage> PostArquivo()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var httpRequest = HttpContext.Current.Request;

            string root = HttpContext.Current.Server.MapPath("~/Uploads");

            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);

            //await Upload1(root);
            var file = await Upload3(root);

            if (file == null)
                return Request.CreateResponse(HttpStatusCode.NoContent);

            List<Arquivo> arquivos = new List<Arquivo>();

            foreach (var f in file.FileData)
            {
                FileInfo fileInfo = new FileInfo(f.LocalFileName);
                Guid arquivoId = Guid.Parse(fileInfo.Name.Replace(fileInfo.Extension, ""));
                var arquivo = new Arquivo()
                {
                    ArquivoId = arquivoId,
                    DiretorioId = new Guid(httpRequest.Form["DiretorioId"]),
                    Nome = httpRequest.Form["Nome"],
                    MimeType = f.Headers.ContentType.MediaType
                };

                arquivos.Add(arquivo);
            }

            db.Arquivos.AddRange(arquivos);
       
            await db.SaveChangesAsync();
           
            return Request.CreateResponse(HttpStatusCode.OK, arquivos);
        }

        public async Task<FileUploadResult> Upload3(string uploadPath)
        {
            var streamProvider = new UploadMultipartFormProvider(uploadPath);

            // Read the MIME multipart asynchronously 
            await Request.Content.ReadAsMultipartAsync(streamProvider);

            string _localFileName = streamProvider
                .FileData.Select(multiPartData => multiPartData.LocalFileName).FirstOrDefault();

            string contentType = MimeMapping.GetMimeMapping(_localFileName);
            
            return new FileUploadResult
            {
                FileData = streamProvider.FileData,
                FileNames = streamProvider.FileData.Select(entry => entry.LocalFileName),
                Names = streamProvider.FileData.Select(entry => entry.Headers.ContentDisposition.FileName),
                ContentTypes = streamProvider.FileData.Select(entry => entry.Headers.ContentType.MediaType),
                Description = streamProvider.FormData["description"],
                CreatedTimestamp = DateTime.UtcNow,
                UpdatedTimestamp = DateTime.UtcNow,
                DownloadLink = "TODO, will implement when file is persisited"
            };


        }

        public async void Upload2(string root)
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    Trace.WriteLine("Server file path: " + file.LocalFileName);
                }

            }
            catch (System.Exception e)
            {

            }
        }

        private async Task Upload1(string root)
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.UnsupportedMediaType));
            }

            // Create a stream provider for setting up output streams
            MultipartFormDataStreamProvider streamProvider = new MultipartFormDataStreamProvider(root);

            // Read the MIME multipart asynchronously content using the stream provider we just created.
            await Request.Content.ReadAsMultipartAsync(streamProvider);

            // Create response
            var fileResult = new FileResult
            {
                FileNames = streamProvider.FileData.Select(entry => entry.LocalFileName),
                Submitter = streamProvider.FormData["submitter"]
            };
        }

        // DELETE: api/Arquivo/5
        [ResponseType(typeof(Arquivo))]
        public async Task<IHttpActionResult> DeleteArquivo(Guid id)
        {
            Arquivo arquivo = await db.Arquivos.FindAsync(id);
            if (arquivo == null)
            {
                return NotFound();
            }

            db.Arquivos.Remove(arquivo);
            await db.SaveChangesAsync();

            return Ok(arquivo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArquivoExists(Guid id)
        {
            return db.Arquivos.Count(e => e.ArquivoId == id) > 0;
        }
    }
}