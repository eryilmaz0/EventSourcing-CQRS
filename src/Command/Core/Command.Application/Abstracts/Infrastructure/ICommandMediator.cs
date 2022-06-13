namespace Command.Application.Abstracts.Infrastructure;

public interface ICommandMediator
{
    public Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request);
}