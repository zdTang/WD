﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
{
    public class logger
    {
        string logFileName;
        StreamWriter sw;
        public logger(string path)
            {
            logFileName = Path.Combine(path, "Mylog.txt");

            if (!File.Exists(logFileName))
                File.Create(@logFileName).Close();

            
        }

        public void logError(string content)
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
        public void logRequest(string content)
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

        public void logResponse(string content)
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

        public void logEvent(string content)
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
