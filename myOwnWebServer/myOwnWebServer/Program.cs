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

                //Console.WriteLine(myServer.Path);
                //Console.WriteLine(myServer.IP);
                //Console.WriteLine(myServer.Port);
                myServer.mylog.logEvent("Server is closed!");
                //Console.WriteLine("PUT CODE HERE");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("NOT OK");
                Console.WriteLine("GONNA QUIT");
                Console.ReadKey();
            }

        }


        


    }
}
