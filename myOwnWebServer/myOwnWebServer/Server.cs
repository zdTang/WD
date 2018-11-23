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


        public void ServerStart()
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(this.IP);  // IP address
                IPEndPoint endPoint = new IPEndPoint(ipAddress, int.Parse(this.Port)); // Port to listen
                Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); // Create a Socket
                socketWatch.Bind(endPoint); // Socket bind a IP and Port
                socketWatch.Listen(1);  //  only support one Client
                                        //Thread theListen = new Thread(Listen);    // Listen is a method for listening
                                        //theListen.IsBackground = true;
                                        //theListen.Start(socketWatch);
                while(true)
                {
                    Socket socketAgent = socketWatch.Accept();
                    Thread threadAgent = new Thread(Agent);
                    threadAgent.IsBackground = true;
                    threadAgent.Start(socketAgent);
                }

                

            }
            catch
            {
                Console.WriteLine("Create first Socket ERROR");
            }
            
            
        }
        /// <summary>
        /// this thread keep on listening
        /// </summary>
        /// <param name="o"></param>
        //void Listen(object o)
        //{
        //    Socket socketWatch = o as Socket;
        //    while (true)
        //    {
        //        try
        //        {
        //            // once CLIENT connected, create another socketAgent to deal with
        //            Socket socketAgent = socketWatch.Accept();
        //            Thread threadAgent = new Thread(Agent);
        //            threadAgent.IsBackground = true;
        //            threadAgent.Start(socketAgent);
        //        }
        //        catch
        //        {
        //            Console.WriteLine("Create Agent Socket ERROR");
        //        }
        //    }

        //}

        /// <summary>
        /// this Method deal with Client
        /// </summary>
        /// <param name="o"></param>
        void Agent(object o)
        {
            Socket socketAgent = o as Socket;
            //HttpApplication httpApplication = new HttpApplication(socketAgent);


            byte[] byteBuffer = new byte[1024 * 1024];

            int numOfReceive = socketAgent.Receive(byteBuffer);
            // Read Request and store it into strRequest
            string strRequest = Encoding.ASCII.GetString(byteBuffer, 0, numOfReceive);
            Console.WriteLine(strRequest);
            socketAgent.Close();
            //Console.ReadKey();
            //ServerRun(strRequest, socketAgent);


        }

        /// <summary>
        /// deal with the requeat
        /// </summary>
        /// <param name="request"></param>
        public void ServerRun(string strRequest, Socket socket)
        {

            HttpContext context = new HttpContext(strRequest);
            HttpApplication application = new HttpApplication();

            application.ProcessRequest(context);

            socket.Send(context.Response.GetHeader());

            socket.Send(context.Response.BodyData);

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }








    }








}
