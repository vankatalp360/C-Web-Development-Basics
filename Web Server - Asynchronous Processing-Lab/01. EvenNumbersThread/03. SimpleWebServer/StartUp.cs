using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _03._SimpleWebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress address = IPAddress.Parse("127.0.0.1");
            int port = 1300;

            TcpListener listener = new TcpListener(address, port);
            listener.Start();

            Console.WriteLine($"Server started.");
            Console.WriteLine($"Listener to TCP clients at 127.0.0.1:{port}");

            var task = Task.Run(() => ConnectWithClient(listener));
            task.Wait();
        }

        public static async Task ConnectWithClient(TcpListener listener)
        {
            while (true)
            {
                Console.WriteLine("Waiting for client...");
                var client = await listener.AcceptTcpClientAsync();

                Console.WriteLine("Client connected.");

                byte[] buffer = new byte[1024];
                await client.GetStream().ReadAsync(buffer, 0, buffer.Length);

                var message = Encoding.UTF8.GetString(buffer);
                Console.WriteLine(message.Trim('\0'));

                var responseMessage = "HTTP/1.1.200 OK\r\nContent-Type: text/plain\r\n\r\nHello from server!";
                byte[] data = Encoding.UTF8.GetBytes(responseMessage);
                await client.GetStream().WriteAsync(data, 0, data.Length);

                Console.WriteLine("Clossing connection.");
                client.Dispose();
            }
        }
    }
}
