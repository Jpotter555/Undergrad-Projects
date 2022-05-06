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
using Microsoft.VisualBasic.CompilerServices;


class Program
    {
        static Dictionary<int, string> dictionary = new Dictionary<int, string>();
        static int key = 0;

        static void Main(string[] args)
        {
            new Thread(Poster).Start();
            new Thread(Retriever).Start();
        }

        static void WriteToDictionary(string incoming)
        {
            lock (dictionary)
            {
                dictionary[key] = incoming;
            }
        }

        
        static void Poster()
            {
                TcpListener listenPost = new TcpListener(IPAddress.Any, 8081);
                listenPost.Start();
                Console.WriteLine($"Listening on 8081 ...");
                using (TcpClient clientConnect1 = listenPost.AcceptTcpClient())
                using (NetworkStream theClient1 = clientConnect1.GetStream())
                {
                    BinaryWriter bw = new BinaryWriter(theClient1);
                    BinaryReader br = new BinaryReader(theClient1);

                    try
                    {
                        while (true)
                        {
                            
                            string incomming = br.ReadString();
                            WriteToDictionary(incomming);
                            var sKey = key.ToString();
                            bw.Write(sKey);
                            bw.Flush();
                            key++;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Connection Broken");
                    }
                }
            }

        static void Retriever()
        {
            TcpListener listenRetrieve = new TcpListener(IPAddress.Any, 8082);
            listenRetrieve.Start();
            Console.WriteLine($"Listening on 8082 ...");
            using (TcpClient clientConnect2 = listenRetrieve.AcceptTcpClient())
            using (NetworkStream theClient2 = clientConnect2.GetStream())
            {
                BinaryWriter bw = new BinaryWriter(theClient2);
                BinaryReader br = new BinaryReader(theClient2);

                try
                {
                    while (true)
                    {
                        string incomming = br.ReadString();
                        var newKey = int.Parse(incomming);
                        var message = dictionary[newKey];
                        bw.Write(message);
                        bw.Flush();
                    }
                }
                catch
                {
                    Console.WriteLine("Connection Broken");
                }
            }
        }
    }
        
    
