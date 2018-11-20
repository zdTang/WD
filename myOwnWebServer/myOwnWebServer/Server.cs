using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
{
    class Server
    {
        public string Path { get; set; }
        public string IP { get; set; }
        public string Port { get; set; }

        public Server()
        {
            this.Path = ParseCommandLine.PathInput;
            this.IP = ParseCommandLine.IpInput;
            this.Port = ParseCommandLine.PortInput;

        }
    }
}
