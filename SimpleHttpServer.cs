using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HTTP_Server
{
    class SimpleHttpServer
    {
        private static readonly string fileName = @"c:/temp/file.txt";
        private static readonly string RootCatalog = "c:/temp";
        public static int DefaultPort = 8080;
        private static FileStream fs;


        public static string ReadFile()
        {
            string fileContents = "";
                try
                {
                    fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    if (fs.CanRead)
                    {
                        byte[] buffer = new byte[fs.Length];
                        int bytesRead = fs.Read(buffer, 0, buffer.Length);
                        fileContents = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        fs.Flush();
                        return fileContents;
                    }
                }
                catch (IOException ioex)
                {
                    Console.WriteLine(ioex.Message);
                }
                finally
                {
                    fs.Close();
                }

                return fileContents;
        }
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener serverSocket = new TcpListener(ip, DefaultPort);
            serverSocket.Start();
            try
            {
                while (true)
                {
                    Socket connectionSocket = serverSocket.AcceptSocket();
                    Console.WriteLine("Request received.");
                    ResponseService service = new ResponseService(connectionSocket);
                    Thread myThread = new Thread(new ThreadStart(service.Respond));
                    myThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                serverSocket.Stop(); 
            }
            
        }
    }
}
