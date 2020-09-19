using Library.Entity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Library.Models
{
    public class RequestPile
    {
        /// <summary>
        /// Dictionary which contains the requests
        /// For each editorial a background thread is created, and all it's requests are executed on its own thread
        /// That way the requests are executed secuentially and if one fails the others that depend on that request
        /// get discarded.
        /// Dictionary<idEditorial,ConcurrentDictionary<timestampRequest, request>>
        ///     - the id of the editorial that defines the thread where the req should be executed
        ///     - the dictionary that keep the requests
        ///             - the timestamp of a request
        ///             - the action of the request
        /// </summary>
        private Dictionary<string, ConcurrentDictionary<long, RequestManager>> editorialsThreads = new Dictionary<string, ConcurrentDictionary<long, RequestManager>>();

        public RequestPile() { }

        /// <summary>
        /// Adds the request to the pile of the editorial.
        /// </summary>
        /// <param name="idEditorial"> Id of the editorial</param>
        /// <param name="request">The request</param>
        public void AddRequest(string idEditorial, RequestManager request)
        {
            CreateThreadIfNotExist(idEditorial);

            DateTime dt = DateTime.UtcNow.AddSeconds(1);
            long timestampToExecute = ((DateTimeOffset)dt).ToUnixTimeMilliseconds();
            try
            {
                editorialsThreads[idEditorial].TryAdd(timestampToExecute, request);
            }
            catch (Exception ex)
            {
                Console.WriteLine("The key already exists: " + ex.StackTrace);
                bool addedRequest = false;
                while (!addedRequest)
                {
                    dt = DateTime.UtcNow;
                    addedRequest = TryToAddRequest(dt, timestampToExecute, idEditorial, request);
                }
            }
        }

        private void CreateThreadIfNotExist(string idEditorial)
        {
            string key = editorialsThreads.Where(item => item.Key == idEditorial).Select(item => item.Key).FirstOrDefault();
            if (string.IsNullOrEmpty(key))
            {
                editorialsThreads.Add(idEditorial, new ConcurrentDictionary<long, RequestManager>());
                CreateThread(idEditorial);
            }
        }


        private bool TryToAddRequest(DateTime dt, long timestampToExecute, string idEditorial, RequestManager request)
        {
            try
            {
                dt.AddSeconds(0.5);
                timestampToExecute = ((DateTimeOffset)dt).ToUnixTimeSeconds();
                editorialsThreads[idEditorial].TryAdd(timestampToExecute, request);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private void CreateThread(string idEditorial)
        {
            var thread = new Thread(new ParameterizedThreadStart(OnStart));
            thread.IsBackground = true;
            thread.Start(idEditorial);
        }

        private void OnStart(object idEditorial)
        {
            while (true)
            {
                if (editorialsThreads.ContainsKey((string)idEditorial) && editorialsThreads[(string)idEditorial].Count > 0)
                {
                    ConcurrentDictionary<long, RequestManager> editorialRequests = editorialsThreads[(string)idEditorial];

                    long tsToExecute = GetTimestampToExecute(editorialRequests);
                    if (tsToExecute == 0)
                        Thread.Sleep(1000);
                    else
                    {
                        try
                        {
                            List<string> changesToDiscard = new List<string>();
                            editorialRequests.TryRemove(tsToExecute, out RequestManager requestManager);
                            if (requestManager != null)
                                changesToDiscard = requestManager.Action.Do();
                            //TODO Discard requests
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                }
                else
                {
                    Thread.Sleep(3000);
                }
            }
        }

        /// <summary>
        /// Gets the timestamp of the next request to execute
        /// </summary>
        /// 
        /// <param name="editorialRequests"> The editorials requests. </param>
        /// 
        /// <returns> The request timestamp. </returns>
        private long GetTimestampToExecute(ConcurrentDictionary<long, RequestManager> editorialRequests)
        {
            DateTime dt = DateTime.UtcNow;
            long actualTimestamp = ((DateTimeOffset)dt).ToUnixTimeMilliseconds();
            List<long> timestamps = new List<long>();

            foreach (long key in editorialRequests.Keys)
            {
                if (key <= actualTimestamp)
                    timestamps.Add(key);
            }

            long idToExecute = 0;
            long tsToExecute = 0;

            if (timestamps.Count == 0)
                return tsToExecute;

            foreach (long key in editorialRequests.Keys)
            {

                editorialRequests.TryGetValue(key, out RequestManager llamada);
                if (idToExecute == 0)
                {
                    idToExecute = llamada.Timestamp;
                    tsToExecute = key;
                }
                else if (idToExecute > llamada.Timestamp)
                {
                    idToExecute = llamada.Timestamp;
                    tsToExecute = key;
                }
            }
            return tsToExecute;
        }

    }
}