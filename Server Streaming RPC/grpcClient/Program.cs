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
            var channel = GrpcChannel.ForAddress("http://localhost:5235");
            
            var messageClient = new Message.MessageClient(channel);

            var response = messageClient.SendMessage(new MessageRequest
            {
               Name = "Exwii",
               Message = "Test Hello"
            });

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            
            while (await response.ResponseStream.MoveNext(cancellationTokenSource.Token))
            {
                System.Console.WriteLine(response.ResponseStream.Current.Message);
            }

        }
    }
}