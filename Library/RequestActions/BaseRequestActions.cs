using Library.DBRepositories;
using Library.Entity;
using Library.RequestActions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Library.RequestManagers
{
    public abstract class BaseRequestActions : IRequestActions
    {
        /// <summary>
        /// The custom scope to get access to unitOfWork in background thread.
        /// </summary>
        protected CustomScope Scope { get; set; }

        /// <summary>
        /// The entity Json.
        /// </summary>
        protected string JsonOfTheEntity;

        /// <summary>
        /// The custom request
        /// </summary>
        protected CustomRequest Request;

        /// <summary>
        /// Logger
        /// </summary>
        protected static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// UnitOfWork - is a singleton by thread - there has to be only one in each thread
        /// </summary>
        private UnitOfWork unitOfWork;

        public UnitOfWork UnitOfWork
        {
            get
            {
                if (unitOfWork == null)
                    unitOfWork = Scope.ServiceScope.ServiceProvider.GetRequiredService<UnitOfWork>();
                return unitOfWork;
            }
        }

        /// <summary>
        /// Checks if the entity is null or not.
        /// </summary>
        /// <typeparam name="T"> The entity class</typeparam>
        /// <param name="entity">The entity name</param>
        protected void CheckEntity<T>(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("The entity is null.");
        }

        /// <summary>
        /// Checks if the string is null or not
        /// </summary>
        ///
        /// <param name="strValue">The string to be checked.</param>
        protected void CheckString(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                throw new ArgumentNullException("The string is empty.");
        }

        /// <summary>
        /// The action to be done.
        /// This method tries to make an action. If there is any error it reverts it. Finally it sends the
        /// result to an external api and if there were any errors, the external api returns a list of
        /// actions that should be discarded.
        /// For example: in this thread there are 3 actions that have to be executed one after another and
        /// depende on each other. If the first one fails the other 2 sould be discarded.
        /// </summary>
        ///
        /// <returns>List of ids of other actions to be discarded.</returns>
        public List<string> Do()
        {
            List<string> discardedActions = new List<string>();
            bool result = true;

            try
            {
                Process();
            }
            catch (Exception ex)
            {
                result = false;

                Logger.Error(ex, "Stopped program because of exception");

                try
                {
                    Revert();
                }
                catch (Exception exception)
                {
                    Logger.Error(exception, "There has been an error while reverting");
                }
            }
            finally
            {
                ExternalApiRequest externalApi = new ExternalApiRequest(UnitOfWork, Scope.HttpFactory);
                bool externalApiResult = externalApi.Send(Request.Id, result).Result;
                if (!string.IsNullOrEmpty(externalApi.JsonResponse))
                    discardedActions = JsonConvert.DeserializeObject<List<string>>(externalApi.JsonResponse);
                if (!externalApiResult)
                {
                    try
                    {
                        Revert();
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex, "There has been an error while reverting");
                    }
                }
            }
            return discardedActions;
        }

        public abstract void Process();

        public abstract void Revert();

        public string GetPetitionId()
        {
            return Request.Id;
        }
    }
}