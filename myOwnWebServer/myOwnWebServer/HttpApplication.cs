﻿using System;
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
    class HttpApplication
    {
        /// <summary>
        /// D
        /// </summary>
        /// <param name="socketAgent"></param>
        //public  HttpApplication(Socket socketAgent)
        //    {

        //    }


        public void ProcessRequest(HttpContext context)
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
            string className = Path.GetFileNameWithoutExtension(context.RequestURL);

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
           
            string fileName = Path.Combine(currentWebDir, context.RequestURL.TrimStart('/'));

            context.BodyData = File.ReadAllBytes(fileName);
        }
    }
}
