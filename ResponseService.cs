using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HTTP_Server
{
    class ResponseService
    {
        private Socket connectionSocket;
        private SimpleHttpServer shs;

        public ResponseService(Socket connectionSocket)
        {
            this.connectionSocket = connectionSocket;
        }

        public void Respond()
        {
            Stream ns = new NetworkStream(connectionSocket);
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            string request = sr.ReadLine();
            string answer = "Hello People!";
            sw.WriteLine("HTTP/1.0 200 OK \r\n");
            sw.WriteLine("Connection: open \r\n");
            sw.WriteLine("Date: " + DateTime.Now +"\r\n");
            sw.WriteLine("Server: Simple HTTP server, written in C# \r\n");
            sw.WriteLine("Content-Type: text/html \r\n");
            sw.WriteLine("\r\n");
            sw.WriteLine(answer);
            string[] words = request.Split(' ');
            sw.WriteLine("You've requested: " + words[1]);
            //sw.WriteLine("<br />");
            if (words[1] == "/file.html")
            {
                //sw.WriteLine("<br />");
                sw.Write("Contents of file.html:\r\n");
                sw.WriteLine("");
                sw.Write(SimpleHttpServer.ReadFile());
            }
            connectionSocket.Close();
        }
        public string answer { get; set; }
    }
}
