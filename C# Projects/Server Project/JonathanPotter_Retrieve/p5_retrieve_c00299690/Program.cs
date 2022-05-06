// Jonathan Potter
// C00299690
// CMPS 358
// project #5
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Text;


    class Program
    {
        static void Main(string[] args)
        {
            Client();
        }

        static void Client()
        {
            Console.Write("Enter IP: ");
            var IP = Console.ReadLine();
            Console.Write("Enter Port #: ");
            var port = Console.ReadLine();
            var portNum = int.Parse(port);
            using (TcpClient connectionToServer = new TcpClient(IP, portNum))
            using (NetworkStream theServer = connectionToServer.GetStream())
            {
                BinaryReader br = new BinaryReader(theServer);
                BinaryWriter bw = new BinaryWriter(theServer);

                try
                {
                      
                        Console.Write("Send: ");
                        string outgoing = Console.ReadLine();
                        bw.Write(outgoing);
                        bw.Flush();

                        Console.Write("Message: ");
                        string incomming = br.ReadString();
                        Console.WriteLine(incomming);
                    

                }
                catch
                {
                    Console.WriteLine("Connection Broken");
                }
            }
        }
    }  
