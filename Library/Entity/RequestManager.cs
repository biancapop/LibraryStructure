using Library.RequestActions;

namespace Library.Entity
{
    /// <summary>
    /// This class holds the request data: the timestamp of when it was made and the action that it has to make.
    /// </summary>
    public class RequestManager
    {
        /// <summary>
        /// Timestamp of the request
        /// </summary>
        public long Timestamp { get; set; }

        /// <summary>
        /// Action to be made
        /// </summary>
        public IRequestActions Action { get; set; }

        public RequestManager(long timestamp, IRequestActions requestActions)
        {
            Timestamp = timestamp;
            Action = requestActions;
        }
    }
}