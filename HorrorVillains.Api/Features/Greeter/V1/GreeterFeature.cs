

using Grpc.Core;

namespace HorrorVillains.Api.Features.Greeter.V1;

public class GreeterFeature : GreeterService.GreeterServiceBase
{
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}   