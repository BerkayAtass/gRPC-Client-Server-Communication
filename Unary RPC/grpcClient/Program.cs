using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using grpcServer;
using grpcMessageClient;

namespace MyApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5073");
            //var greetClient = new Greeter.GreeterClient(channel);
            var messageClient = new Message.MessageClient(channel);

            MessageResponse response = await messageClient.SendMessageAsync(new MessageRequest 
            {
                Message = "Hello from Exwii",
                Name = "Exwii Test"
            });


            //HelloReply response = await greetClient.SayHelloAsync(new HelloRequest 
            //{ 
            //    Name = "Exwii Test"
            //});

            System.Console.WriteLine(response.Message);

        }
    }
}