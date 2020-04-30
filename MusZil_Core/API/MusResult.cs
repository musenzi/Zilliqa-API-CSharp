using System;
using System.Collections.Generic;
using System.Text;

namespace MusZil_Core.API
{
    public class MusResult
    {
        public MusResult(object result, string msg)
        {
            Result = result;
            Message = msg;
        }
        public object Result { get; set; }
        public string Message { get; set; }
        public bool Error { get; set; }
        public string ErrorMessage { get; set; }

    }
}
