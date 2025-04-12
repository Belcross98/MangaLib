using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vaporAPI.Helpers
{
    public class ResponseType
    {
        public List<string> Errors { get; set; }
        public List<ResponseTypeCode> Code { get; set; }
        public bool Success { get; set; }
        public object Data { get; set; }
    }

    public class ResponseTypeCode
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}