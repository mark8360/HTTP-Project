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
            TcpListener serverSocket = new TcpListener(ip, 8080);

            serverSocket.Start();
            try
            {
                Socket connectionSocket = serverSocket.AcceptSocket();
                Console.WriteLine("Request received.");
                ResponseService service = new ResponseService(connectionSocket);
                Thread myThread = new Thread(new ThreadStart(service.Respond));
                myThread.Start();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                serverSocket.Stop(); 
            }
                
 
            
        }
    }
}
