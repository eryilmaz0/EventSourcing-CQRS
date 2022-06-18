using FirstQueryProject.Abstract.Projection;
using FirstQueryProject.Abstract.Repository;
using FirstQueryProject.Consumer;
using FirstQueryProject.Projection;
using FirstQueryProject.ReadModel;
using FirstQueryProject.Repository;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IProjectionBuilder<Course>, CourseProjectionBuilder>();
builder.Services.AddSingleton(typeof(IRepository<Course>), typeof(Repository<Course>));


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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();