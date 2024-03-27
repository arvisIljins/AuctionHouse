using MassTransit;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMassTransit(x => 
{
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("notification", false));
    
    x.UsingRabbitMq((context, cfg) => 
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host => 
        {
            host.Username(builder.Configuration.GetValue("RabbitMq:UserName", ""));
            host.Password(builder.Configuration.GetValue("RabbitMq:Password", ""));
        });
        cfg.ConfigureEndpoints(context);
    });
});
var app = builder.Build();


app.Run();
