using MediatR;

namespace Command.Application.Abstracts.Infrastructure;

public class CommandMediator : ICommandMediator
{
    private readonly IMediator _mediator;

    public CommandMediator(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<TResponse> Send<TRequest, TResponse>(TRequest request)
    {
        if (!typeof(TRequest).IsAssignableFrom(typeof(IRequest<TResponse>)))
            throw new ApplicationException();

        var castedRequest = (IRequest<TResponse>)request;

        var response = await _mediator.Send<TResponse>(castedRequest);
        return response;
    }
}