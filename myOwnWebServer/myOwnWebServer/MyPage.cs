using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
{
    class MyPage:IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string strBody = @"<html><head></head><body><h2> big shit y</h2></body></html>";

            context.Response.BodyData = Encoding.UTF8.GetBytes(strBody);
        }
    }
}
