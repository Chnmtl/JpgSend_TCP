using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TcpSender
{
    class Program
    {
        static void Main(string[] args)
        {
           
            TcpClient clientsent = new TcpClient("10.31.8.215", 8080);
            NetworkStream n = clientsent.GetStream();
            /* TCP Listen method */
            TcpListener listen = new TcpListener(IPAddress.Any, 1200);
            listen.Start();
            TcpClient client = listen.AcceptTcpClient();
            Console.WriteLine("Client connected.....");



            bool x = true;           
           
            while (x == true)
            {
                NetworkStream stream = client.GetStream(); // Getting array length
                byte[] buffer = new byte[client.ReceiveBufferSize];
                int data = stream.Read(buffer, 0, client.ReceiveBufferSize);
               
                //string ch = Encoding.ASCII.GetString(buffer, 0, data);
                for (int i=0; i<data-1; i++)
                {
                    NetworkStream stream2 = client.GetStream();
                    byte[] returndata = new byte[client.ReceiveBufferSize];
                    int data2 = stream2.Read(buffer, 0, client.ReceiveBufferSize);
                    
                    Console.WriteLine("jpg is receiving...");
                }            
                
                
                
                NetworkStream stream1 = client.GetStream();
                byte[] returnd = new byte[client.ReceiveBufferSize];
                int data1 = stream1.Read(returnd, 0, client.ReceiveBufferSize);
                string ch1 = Encoding.Unicode.GetString(returnd, 0, data1);

                if (ch1 == "Done")
                {
                    x = false;
                    Console.WriteLine("Done");
                }
                Console.ReadKey();
                x = false;


            }

            

            Console.WriteLine(".jpg is downloaded succesfully.");
            Thread.Sleep(50000);
        }
    }
}
