using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
{
    public class ParseCommandLine
    {
        public static string IpInput = string.Empty;
        public static string PathInput = string.Empty;
        public static string PortInput = string.Empty;


        public static bool CheckCommandLine()
        {
            
            bool isCommandLineOK = false;
            String[] args = Environment.GetCommandLineArgs();
            // if the number of arguments less 4 or the formt is not correct

            if ((Environment.GetCommandLineArgs().Length < 4)||(!(ParseCommand(ref args[1]) && ParseCommand(ref args[2]) && ParseCommand(ref args[3]))))
            {
                Console.WriteLine("Please input the command as the following format.");
                Console.WriteLine(@"myOwnWebServer –webRoot=C:\localWebSite –webIP=192.168.100.23 –webPort=5300");
                //Console.WriteLine("ERROR 1");
                //Console.WriteLine(Environment.GetCommandLineArgs().Length);

            }
            //else if (!(ParseCommand(ref args[1]) && ParseCommand(ref args[2]) && ParseCommand(ref args[3])))
            //{
            //    ////string webRoot = args[0];
            //    //Console.WriteLine("ERROR 2");
            //    //Console.WriteLine(args[0]);
            //    //Console.WriteLine(args[1]);
            //    //Console.WriteLine(args[2]);
            //    Console.WriteLine("The input of your format is not correct");
            //    Console.WriteLine(@"myOwnWebServer –webRoot=C:\localWebSite –webIP=192.168.100.23 –webPort=5300");
            //}
            else// here means the command line is good, 
            {
                /*     put code here     */
                // need some Validation for PATH,IP,PORT
                PathInput=args[1];
                IpInput=args[2];
                PortInput=args[3];
                isCommandLineOK = true;

            }

            return isCommandLineOK;

        }

        static bool ParseCommand(ref string userInput)
        {
            bool isInputOK = true;
            int index = userInput.IndexOf("=", 0);
            if (index == -1) //  not "=" been found in the argument
            {
                isInputOK = false;

            }
            else
            {
                userInput = userInput.Substring(index + 1);
            }
            return isInputOK;
        }



    }
}
