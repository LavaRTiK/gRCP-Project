using Grpc.Core;
using GrpcMessageCounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gRCP_Server.Services
{
    public class MessageServiceImpl : MessageService.MessageServiceBase
    {
        public override async Task StreamMessages(IAsyncStreamReader<MessageRequest> requestStream, IServerStreamWriter<MessageResponse> responseStream, ServerCallContext context)
        {
            int count = 0;
            var chatResponse = Task.Run(async() => {
                while(await requestStream.MoveNext())
                {
                    count++;
                    var response = new MessageResponse()
                    {
                        Echo = requestStream.Current.Content,
                        TotalReceived = count
                    };
                    await responseStream.WriteAsync(response);
                }
            });
        }
    }
}
