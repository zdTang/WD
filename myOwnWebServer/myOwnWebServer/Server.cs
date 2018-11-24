using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace myOwnWebServer
{
    /// \brief Server Class
    /// \details <b>Details</b>
    /// This Class contains all Methods and Properties required for a Server
    /// \return  void

    public class Server
    {
        static bool isRun = true;  // To indicate if the server is running
        public string Path { get; set; } // To keep the Website folder path
        public string IP { get; set; } // To keep the IP address of the Website
        public string Port { get; set; } // To keep the Port of the Website

        public HttpContext context { get; set; } // An instance worked as a context for both Request and Response 
        public Logger mylog { get; set; }        // An instance for logging information of the current server.

        /// \brief Server Constructor
        /// \details <b>Details</b>
        /// This Method is to instantiate an object of Server Class
        /// \return  void
        public Server()
        {
            this.Path = CommandLine.PathInput;  // Obtain Website Folder from the command-line argument
            this.IP = CommandLine.IpInput;      // Obtain IP address from the command-line argument
            this.Port = CommandLine.PortInput;  // Obtain Port from the command-line argument
            mylog = new Logger(Path);
 
        }

        /// \brief The method to start a Server
        /// \details <b>Details</b>
        /// This method  will create socket to spy specified IP address and Port and response with
        /// the request from certain Client
        /// \return  void
        public void ServerStart()
        {
           
            try
            {
                IPAddress ipAddress = IPAddress.Parse(this.IP);  // IP address
                IPEndPoint endPoint = new IPEndPoint(ipAddress, int.Parse(this.Port)); // Port to listen
                Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); // Create a Socket
                socketWatch.Bind(endPoint); // Socket bind a IP and Port
                socketWatch.Listen(1);     //  only support one Client

               /*Once the socket accept a connection, a thread will be created to deal with the connection*/                      
                while(isRun)
                {

                    Socket socketAgent = socketWatch.Accept();
                    
                    mylog.LogEvent("Accept connection!");
                    Thread threadAgent = new Thread(Agent);   // Create a new thread to deal with this Connection
                    threadAgent.IsBackground = true;
                    threadAgent.Start(socketAgent);
                   
                }

               
            }
            catch (Exception e)
            {
                
                mylog.Logerror(e.Message);

            }
            
            
        }


        /// \brief The method is running by a thread
        /// \details <b>Details</b>
        /// This method  will response to request from a certain Client
        /// \para  The socket object created for the connection 
        /// \return  void
        void Agent(object o)
        {
            try
            {
              
                Socket socketAgent = o as Socket;
                byte[] byteBuffer = new byte[1024 * 1024];    // Buffer for receiving the request
                int numOfReceive = socketAgent.Receive(byteBuffer); // number of bytes of the request
                string strRequest = Encoding.ASCII.GetString(byteBuffer, 0, numOfReceive);// read from the socket
                mylog.LogRequest(strRequest);
                context = new HttpContext(strRequest); // create a Httpcontext object according to the Request String
                context.GetURL();   //  Parse the Request String
                context.GetExtension();  //  Parse the Request for the extension of the target file
               
                context.ProcessRequest(); //Generate the content for the Response action
                socketAgent.Send(context.GenerateResponseHeader());   //Send the Header of the Response
                mylog.LogResponse(System.Text.Encoding.Default.GetString(context.GenerateResponseHeader()));

                if (context.BodyData != null)
                {
                    socketAgent.Send(context.BodyData);  // Send the body of the Response
                }
                else
                {
                    context.BodyData = new byte[0]; // if cannot read the source file,  send 0 byte
                    socketAgent.Send(context.BodyData);
                }
                socketAgent.Shutdown(SocketShutdown.Both); // Shutdown the Socket

                mylog.LogEvent("Connection is Closed!\r\n\r\n");

            }
            catch(Exception e)
            {
                mylog.Logerror(e.Message);
            }

        }

        

    }








}
