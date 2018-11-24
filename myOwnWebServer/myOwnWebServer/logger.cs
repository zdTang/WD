using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
{
    /// \brief The class is to log the event of the Server
    /// \details <b>Details</b>
    /// This class  comprises several methods for logging
    public class Logger
    {
        string logFileName; //   The path for log file
        StreamWriter sw;    //   A instance of StreamWriter


        /// \brief The Constructor of the class Logger
        /// \details <b>Details</b>
        /// This method will instantiate a Logger object
        

        public Logger(string path)
            {
            logFileName = Path.Combine(path, "Mylog.txt");

            if (!File.Exists(logFileName))
                File.Create(@logFileName).Close();

            
        }

        /// \brief The method is to log error information
        /// \details <b>Details</b>
        /// This method  is to log error information during the running of the Server
        /// \para  Error information
        /// \return  void
        public void Logerror(string content)
        {
            DateTime dt = DateTime.Now;
            string CurrentTime = dt.ToString("yyyy-MM-dd HH:mm:ss");
            StringBuilder log = new StringBuilder();
            string totalContent;
            log.Append("====[");
            log.Append(CurrentTime);
            log.Append("]>>Error");
            log.Append("====\r\n");
            log.Append(content);
            totalContent = log.ToString();

            sw = File.AppendText(logFileName);
            sw.WriteLine(totalContent);
            sw.Flush();
            sw.Close();
        }

        /// \brief The method is to log Request Header
        /// \details <b>Details</b>
        /// This method  is to log Request Header during the running of the Server
        /// \para  The Header of the Request
        /// \return  void
        public void LogRequest(string content)
        {
            DateTime dt = DateTime.Now;
            string CurrentTime = dt.ToString("yyyy-MM-dd HH:mm:ss");
            StringBuilder logRequest = new StringBuilder();
            string totalContent;
            logRequest.Append("====[");
            logRequest.Append(CurrentTime);
            logRequest.Append("]>>Request Header");
            logRequest.Append("====\r\n");
            logRequest.Append(content);
            totalContent = logRequest.ToString();
            
            sw = File.AppendText(logFileName);
            sw.WriteLine(totalContent);
            sw.Flush();
            sw.Close();

        }
        /// \brief The method is to log Response Header
        /// \details <b>Details</b>
        /// This method  is to log Response Header during the running of the Server
        /// \para  The Header of the Response
        /// \return  void
        public void LogResponse(string content)
        {
            DateTime dt = DateTime.Now;
            string CurrentTime = dt.ToString("yyyy-MM-dd HH:mm:ss");
            StringBuilder logResponse = new StringBuilder();
            string totalContent;
            logResponse.Append("====[");
            logResponse.Append(CurrentTime);
            logResponse.Append("]>>Respond Header");
            logResponse.Append("====\r\n");
            logResponse.Append(content);
            totalContent = logResponse.ToString();

            sw = File.AppendText(logFileName);
            sw.WriteLine(totalContent);
            sw.Flush();
            sw.Close();

        }

        /// \brief The method is to log usual Event
        /// \details <b>Details</b>
        /// This method  is to log usual event during the running of the Server
        /// \para  The Header of the Response
        /// \return  void
        public void LogEvent(string content)
        {
            DateTime dt = DateTime.Now;
            string CurrentTime = dt.ToString("yyyy-MM-dd HH:mm:ss");
            StringBuilder log = new StringBuilder();
            string totalContent;
            log.Append("====[");
            log.Append(CurrentTime);
            log.Append("]>>Event:");
            log.Append(content);
            totalContent = log.ToString();
            sw = File.AppendText(logFileName);
            sw.WriteLine(totalContent);
            sw.Flush();
            sw.Close();

        }


    }
}
