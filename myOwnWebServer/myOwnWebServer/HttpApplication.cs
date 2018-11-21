using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
{
    class HttpApplication
    {
        public void ProcessRequest(HttpContext context)
        {
            
            string ext = Path.GetExtension(context.Request.RequestURL);

            switch (ext)
            {
                case ".jpg":
                case ".jpeg":
                case ".html":
                case ".htm":
                case ".css":
                case ".js":
                    ProcessStaticFile(context); break;
                case ".aspx":
                    ProcessDynamicFile(context);
                    break;
                default:
                    ProcessStaticFile(context);
                    break;

            }
            
        }

        private void ProcessDynamicFile(HttpContext context)
        {
            ///IndexPage.aspx
            /// //mypage
            string className = Path.GetFileNameWithoutExtension(context.Request.RequestURL);

            string nameSpace = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace;
            //HeimaIIS.IndexPage
            string fullName = nameSpace + "." + className;

            IHttpHandler obj = (IHttpHandler)System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(fullName, true);

            if (obj == null)
            {
                //404 
            }
            else
            {
                obj.ProcessRequest(context);
            }


        }

        public void ProcessStaticFile(HttpContext context)
        {
            string currentWebDir = AppDomain.CurrentDomain.BaseDirectory;
           
            string fileName = Path.Combine(currentWebDir, context.Request.RequestURL.TrimStart('/'));

            context.Response.BodyData = File.ReadAllBytes(fileName);
        }
    }
}
