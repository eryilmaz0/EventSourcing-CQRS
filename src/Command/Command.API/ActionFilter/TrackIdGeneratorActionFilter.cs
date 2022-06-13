using Microsoft.AspNetCore.Mvc.Filters;

namespace Command.API.ActionFilter;

public class TrackIdGeneratorActionFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var commandRequestModel = context.ActionArguments.FirstOrDefault(x => x.Value != null).Value;

        if (!typeof(Application.Abstracts.Command.Command).IsAssignableFrom(commandRequestModel.GetType()))
            throw new InvalidOperationException("You can not send a non command model.");

        Application.Abstracts.Command.Command castedRequestModel = (Application.Abstracts.Command.Command)commandRequestModel;
        castedRequestModel.TrackId = Guid.NewGuid();

        await next();
    }
}