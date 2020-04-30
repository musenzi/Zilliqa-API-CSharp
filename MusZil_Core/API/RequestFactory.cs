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
        public static MusRequest New(string method, string parameters = "")
        {
            return new MusRequest(method, parameters);
        }
    }
}
