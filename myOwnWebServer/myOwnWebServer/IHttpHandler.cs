﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
{
    interface IHttpHandler
    {
        void ProcessRequest(HttpContext context);
    }
}
