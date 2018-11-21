using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
{
    public class HttpResponse
    {
        private string requestFileExt;
        public byte[] BodyData { get; set; }
        public HttpResponse(HttpRequest request)
        {
            requestFileExt = System.IO.Path.GetExtension(request.RequestURL);
            
        }

        public byte[] GetHeader()
        {
            StringBuilder responseStr = new StringBuilder();
            responseStr.AppendFormat("HTTP/1.1 200 OK\r\n");
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
        public byte[] GetBodyData()
        {
            return BodyData;
        }

    }
}
