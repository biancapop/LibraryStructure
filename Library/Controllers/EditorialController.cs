using Library.DTOModels.DTOMappers;
using Library.Entity;
using Library.Models;
using Library.RequestManagers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Library.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EditorialController : ControllerBase
    {
        private readonly RequestPile requestsPile;
        private readonly IHttpClientFactory httpFactory;
        private CustomScope CustomScope;


        public EditorialController(RequestPile reqPile, IServiceScopeFactory serviceScopeFactory, IHttpClientFactory httpClientFactory)
        {
            this.requestsPile = reqPile;
            httpFactory = httpClientFactory;
            CustomScope = new CustomScope(httpFactory, serviceScopeFactory);
        }

        //Creates a new editorial        
        [HttpPost("{requestId}/{timestamp}")]
        public ActionResult<string> Post(string requestId, long timestamp, [FromBody]DTOEditorial editorialDTO)
        {
            CustomRequest request = new CustomRequest(requestId, CustomRequest.FLAG_CREATE, editorialDTO, editorialDTO.Id);
            EditorialRequestActions editorialRequest = new EditorialRequestActions(request, CustomScope);
            RequestManager requestManager = new RequestManager(timestamp, editorialRequest);
            requestsPile.AddRequest(editorialDTO.Id, requestManager);
            return Program.REQ_RESPONSE_POSITIVE;
        }

        //Updates the entity
        [HttpPut("{requestId}/{timestamp}")]
        public ActionResult<string> Put(string requestId, long timestamp, [FromBody]DTOEditorial editorialDTO)
        {
            CustomRequest request = new CustomRequest(requestId, Entity.CustomRequest.FLAG_UPDATE, editorialDTO, editorialDTO.Id);
            EditorialRequestActions editorialRequest = new EditorialRequestActions(request, CustomScope);
            RequestManager requestManager = new RequestManager(timestamp, editorialRequest);
            requestsPile.AddRequest(editorialDTO.Id, requestManager);
            return Program.REQ_RESPONSE_POSITIVE;
        }


        //Deletes the entity
        [HttpDelete("{timestamp}/{idEditorial}")]
        public ActionResult<string> Delete(long timestamp, string idEditorial, [FromBody]DTOModelBase dto)
        {
            CustomRequest request = new CustomRequest(null, CustomRequest.FLAG_DELETE, dto, idEditorial);
            EditorialRequestActions editorialRequest = new EditorialRequestActions(request, CustomScope);
            RequestManager requestManager = new RequestManager(timestamp, editorialRequest);
            requestsPile.AddRequest(idEditorial, requestManager);
            return Program.REQ_RESPONSE_POSITIVE;
        }


 
    }
}