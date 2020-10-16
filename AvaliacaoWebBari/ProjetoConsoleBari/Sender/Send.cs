using System;
using RabbitMQ.Client;
using System.Text;
using System.Diagnostics;

namespace Sender
{
    public class Send
    {
        static string rndNumber = Guid.NewGuid().ToString().Substring(1, 7);
        public static string senderID;
        static void Main(string[] args)
        {
            Random rnd = new Random();
            string rndNumber = rnd.Next(100000).ToString();
            int i = 0;
            var factory = new ConnectionFactory() {
                HostName = "localhost",
                UserName = "testes",
                Password = "userBari"
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);
                    while (i == 0) {
                        string randomID = Guid.NewGuid().ToString().Substring(1, 7);
                        string time = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                        string message = "time: " + time + " ID Sender: " + senderID + " ID requisição: " + randomID;
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: "",
                                             routingKey: "hello",
                                             basicProperties: null,
                                             body: body);
                        Console.WriteLine(" [x] Sent {0}", message);
                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
                    }
                }
            }
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
