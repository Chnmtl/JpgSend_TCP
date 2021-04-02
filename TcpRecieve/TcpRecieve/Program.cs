using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TcpRecieve
{
    class Program
    {
        public static string jpg;
        static void Main(string[] args)
        {
            
            TcpListener listen = new TcpListener(IPAddress.Any, 8080);
            listen.Start();
            TcpClient clientlisten = listen.AcceptTcpClient();
            NetworkStream stream = clientlisten.GetStream();
            ///////////////////////tcp sent /////////////////////////
            TcpClient client = new TcpClient("10.31.8.215", 1200);
            Console.WriteLine("Connecting.....");
            NetworkStream n = client.GetStream();
            Console.WriteLine("Connected");

            Console.ReadKey();
            Boolean control;
            string jpgfile;
            
            control = true;
            while (control == true)
            {
                Console.WriteLine(".jpg file");
                jpgfile = Console.ReadLine();
                if (File.Exists(jpgfile))
                {
                    using (StreamReader sr = new StreamReader(jpgfile))
                        jpg = sr.ReadToEnd();
                    Console.WriteLine("jpg is ok");

                    //control = false;

                }
                else
                {
                    control = true;
                }

                byte[] packetdata = Encoding.ASCII.GetBytes(jpg);
                Console.WriteLine("" + packetdata.Length);
                List<byte[]> byteArrayList = new List<byte[]>();
                int x = packetdata.Length / 1024;
                int y = packetdata.Length % 1024;

                for (int b = 0; b < x; b++)
                {
                    byte[] bytes = new byte[1024];
                    int index = 0;
                    for (int a = b * 1024; a < b * 1024 + 1024; a++)
                    {

                        bytes[index] = packetdata[a];
                        index++;
                    }
                    byteArrayList.Add(bytes);
                }
                byte[] remain = new byte[y];
                int index2 = 0;
                for (int c = x * 1024; c < x * 1024 + y; c++)
                {
                    remain[index2] = packetdata[c];
                    index2++;
                }
                Console.WriteLine(x.ToString());
                byteArrayList.Add(remain);
                byte[] araylenght = new byte[x+1];
                n.Write(araylenght, 0, araylenght.Length);

                for (int i = 0; i < x + 1; i++)
                {
                    n.Write(byteArrayList[i], 0, byteArrayList[i].Length); // Sending array         

                    Console.WriteLine(".jpg is sending." + byteArrayList[i].Length);
                    control = false;

                }

                string final = "Done";
                byte[] finalbyte = Encoding.ASCII.GetBytes(final);
                n.Write(finalbyte, 0, finalbyte.Length);                
                
            }
            Console.WriteLine(".jpg is sent succesfully.");
            Thread.Sleep(50000);
            
        }
    }
}
