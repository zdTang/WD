using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
{
    /// <summary>
    /// 
    /// </summary>
    public static class HttpApplication
    {
        

        public static string fileName { set; get; }
       
        
        public static void ProcessRequest(HttpContext context)
        {
            
            try
            {
                fileName = Path.Combine(Program.myServer.Path, context.RequestURL.TrimStart('/'));
                if (File.Exists(fileName))
                {
                    context.BodyData = File.ReadAllBytes(fileName);
                    context.ResponseStatus = "200 OK";
                }
                else
                {
                    context.BodyData = new byte[0];
                    context.ResponseStatus = "404 Not Found";
                }
            }
            catch (Exception e)
            {
                context.BodyData = new byte[0];
                context.ResponseStatus = "500 Internal Server Error";
                Program.myServer.mylog.logError(e.Message);
            }
        }
    }
}
