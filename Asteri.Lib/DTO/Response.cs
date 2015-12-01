using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteri.Lib.DTO
{
    public class Response
    {
        public enum Results {success, notSuccess, error}
        public Results Result { get; set; }
        public string Message { get; set; }
    }

    public class Response<T> : Response
    {
        public T Data { get; set; }
    }
}
