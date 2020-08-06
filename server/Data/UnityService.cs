using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;

namespace server.Data
{
    public class UnityService
    {
        private BlockingCollection<UnityMessage> inbox;
        IHttpContextAccessor httpContextAccessor;
        ConcurrentDictionary<string, int> clientIps = new ConcurrentDictionary<string, int>();

        UnityMessage savedMessage = null; 

        public UnityService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            inbox = new BlockingCollection<UnityMessage>(new ConcurrentQueue<UnityMessage>());
        }

        public void SendMessage(string mtype)
        {
            Console.WriteLine(httpContextAccessor.HttpContext.Connection.RemoteIpAddress);
            UnityMessage message = new UnityMessage(mtype);
            SendMessage(message);
        }

        public void SendMessage(UnityMessage message)
        {
            inbox.Add(message);
        }

        public void HandleUnityConnectionAsync()
        {
            while(true)
            {
                try
                {
                    TcpClient client = new TcpClient();
                    client.Connect("127.0.0.1", 8052);
                    using NetworkStream stream = client.GetStream();
                    {
                        while(client.Connected)
                        {
                            //if (savedMessage == null)
                            if (!inbox.TryTake(out savedMessage, 100))
                            {
                                savedMessage = new UnityMessage("heartbeat");
                            }

                            string jsonString = JsonSerializer.Serialize(savedMessage);
                            string jsonSocketString = jsonString.Length + "#" + jsonString;
                            Console.WriteLine(jsonSocketString);
                            byte[] buf = Encoding.ASCII.GetBytes(jsonSocketString);
                            stream.Write(buf, 0, buf.Length);
                            //savedMessage = null;
                        }
                        Console.WriteLine("Disconnected");
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }



        }

        public void Start()
        {
            Task.Run(HandleUnityConnectionAsync);
        }
    }
}
