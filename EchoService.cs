using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HTTP_Server
{
    class EchoService
    {
        private Socket connectionSocket;

        public EchoService(Socket connectionSocket)
        {
            this.connectionSocket = connectionSocket;
        }

        public void DoIt()
        {
            Stream ns = new NetworkStream(connectionSocket);
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            string message = sr.ReadLine();
            string answer = "Hello People!";
          
            sw.WriteLine(answer);
            connectionSocket.Close();
        }
        public string answer { get; set; }
    }
}
