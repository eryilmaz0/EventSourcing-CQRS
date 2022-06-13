using Command.Application.Abstracts.Infrastructure;
using MediatR;

namespace Command.Infrastructure.CommandMediator;

public class CommandMediator : ICommandMediator
{
    private readonly IMediator _mediator;

    public CommandMediator(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    

    public async Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request)
    {
        if (!typeof(IRequest<TResponse>).IsAssignableFrom(typeof(TRequest)))
            throw new ApplicationException();

        var handlerResponse = await _mediator.Send<TResponse>((IRequest<TResponse>) request);
        return handlerResponse;
    }
}