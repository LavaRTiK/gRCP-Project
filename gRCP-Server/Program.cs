using gRCP_Server.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");
var builder = WebApplication.CreateBuilder(args);
Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;
builder.Services.AddGrpc();
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(System.Net.IPAddress.Loopback, 5000, listenoption =>
    {
        listenoption.Protocols = HttpProtocols.Http2;
    });
});
var app = builder.Build();
app.MapGrpcService<MessageServiceImpl>();
app.Run();
