using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ShopOnline.Logger
{
    public class Logger<T>
    {
        public T Message { get; protected set; }

        public string GetMessage()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
