using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
{
    public class HttpContext
    {
        public HttpRequest Request { get; set; }
        public HttpResponse Response { get; set; }

        /// <summary>
        /// constructor of HttpContext
        /// </summary>
        /// <param name="strRequest"></param>
        public HttpContext(string strRequest)
        {
            Request = new HttpRequest(strRequest);
            Response = new HttpResponse(Request);
        }
    }
}
