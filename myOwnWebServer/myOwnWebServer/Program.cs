using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            
            if(ParseCommandLine.CheckCommandLine())
            {
                Server myServer = new Server();

                Console.WriteLine(myServer.Path);
                Console.WriteLine(myServer.IP);
                Console.WriteLine(myServer.Port);
                Console.WriteLine("PUT CODE HERE");
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
