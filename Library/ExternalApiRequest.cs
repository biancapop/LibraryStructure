using Library.DBRepositories;
using System.Net.Http;
using System.Threading.Tasks;

namespace Library
{
    public class ExternalApiRequest
    {
        
        private UnitOfWork UnitOfWork { get; set; }
        private IHttpClientFactory HttpFactory { get; set; }
        public string JsonResponse { get; set; }

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();


        public ExternalApiRequest(UnitOfWork unitOfWork, IHttpClientFactory httpFactory)
        {
            UnitOfWork = unitOfWork;
            HttpFactory = httpFactory;
            JsonResponse = "";
        }

        /// <summary>
        /// Sends the value of the action to another api.
        /// </summary>
        /// 
        /// <param name="id"></param>
        /// <param name="res"></param>
        /// <returns></returns>
        public async Task<bool> Send(string id, bool res)
        {
            //This method just makes a request to another api and gets the result of that request
            return true;
        }

    }
}