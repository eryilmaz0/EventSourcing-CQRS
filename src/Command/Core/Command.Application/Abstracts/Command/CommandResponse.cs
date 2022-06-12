namespace Command.Application.Abstracts.Command;

public abstract class CommandResponse
{
    public bool IsSuccess { get; set; }
    public string ResultMessage { get; set; }
}