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

namespace Exercicio05WebAPI.Controllers
{
    [Authorize]
    public class ArquivoController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Arquivo
        public IQueryable<Arquivo> GetArquivoes()
        {
            return db.Arquivos;
        }

        // GET: api/Arquivo/5
        [ResponseType(typeof(Arquivo))]
        public async Task<IHttpActionResult> GetArquivo(Guid id)
        {
            Arquivo arquivo = await db.Arquivos.FindAsync(id);
            if (arquivo == null)
            {
                return NotFound();
            }

            return Ok(arquivo);
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

        public async Task<IHttpActionResult> PostArquivo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var httpRequest = HttpContext.Current.Request;

            string root = HttpContext.Current.Server.MapPath("~/Uploads");

            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);

            //await Upload1(root);
            Upload2(root);

            var arquivo = new Arquivo()
            {
                ArquivoId = Guid.NewGuid(),
                DiretorioId = new Guid(httpRequest.Form["DiretorioId"]),
                Nome = httpRequest.Form["Nome"],
                MimeType = httpRequest.Form["MimeType"]
            };

            db.Arquivos.Add(arquivo);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ArquivoExists(arquivo.ArquivoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = arquivo.ArquivoId }, arquivo);
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