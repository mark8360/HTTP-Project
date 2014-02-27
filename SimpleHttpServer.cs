using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HTTP_Server
{
    class SimpleHttpServer
    {
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener serverSocket = new TcpListener(ip, 6789);

            serverSocket.Start();

            while (true)
            {
                Socket connectionSocket = serverSocket.AcceptSocket();
                Console.WriteLine("Server now active.");
                EchoService service = new EchoService(connectionSocket);
                Thread myThread = new Thread(new ThreadStart(service.DoIt));
                myThread.Start();
            }
            serverSocket.Stop();
        }
    }
}
