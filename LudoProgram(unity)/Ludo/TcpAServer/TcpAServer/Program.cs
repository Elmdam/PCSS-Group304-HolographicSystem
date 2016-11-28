using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TcpAServer
    
{
	public class TcpAServer
	{
		public static void Main()
		{
            TcpServer server = new TcpServer(1234);
            

        }

        public class TcpServer
        {
            private TcpListener server;
            private bool isRunning;
            private int numberOfClients;
            string[,] dataArray;
            StreamWriter writer;
            StreamReader reader;

            public TcpServer(int port)
            {
                
                dataArray = new string[4, 2];
                Console.WriteLine("Starting server...");
                
                server = new TcpListener(IPAddress.Loopback, port);
                server.Start();

                isRunning = true;

                LoopClients();

            }
            public void LoopClients()
            {
                while (true)
                {
                    TcpClient newClient = server.AcceptTcpClient();

                    Thread thread = new Thread(new ParameterizedThreadStart(HandleClient));
                    thread.Start(newClient);
                    
                }
            }
            public void HandleClient(object obj)
            {
                TcpClient client = (TcpClient)obj;

                writer = new StreamWriter(client.GetStream(), Encoding.ASCII) { AutoFlush = true };
                reader = new StreamReader(client.GetStream(), Encoding.ASCII);


                bool ClientConnected = true;
                string data = null;


                numberOfClients++;
                Console.WriteLine("Current number of clients connected: {0}", numberOfClients);
                

                while (ClientConnected)
                {

                    ReceiveData();
                }
                
            }
           public void ReceiveData()
            {
                
                for (int i = 0; i < dataArray.GetLength(0); i++)
                {
                    
                    for (int j = 0; j < dataArray.GetLength(1); j++)
                    {

                        dataArray[i, j] = reader.ReadLine();
                        Console.WriteLine("Brick " + (1+i) + " position X and Y: " + dataArray[i, j]);


                    }
                }
                
                
            }
            

        }
        
	}
}