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
            sw.WriteLine("HTTP/1.0 200 OK");
            sw.WriteLine("Connection: close");
            sw.WriteLine("Date: " + DateTime.Now);
            sw.WriteLine("Server: Simple HTTP server, written in C#");
            sw.WriteLine("Content-Typ: text/html");
            sw.WriteLine("\r\n");
            sw.WriteLine(answer);
            string[] words = request.Split(' ');
            sw.WriteLine("You've requested: " + words[1]);
            connectionSocket.Close();
        }
        public string answer { get; set; }
    }
}
