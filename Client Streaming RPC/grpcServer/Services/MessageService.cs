using Grpc.Core;
using grpcServer;
using grpcMessageServer;
using System.Threading.Tasks;
using System;


namespace grpcServer.Services;

public class MessageService : Message.MessageBase
{


    public override async Task<MessageResponse> SendMessage(IAsyncStreamReader<MessageRequest> requestStream, ServerCallContext context)
    {

      while(await requestStream.MoveNext(context.CancellationToken))
        {
            var message = requestStream.Current;
            Console.WriteLine($"Received message: {message.Message}   /    Received name: {message.Name}");
        }

       
        return new MessageResponse
        {
            Message = "Message received!!!"
        };
    }

}
