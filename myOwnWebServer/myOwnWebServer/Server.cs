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
    /// <summary>
    /// Constructor of Server
    /// </summary>
    public class Server
    {
        static bool isRun = true;
        public string Path { get; set; }
        public string IP { get; set; }
        public string Port { get; set; }

        public HttpContext context { get; set; }
        public logger mylog { get; set; }

    public Server()
        {
            this.Path = ParseCommandLine.PathInput;
            this.IP = ParseCommandLine.IpInput;
            this.Port = ParseCommandLine.PortInput;
            mylog = new logger(Path);
 
        }


        public void ServerStart()
        {
           
            try
            {
                IPAddress ipAddress = IPAddress.Parse(this.IP);  // IP address
                IPEndPoint endPoint = new IPEndPoint(ipAddress, int.Parse(this.Port)); // Port to listen
                Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); // Create a Socket
                socketWatch.Bind(endPoint); // Socket bind a IP and Port
                socketWatch.Listen(1);  //  only support one Client
                                        
                while(isRun)
                {
                    Socket socketAgent = socketWatch.Accept();
                    
                    mylog.logEvent("Accept connection!");
                    Thread threadAgent = new Thread(Agent);
                    threadAgent.IsBackground = true;
                    threadAgent.Start(socketAgent);
                   
                }

               
            }
            catch (Exception e)
            {
                
                mylog.logError(e.Message);

            }
            
            
        }


        /// <summary>
        /// this Method deal with Client
        /// </summary>
        /// <param name="o"></param>
        void Agent(object o)
        {
            try
            {
              
                Socket socketAgent = o as Socket;
               
                byte[] byteBuffer = new byte[1024 * 1024];

                int numOfReceive = socketAgent.Receive(byteBuffer);
                // Read Request and store it into strRequest
                string strRequest = Encoding.ASCII.GetString(byteBuffer, 0, numOfReceive);
                mylog.logRequest(strRequest);
                context = new HttpContext(strRequest);
                context.HttpRequest();
                context.HttpResponse();
                
                Thread ServerRun = new Thread(Run);
                ServerRun.IsBackground = true;
                ServerRun.Start(socketAgent);
                
            }
            catch(Exception e)
            {
                mylog.logError(e.Message);
            }

        }

        /// <summary>
        /// deal with the requeat
        /// </summary>
        /// <param name="request"></param>
        //public void ServerRun(string strRequest, Socket socket)
       public void Run( object o)
        {
             Socket socketAgent = o as Socket;

            

            try
            {
                HttpApplication.ProcessRequest(context);
                socketAgent.Send(context.GetHeader());
                mylog.logResponse(System.Text.Encoding.Default.GetString(context.GetHeader()));
                
                if (context.BodyData != null)
                {
                    socketAgent.Send(context.BodyData);
                }
                else
                {
                    context.BodyData = new byte[0];
                    socketAgent.Send(context.BodyData);
                }
                socketAgent.Shutdown(SocketShutdown.Both);
                mylog.logEvent("Connection is Closed!");

            }
            catch (Exception e)
            {
                mylog.logError(e.Message);
            }

        }








    }








}
