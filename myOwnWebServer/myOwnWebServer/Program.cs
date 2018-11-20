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
                Console.WriteLine("OK");
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
