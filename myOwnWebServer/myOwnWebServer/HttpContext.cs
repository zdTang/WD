using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
/// <summary>
/// The class HttpContext comprise two properties: one is an instance of Request.
/// Another one is an instance of Response
/// </summary>
{     
    public class HttpContext
    {
        //public HttpRequest Request { get; set; }// a instance of HttpRequest
        //public HttpResponse Response { get; set; } // a instance of Response

       



        public string clientMethod { get; set; }
        public string RequestURL { get; set; }

        private string requestFileExt;   // The extension of required file
        public byte[] BodyData { get; set; }
        public string ResponseStatus { get; set; }  // The status Code and explanation 


        ///// <summary>
        ///// constructor of HttpContext
        ///// </summary>
        ///// <param name="strRequest"></param>
        public HttpContext(string strRequest)
        {
            HttpRequest(strRequest);
            HttpResponse(this.RequestURL);
        }
        // Check if the "METHOD" is necessarily needed

        /// <summary>
        /// Parse the request string and get the URL and Method
        /// </summary>
        /// <param name="strRequest"></param>
        public void HttpRequest(string strRequest)
        {
            if (!string.IsNullOrEmpty(strRequest))
            {
                string temp = strRequest.Replace("\r\n", "\r");
                string[] lines = temp.Split('\r');
                this.clientMethod = lines[0].Split(' ')[0];   // get Client's method
                this.RequestURL = lines[0].Split(' ')[1];     // get URL from the request
            }
        }

         /// <summary>
         /// Find the format of the requested file from parsing the URL
         /// </summary>
         /// <param name="RequestURL"></param>
        public void HttpResponse(string RequestURL)
        {
            requestFileExt = System.IO.Path.GetExtension(RequestURL);
        }
        /// <summary>
        /// Create Header according to different situation
        /// </summary>
        /// <returns></returns>
        public byte[] GetHeader()
        {
            StringBuilder responseStr = new StringBuilder();

            // THE Response should change according to the different status
            responseStr.AppendFormat("HTTP/1.1 {0}\r\n", ResponseStatus);//"200 OK"
            responseStr.AppendFormat("Content=Type: {0}\r\n", GetContentType(requestFileExt));
            responseStr.AppendFormat("Content-Length: {0}\r\n\r\n", BodyData.Length);
            return Encoding.ASCII.GetBytes(responseStr.ToString());
        }

        public string GetContentType(string ext)
        {
            string type = "text/html";
            switch (ext)
            {
                case ".aspx":
                case ".html":
                case ".htm":
                    type = "text/html";
                    break;
                case ".png":
                    type = "image/png";
                    break;
                case ".gif":
                    type = "image/gif";
                    break;
                case ".jpg":
                case ".jpeg":
                    type = "image/jpeg";
                    break;
                case ".css":
                    type = "text/css";
                    break;
                case ".js":
                    type = "application/x-javascript";
                    break;
                default:
                    type = "text/plain";
                    break;
            }
            return type;
        }

        /// <summary>
        /// the BodyData IS A PROBLEM
        /// </summary>
        /// <returns></returns>
        public byte[] GetBodyData()
        {
            return BodyData;
        }










    }
}
