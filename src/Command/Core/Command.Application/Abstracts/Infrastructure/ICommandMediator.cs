namespace Command.Application.Abstracts.Infrastructure;

public interface ICommandMediator
{
    public Task<TResponse> Send<TRequest, TResponse>(TRequest request);
}