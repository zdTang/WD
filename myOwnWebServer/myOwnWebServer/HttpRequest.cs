using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
{
    class dd
    {
        public string clientMethod { get; set; }
        public string RequestURL { get; set;}

        public dd(string strRequest)
        {
            if(!string.IsNullOrEmpty(strRequest))
            {
                string temp = strRequest.Replace("\r\n", "\r");
                string[] lines = temp.Split('\r');
                this.clientMethod = lines[0].Split(' ')[0]; // get Client's method
                this.RequestURL=lines[0].Split(' ')[1];     // get URL from the request
            }
        }


    }
}
