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

        public void DoIt()
        {
            Stream ns = new NetworkStream(connectionSocket);
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            string request = sr.ReadLine();
            string answer = "Hello People!";
          
            sw.WriteLine(answer);
            sw.WriteLine(request);
            string[] words = request.Split(' ');
            foreach (string word in words)
            {
                Console.WriteLine(word);
            }
            connectionSocket.Close();
        }
        public string answer { get; set; }
    }
}
