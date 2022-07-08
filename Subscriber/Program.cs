using StackExchange.Redis;
using System;

namespace Subscriber
{
    internal class Program
    {
        private const string RedisConnectionString = "127.0.0.1:6379";
        private static ConnectionMultiplexer connection =
                        ConnectionMultiplexer.Connect(RedisConnectionString);
        private const string Channel = "message-channel";
        static void Main(string[] args)
        {
            Console.WriteLine("Listening message-channel");
            var pubsub = connection.GetSubscriber();

            pubsub.Subscribe(Channel, (channel, message) => Console.Write
                        ("Message received from message-channel : " + message));

            Console.ReadLine();
        }
    }
}
