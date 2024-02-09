using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Model
{
    public class APIResponse<T>
    {

        public bool IsSuccess { get; set; } = true;
        public required string Message { get; set; }
        public  T? Result { get; set; }
    }
}
