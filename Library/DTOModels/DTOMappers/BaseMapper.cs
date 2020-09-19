using System;

namespace Library.DTOModels.DTOMappers
{
    public class BaseMapper
    {
        /// <summary>
        /// Checks if the dynamic element id empty.
        /// </summary>
        ///
        /// <param name="element"> The dinamyc element.</param>
        /// <returns> True if it is empty, false if not. </returns>
        protected bool DynamicIsEmpty(dynamic element)
        {
            if (string.IsNullOrEmpty(Convert.ToString(element)))
            {
                return true;
            }
            return false;
        }
    }
}