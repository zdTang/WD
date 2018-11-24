using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
{
    public class Program
    {
        public static Server myServer;
        static void Main(string[] args)
        {
            
            if(ParseCommandLine.CheckCommandLine())
            {
                myServer = new Server();
                myServer.ServerStart();
            }
 
        }

    }
}
