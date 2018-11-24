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
                socketWatch.Listen(10);  //  only support one Client
                                        //Thread theListen = new Thread(Listen);    // Listen is a method for listening
                                        //theListen.IsBackground = true;
                                        //theListen.Start(socketWatch);
                while(isRun)
                {
                    Socket socketAgent = socketWatch.Accept();
                    Console.WriteLine("======find connection!========");
                    Thread threadAgent = new Thread(Agent);
                    threadAgent.IsBackground = true;
                    threadAgent.Start(socketAgent);
                    Console.WriteLine("======Thread Agent go=======");
                }

                // Once receive the command to shutdown the server
                Console.WriteLine("Receive order to shut down!");
                Console.ReadKey();
                return;
                

            }
            catch
            {
                Console.WriteLine("Create first Socket ERROR");
            }
            
            
        }
       
        //}

        /// <summary>
        /// this Method deal with Client
        /// </summary>
        /// <param name="o"></param>
        void Agent(object o)
        {
            Console.WriteLine("==thread Agent start.==");
            Socket socketAgent = o as Socket;
            //HttpApplication httpApplication = new HttpApplication(socketAgent);


            byte[] byteBuffer = new byte[1024 * 1024];

            int numOfReceive = socketAgent.Receive(byteBuffer);
            // Read Request and store it into strRequest
            string strRequest = Encoding.ASCII.GetString(byteBuffer, 0, numOfReceive);
            context = new HttpContext(strRequest);
            context.HttpRequest();
            context.HttpResponse();
            Console.Clear();
            Console.WriteLine("======Thread agent message=======");
            Console.WriteLine(context.RequestURL);
            Console.WriteLine(context.clientMethod);
            //===========================================================
            //   check the content of the string
            //if (shutdown command, then isRun=false,and will not display the message.
            //   put some code here.
            //============================================================
            //ServerRun(strRequest,socketAgent);
            Thread ServerRun= new Thread(Run);
            ServerRun.IsBackground = true;
            ServerRun.Start(socketAgent);
            //Console.WriteLine("======message 2=======");
            //ServerRun(socketAgent);
            Console.WriteLine("======THread Run go!=====");
            Console.WriteLine(strRequest);
            //Thread.Sleep(3000);
            //socketAgent.Close();
            //Console.ReadKey();
            //
            Console.WriteLine("===thread Agent Done.=====");

        }

        /// <summary>
        /// deal with the requeat
        /// </summary>
        /// <param name="request"></param>
        //public void ServerRun(string strRequest, Socket socket)
       public void Run( object o)
        {
            Console.WriteLine("==thread Run start.==");
            Socket socketAgent = o as Socket;

            HttpApplication.ProcessRequest(context);

            try
            {
                socketAgent.Send(context.GetHeader());

                if (context.BodyData != null)
                {
                    socketAgent.Send(context.BodyData);
                    Console.WriteLine("sent message done. has message");
                }
                else
                {
                    Console.WriteLine("body is null!");
                    context.BodyData = new byte[0];
                    socketAgent.Send(context.BodyData);
                }
            }
            catch
            {
                Console.WriteLine("expection!");
            }


            socketAgent.Shutdown(SocketShutdown.Both);
            //socket.Close();
            Console.WriteLine("socket final closed.");
            //Thread.Sleep(3000);
            //Console.Clear();
            Console.WriteLine("===thread Run Done.===");
        }








    }








}
