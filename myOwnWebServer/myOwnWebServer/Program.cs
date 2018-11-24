/*============================================================================
* FILE          :  Program.cs
* PROJECT       :  WDD Assignment #6
* PROGRAMMER    :  Zhendong Tang
* FIRST VERSION :  2018-11-24
* DESCRIPTION   :  This assignment is to create a HTTP server
*               :  The Main() is here.
=================================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOwnWebServer
{
    /// \brief The Program Class
    /// \details <b>Details</b>
    /// This Class contains Main() which is the entry point of the application
    /// \return  void
 
    public class Program
    {
        public static Server myServer;
        static void Main(string[] args)
        {
            
            if(CommandLine.CheckCommandLine())
            {
                myServer = new Server();   // Instantiate a server 
                myServer.ServerStart();    // Start the Server 
            }
 
        }

    }
}
