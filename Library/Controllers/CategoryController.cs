using Library.DTOModels.DTOMappers;
using Library.Entity;
using Library.Models;
using Library.RequestActions;
using Library.RequestManagers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Fluent;
using System.Net.Http;

namespace Library.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly RequestPile requestPile;
        private readonly IHttpClientFactory httpFactory;
        private CustomScope CustomScope;

        public CategoryController(RequestPile pilaDeLlamadas, IServiceScopeFactory serviceScopeFactory, IHttpClientFactory httpClientFactory)
        {
            this.requestPile = pilaDeLlamadas;
            httpFactory = httpClientFactory;
            CustomScope = new CustomScope(httpFactory, serviceScopeFactory);

        }


        //Creates a new category
        [HttpPost("{idRequest}/{timestamp}")]
        public ActionResult<string> Post(string idRequest, long timestamp, [FromBody]DTOCategory categoryDTO)
        {
            CustomRequest req = new CustomRequest(idRequest, CustomRequest.FLAG_CREATE, categoryDTO, categoryDTO.IdEditorial);
            CategoryRequestActions categoryRequest = new CategoryRequestActions(req, CustomScope);
            RequestManager requestManager = new RequestManager(timestamp, categoryRequest);
            requestPile.AddRequest(categoryDTO.IdEditorial, requestManager);
            return Program.REQ_RESPONSE_POSITIVE;
        }

        //Updates the category
        [HttpPut("{idRequest}/{timestamp}")]
        public ActionResult<string> Put(string idRequest, long timestamp, [FromBody]DTOCategory categoryDTO)
        {
            CustomRequest req = new CustomRequest(idRequest, CustomRequest.FLAG_UPDATE, categoryDTO, categoryDTO.IdEditorial);
            CategoryRequestActions categoryRequest = new CategoryRequestActions(req, CustomScope);
            RequestManager requestManager = new RequestManager(timestamp, categoryRequest);
            requestPile.AddRequest(categoryDTO.IdEditorial, requestManager);
            return Program.REQ_RESPONSE_POSITIVE;
        }

        //Deletes the category
        [HttpDelete("{idRequest}/{timestamp}/{idCategory}")]
        public ActionResult<string> Delete(string idRequest, long timestamp, string idCategory, [FromBody]DTOModelBase dto)
        {
            Log.Error("CategoryController delete idPeitcion=" + idRequest + " idCategory=" + idCategory);

            CustomRequest req = new CustomRequest(idRequest, Entity.CustomRequest.FLAG_DELETE, idCategory, dto, dto.IdEditorial);
            CategoryRequestActions categoryRequest = new CategoryRequestActions(req, CustomScope);
            RequestManager requestManager = new RequestManager(timestamp, categoryRequest);
            requestPile.AddRequest(dto.IdEditorial, requestManager);
            return Program.REQ_RESPONSE_POSITIVE;
        }
    }
}