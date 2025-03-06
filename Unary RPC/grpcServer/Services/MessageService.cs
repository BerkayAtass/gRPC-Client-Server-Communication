using Grpc.Core;
using grpcServer;
using grpcMessageServer;
using System.Threading.Tasks;

namespace grpcServer.Services;

public class MessageService : Message.MessageBase
{

    public override Task<MessageResponse> SendMessage(MessageRequest request, ServerCallContext context)
    {
        System.Console.WriteLine("Message: " + request.Message + "  /  Name : " + request.Name);
        return Task.FromResult(new MessageResponse
        {
            Message = "Message Received Successfully",
        });

    }

}
