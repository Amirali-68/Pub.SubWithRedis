using StackExchange.Redis;
using System;

namespace Publisher
{
    internal class Program
    {
        private const string RedisConnectionString = "127.0.0.1:6379";
        private static ConnectionMultiplexer connection =
                        ConnectionMultiplexer.Connect(RedisConnectionString);
        private const string Channel = "message-channel";
        private static string message = string.Empty;
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Enter your message: ");
                message = Console.ReadLine();

                var pubsub = connection.GetSubscriber();

                pubsub.PublishAsync(Channel, message);
                MessageAction(message);


            }
        }
        static void MessageAction(string message)
        {
            int initialCursorTop = Console.CursorTop;
            int initialCursorLeft = Console.CursorLeft;

#pragma warning disable CA1416 // Validate platform compatibility
            Console.MoveBufferArea(0, initialCursorTop, Console.WindowWidth, 1, 0, initialCursorTop + 1);
#pragma warning restore CA1416 // Validate platform compatibility
            Console.CursorTop = initialCursorTop;
            Console.CursorLeft = 0;

            // Print the message here
            Console.WriteLine($"'{message}' Successfully sent to message-channel");

            Console.CursorTop = initialCursorTop + 1;
            Console.CursorLeft = initialCursorLeft;
        }
    }
}
