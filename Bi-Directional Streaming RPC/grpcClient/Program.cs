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
            var channel = GrpcChannel.ForAddress("http://localhost:5230");

            var messageClient = new Message.MessageClient(channel);

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var request = messageClient.SendMessage();

            var task1 = Task.Run(async() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    await Task.Delay(1000);
                    await request.RequestStream.WriteAsync(new MessageRequest
                    {
                        Message = $"Request Number : {i}",
                        Name = "Exwii"
                    });
                }
            });

            while(await request.ResponseStream.MoveNext(cancellationTokenSource.Token))
            {
                Console.WriteLine($"Message received: {request.ResponseStream.Current.Message}");

            }

            await task1;
            await request.RequestStream.CompleteAsync();
            

        }
    }
}