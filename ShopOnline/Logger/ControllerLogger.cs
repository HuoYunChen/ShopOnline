using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.Logger
{
    public class ControllerLogger<T> : Logger<T>
    {
        public ControllerLogger(ControllerContext controllerContext, T message)
        {
            Controller = controllerContext.RouteData.Values["controller"].ToString();
            Action = controllerContext.RouteData.Values["action"].ToString();
            Message = message;
        }

        public string Controller { get; private set; }

        public string Action { get; private set; }

    }
}
