using System;
using System.Collections.Generic;
using System.Text;

namespace MusZil_Core.API
{
    public class RequestFactory
    {
        
        /// <summary>
        /// Generates new MusRequest
        /// </summary>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static MusRequest New(string method, object[] parameters)
        {
            return new MusRequest(method, parameters);
        }
        /// <summary>
        /// Generates new MusRequest with one parameter
        /// </summary>
        /// <param name="method"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static MusRequest New(string method, string parameter = "")
        {
            return new MusRequest(method, new object[] { parameter });
        }
    }
}
