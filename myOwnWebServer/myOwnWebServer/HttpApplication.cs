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
        /// <summary>
        /// D
        /// </summary>
        /// <param name="socketAgent"></param>
        //public  HttpApplication(Socket socketAgent)
        //    {

        //    }

        public static string fileName { set; get; }
        public static void ProcessRequest(HttpContext context)
        {
            
            string ext = Path.GetExtension(context.RequestURL);

            switch (ext)
            {
                case ".jpg":
                case ".jpeg":
                case ".html":
                case ".htm":
                case ".css":
                case ".js":
                    ProcessStaticFile(context);
                    break;
                //case ".aspx":
                //    ProcessDynamicFile(context);
                //    break;
                default:
                    ProcessStaticFile(context);
                    break;

            }
            
        }

        //public static  void ProcessDynamicFile(HttpContext context)
        //{
        //    ///IndexPage.aspx
        //    /// //mypage
        //    string className = Path.GetFileNameWithoutExtension(context.RequestURL);

        //    string nameSpace = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace;
        //    //HeimaIIS.IndexPage
        //    string fullName = nameSpace + "." + className;

        //    IHttpHandler obj = (IHttpHandler)System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(fullName, true);

        //    if (obj == null)
        //    {
        //        //404 
        //    }
        //    else
        //    {
        //        obj.ProcessRequest(context);
        //    }


        //}

        public static void ProcessStaticFile(HttpContext context)
        {
            //string currentWebDir = AppDomain.CurrentDomain.BaseDirectory;
            // Check if file exist so as to set up different status code
            
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
                    context.BodyData = File.ReadAllBytes(fileName);
                    context.ResponseStatus = "404 Not Found";
                }
            }
            catch
            {
                context.BodyData = new byte[0];
                 
                    context.ResponseStatus = "404 Not Found";
            }
           
           
        }
    }
}
