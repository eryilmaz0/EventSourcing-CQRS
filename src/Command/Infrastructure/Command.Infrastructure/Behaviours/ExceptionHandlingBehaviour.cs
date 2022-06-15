using Command.Application.Abstracts.Command;
using MediatR;

namespace Command.Infrastructure.Behaviours;

public class ExceptionHandlingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : new()
{
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (Exception e)
        {
            dynamic commandResponse = new TResponse();
            commandResponse.IsSuccess = false;
            commandResponse.ResultMessage = e.Message;
            return commandResponse;
        }
    }
}