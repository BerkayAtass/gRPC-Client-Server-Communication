using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using grpcServer;
using grpcMessageClient;
using System.Threading;

namespace MyApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5006");

            var messageClient = new Message.MessageClient(channel);

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var request = messageClient.SendMessage();

            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);
                await request.RequestStream.WriteAsync(new MessageRequest
                {
                    Message = $"Request Number : {i}",
                    Name = "Exwii"
                });
            }
            await request.RequestStream.CompleteAsync();
            System.Console.WriteLine((await request.ResponseAsync).Message);
        
        }
    }
}