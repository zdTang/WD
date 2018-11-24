using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
/// \brief The class is used to prepare stuff for the Response
/// \details <b>Details</b>
/// This class  comprises several properties and methods used for the Response
{
    public class HttpContext
    {
  
        public string RequestURL { get; set; }  // URL

        private string requestFileExt;        // The extension of required file
        public byte[] BodyData { get; set; }   // The content of target file
        public string ResponseStatus { get; set; }  // The status Code and explanation 

        public string strRequest { get; set; }  // The request string

        public static string fileName { set; get; }

        /// \brief The Constructor of Class HttpContext
        /// \details <b>Details</b>
        /// This method  is instantiate a HttpContext Class
        /// <param Request="The Request string"></param>

        public HttpContext(string Request)
        {
            this.strRequest = Request;
           
        }
        /// \brief This method is find the URL from the Request
        /// \details <b>Details</b>
        /// By parsing the Request string to find URL
        
        public void GetURL()
        {
            try
            {
                if (!string.IsNullOrEmpty(strRequest))
                {
                    string temp = strRequest.Replace("\r\n", "\r");
                    string[] lines = temp.Split('\r');
                    this.RequestURL = lines[0].Split(' ')[1];     // get URL from the request
                }
            }
            catch (Exception e)
            {
                Program.myServer.mylog.Logerror(e.Message);
            }

        }

        /// \brief This method is find the extension of the target file
        /// \details <b>Details</b>
        /// By parsing the Request string to find extension of the target file
        
        public void GetExtension()
        {
            requestFileExt = System.IO.Path.GetExtension(RequestURL);
        }


        /// \brief This method is to generate the Header of the Response
        /// \details <b>Details</b>
        /// generate the header of the Response
        /// \return  byte[] to hold the header

        public byte[] GenerateResponseHeader()
        {
            StringBuilder responseStr = new StringBuilder();
            try
            {
                responseStr.AppendFormat("HTTP/1.1 {0}\r\n", ResponseStatus);// The first line of the Response header
                responseStr.AppendFormat("Content=Type: {0}\r\n", GetContentType(requestFileExt)); // The second line of the Response header
                if (BodyData != null)
                {
                    responseStr.AppendFormat("Content-Length: {0}\r\n\r\n", BodyData.Length); // The third line of the response header
                }
                else
                {
                    responseStr.AppendFormat("Content-Length: 0\r\n\r\n");
                }
                
            }
            catch (Exception e)
            {

                Program.myServer.mylog.Logerror(e.Message);

            }
            return Encoding.ASCII.GetBytes(responseStr.ToString());
        }
        /// \brief This method is to generate the type of the content
        /// \details <b>Details</b>
        /// generate the type of the content
        /// \return  string to hold the type of the content
        public string GetContentType(string FileExtension)
        {
            string type = "text/html";
            switch (FileExtension)
            {
               
                case ".html":
                case ".htm":
                case ".txt":
                    type = "text/html";
                    break;
                 case ".gif":
                    type = "image/gif";
                    break;
                case ".jpg":
                case ".jpeg":
                    type = "image/jpeg";
                    break;
                
                default:
                    type = "text/plain";
                    break;
            }
            return type;
        }

       

        /// \brief This method is to generate the content of the target file and status code
        /// \details <b>Details</b>
        /// generate the content of the target file and status code
        /// \return  void
        public void ProcessRequest()
        {

            try
            {
                fileName = Path.Combine(Program.myServer.Path, RequestURL.TrimStart('/'));
                if (File.Exists(fileName))
                {
                    BodyData = File.ReadAllBytes(fileName);
                    ResponseStatus = "200 OK";
                }
                else
                {
                    BodyData = new byte[0];
                    ResponseStatus = "404 Not Found";
                }
            }
            catch (Exception e)
            {
                BodyData = new byte[0];
                ResponseStatus = "500 Internal Server Error";
                Program.myServer.mylog.Logerror(e.Message);
            }
        }

        /// \brief This method is to get the content of the target file
        /// \details <b>Details</b>
        /// generate the content of the target file 
        /// \return  byte array of the target file
        public byte[] GetBodyData()
        {
            return BodyData;
        }






    }
}
