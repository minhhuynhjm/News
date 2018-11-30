using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace News.Common
{
    public class ResponseContainer<T>
    {
        public bool success { get; set; } = true;
        public T response { get; set; }
    }
}