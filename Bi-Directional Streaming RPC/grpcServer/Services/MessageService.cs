using Grpc.Core;
using grpcServer;
using grpcMessageServer;
using System.Threading.Tasks;
using System;


namespace grpcServer.Services;

public class MessageService : Message.MessageBase
{

    public override async Task SendMessage(IAsyncStreamReader<MessageRequest> requestStream, IServerStreamWriter<MessageResponse> responseStream, ServerCallContext context)
    {

        var task1 =  Task.Run(async () => {
            while (await requestStream.MoveNext(context.CancellationToken))
            {
                var message = requestStream.Current;
                Console.WriteLine($"Message received: {message.Message}   /   Name received: {message.Name}");
                await responseStream.WriteAsync(new MessageResponse
                {
                    Message = "Message received!!!"
                });
            }
        });

        for (int i = 0; i < 10; i++)
        {
            await responseStream.WriteAsync(new MessageResponse
            {
                Message = "Message received!!!"
            });
            await Task.Delay(1000);
        }

        await task1;

    }


}
