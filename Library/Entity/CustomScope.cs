using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Library.Entity
{
    /// <summary>
    /// This class allows to access services as a database connection or a http client to be called or used in a background thread
    /// </summary>
    public class CustomScope
    {
        /// <summary>
        /// Service scope to get the unit of work in a background thread
        /// </summary>
        public IServiceScope ServiceScope { get; set; }

        /// <summary>
        /// HttpClient to make requests from a background thread
        /// </summary>
        public IHttpClientFactory HttpFactory { get; set; }

        public CustomScope(IHttpClientFactory httpFactory, IServiceScopeFactory serviceScopeFactory)
        {
            HttpFactory = httpFactory;
            ServiceScope = serviceScopeFactory.CreateScope();
        }
    }
}