using System.Reflection;
using Command.Application.Abstracts.CommandHandler;
using Command.Application.Abstracts.Infrastructure;
using Command.Infrastructure.Behaviours;
using Command.Infrastructure.EventBus;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Command.Infrastructure;

public static class ServiceRegistrator
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration config)
    {
        serviceCollection.AddScoped<ICommandMediator, CommandMediator.CommandMediator>();
        serviceCollection.AddScoped<IEventBus, MassTransitEventBus>();
        
        
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        serviceCollection.AddMediatR(typeof(CommandHandler), typeof(CommandMediator.CommandMediator));
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehaviour<,>));
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        
        serviceCollection.AddMassTransit(x=>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(config.GetSection("EventBusOptions:HostUrl").Value, host =>
                {
                    host.Username(config.GetSection("EventBusOptions:UserName").Value);
                    host.Password(config.GetSection("EventBusOptions:Password").Value);
                });
            });
        });
        
        return serviceCollection;
    }
}