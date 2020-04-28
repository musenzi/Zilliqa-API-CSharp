using System;
using System.Collections.Generic;
using System.Text;

namespace MusZil_Core
{
    public class MusRequest
    {
        public MusRequest(string method, string param)
        {
            Id = 1;
            Method = method;
            Parameters = param;
        }

        public int Id { get; set; }
        public string Jsonrpc { get; set; }
        public string Method { get; set; }
        public object Parameters { get; set; }

        public string ToJson()
        {
            return $"{{\r\n    \"id\": \"{Id}\",\r\n    \"jsonrpc\": \"2.0\",\r\n    \"method\": \"{Method}\",\r\n    \"params\": [\"{Parameters}\"]\r\n}}";
        }
    }
}
