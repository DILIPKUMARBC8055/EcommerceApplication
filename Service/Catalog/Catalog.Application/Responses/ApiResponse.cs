using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Responses
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    public class ApiResponse<T> : ApiResponse
    {
        public T Data { get; set; }
    }

}
