using Library.DTOModels.DTOMappers;
using Library.Entity;
using Library.Models;
using Library.RequestManagers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NLog.Fluent;
using System.Net.Http;

namespace Library.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly RequestPile requestPile;
        private readonly IHttpClientFactory httpFactory;
        private CustomScope CustomScope;

        public BookController(RequestPile reqPile, IServiceScopeFactory serviceScopeFactory, IHttpClientFactory httpClientFactory)
        {
            requestPile = reqPile;
            httpFactory = httpClientFactory;
            CustomScope = new CustomScope(httpFactory, serviceScopeFactory);
        }


        //Create the book
        [HttpPost("{idRequest}/{timestamp}")]
        public ActionResult<string> Post(string idRequest, long timestamp, [FromBody]DTOBook bookDTO)
        {
            CustomRequest request = new CustomRequest(idRequest, Entity.CustomRequest.FLAG_CREATE, bookDTO, bookDTO.IdEditorial);
            BookRequestActions bookRequest = new BookRequestActions(request, CustomScope);
            RequestManager requestManager = new RequestManager(timestamp, bookRequest);
            requestPile.AddRequest(bookDTO.IdEditorial, requestManager);
            return Program.REQ_RESPONSE_POSITIVE;
        }

        //Updates the book
        [HttpPut("{idRequest}/{timestamp}")]
        public ActionResult<string> Put(string idRequest, long timestamp, [FromBody]DTOBook bookDTO)
        {
            CustomRequest request = new CustomRequest(idRequest, Entity.CustomRequest.FLAG_UPDATE, bookDTO, bookDTO.IdEditorial);
            BookRequestActions bookRequest = new BookRequestActions(request, CustomScope);
            RequestManager requestManager = new RequestManager(timestamp, bookRequest);
            requestPile.AddRequest(bookDTO.IdEditorial, requestManager);
            return Program.REQ_RESPONSE_POSITIVE;
        }

        //Deletes the book
        [HttpDelete("{idRequest}/{timestamp}/{idBook}")]
        public ActionResult<string> Delete(string idRequest, long timestamp, string idBook, [FromBody]DTOModelBase dto)
        {
            Log.Error("BookController delete idPeitcion=" + idRequest + " idBook=" + idBook);

            CustomRequest request = new CustomRequest(idRequest, Entity.CustomRequest.FLAG_DELETE, idBook, dto, dto.IdEditorial);
            BookRequestActions bookRequest = new BookRequestActions(request, CustomScope);
            RequestManager requestManager = new RequestManager(timestamp, bookRequest);
            requestPile.AddRequest(dto.IdEditorial, requestManager);
            return Program.REQ_RESPONSE_POSITIVE;
        }
    }
}