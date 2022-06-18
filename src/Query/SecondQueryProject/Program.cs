using MassTransit;
using SecondQueryProject.Abstract.Projection;
using SecondQueryProject.Abstract.Repository;
using SecondQueryProject.Consumer;
using SecondQueryProject.Projection;
using SecondQueryProject.ReadModel;
using SecondQueryProject.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MongoContext>();
builder.Services.AddSingleton<IProjectionBuilder<Course>, CourseProjectionBuilder>();
builder.Services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));


builder.Services.AddMassTransit(config=>
{
    config.AddConsumer<EventConsumer>();
    
    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration.GetSection("EventBusOptions:HostUrl").Value, host =>
        {
            host.Username(builder.Configuration.GetSection("EventBusOptions:UserName").Value);
            host.Password(builder.Configuration.GetSection("EventBusOptions:Password").Value);
        });
        
        cfg.ReceiveEndpoint(builder.Configuration.GetSection("EventBusOptions:QueueName").Value, consumer =>
        {
            consumer.ConfigureConsumer<EventConsumer>(context);
        });
    });
});
        
builder.Services.AddMassTransitHostedService();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();