using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
{
    public class IndexPage:IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string strBody = @"<html><head></head><body><h2>shit y ai chi</h2></body></html>";

            context.BodyData = Encoding.UTF8.GetBytes(strBody);
        }

    }
}
