using Grpc.Core;
using Grpc.Net.Client;
using GrpcMessageCounter;

Console.WriteLine("ww");
string GrpcUrl = "http://localhost:5000";
using var channel = GrpcChannel.ForAddress(GrpcUrl);
var client = new MessageService.MessageServiceClient(channel);
using var call = client.StreamMessages();
var responseTask = Task.Run(async () =>
{
    await foreach(var response in call.ResponseStream.ReadAllAsync())
    {
        Console.WriteLine($"Server:{response.Echo}, total amount:{response.TotalReceived}");
    }
});
Console.WriteLine("Type a message (input Empty line to exit):");
while (true)
{
    var input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input)) break;
    await call.RequestStream.WriteAsync(new MessageRequest { Content = input });
}
await call.RequestStream.CompleteAsync();
await responseTask;
