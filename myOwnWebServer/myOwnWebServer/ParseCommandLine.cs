using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
{
    /// \brief The class is to used for methods and properties for parsing the command line
    /// \details <b>Details</b>
    /// This class contains methods and properties for parsing the command line, so as to find the IP, PORT, Path 

    public class CommandLine
    {
        public static string IpInput = string.Empty;  // IP address
        public static string PathInput = string.Empty; // Path of the Website Folder
        public static string PortInput = string.Empty; // Port

        /// \brief The method to initiate the IP,Path,Port once the command line is legal 
        /// \details <b>Details</b>
        /// This method will get the IP,PORT,Path of the Website from the command line.
        /// \return  a boolean value to indicate if the command line is legal.
        public static bool CheckCommandLine()
        {
            
            bool isCommandLineOK = false;
            String[] args = Environment.GetCommandLineArgs();
            // if the number of arguments less 4 or the formt is not correct

            if ((Environment.GetCommandLineArgs().Length < 4)||(!(ParseCommand(ref args[1]) && ParseCommand(ref args[2]) && ParseCommand(ref args[3]))))
            {
                Console.WriteLine("Please input the command as the following format.");
                Console.WriteLine(@"myOwnWebServer –webRoot=C:\localWebSite –webIP=192.168.100.23 –webPort=5300");
 
            }
            
            else// here means the command line is good, 
            {
                PathInput=args[1];
                IpInput=args[2];
                PortInput=args[3];
                isCommandLineOK = true;

            }

            return isCommandLineOK;

        }

        /// \brief The method to parse the command
        /// \details <b>Details</b>
        /// This method will find the IP,PORT,Path of the Website from the command line.
        /// \return  a boolean value to indicate if the command line is legal.
        
        static bool ParseCommand(ref string userInput)
        {
            bool isInputOK = true;
            int index = userInput.IndexOf("=", 0);  // using the "=" as delimiter 
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
