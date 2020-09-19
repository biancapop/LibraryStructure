using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Library.Controllers
{
    public class AddMissingContentType : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.HttpContext.Request.Headers["Content-Type"] = "application/json";
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }
    }
}