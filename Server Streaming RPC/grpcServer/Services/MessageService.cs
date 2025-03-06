using Grpc.Core;
using grpcServer;
using grpcMessageServer;
using System.Threading.Tasks;
using System;


namespace grpcServer.Services;

public class MessageService : Message.MessageBase
{

    public override async Task SendMessage(MessageRequest request, IServerStreamWriter<MessageResponse> responseStream, ServerCallContext context)
    {

        System.Console.WriteLine($"Received message: {request.Message}   /    Received name: {request.Name}");

        for (int i = 0; i < 10; i++)
        {
            await Task.Delay(1000);
            await responseStream.WriteAsync(new MessageResponse
            {
                Message = $"Hello {request.Name} {i}"
            });
        }
    }

}
