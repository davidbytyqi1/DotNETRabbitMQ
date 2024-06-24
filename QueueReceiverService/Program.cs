using MassTransit;
using QueueReceiverService.QueueServices.PublishConsumer;
using QueueReceiverService.QueueServices.RequestResponseConsumer;
using QueueReceiverService.QueueServices.SenderConsumer;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PublisherService>();
    x.AddConsumer<SenderService>();
    x.AddConsumer<RequestResponseService>();

    x.UsingRabbitMq((context, config) =>
    {
   
        config.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        config.ReceiveEndpoint("send-command", e =>
        {
            e.Consumer<SenderService>(context);
        });
        config.ReceiveEndpoint("publish-event", e =>
        {
            e.Consumer<PublisherService>(context);
        });

        config.ReceiveEndpoint("request-response", e =>
        {
            e.Consumer<RequestResponseService>(context);
        });
    });
});

builder.Services.AddMassTransitHostedService();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
