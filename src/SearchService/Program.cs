using System.Net;
using MassTransit;
using Polly;
using Polly.Extensions.Http;
using SearchService.Consumers;
using SearchService.Data;
using SearchService.Repositories.ItemSearchRepository;
using SearchService.Service.SearchItemService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IItemSearchRepository, ItemSearchRepository>();
builder.Services.AddScoped<IItemSearchService, ItemSearchService>();
builder.Services.AddHttpClient<AuctionServiceHttpClient>().AddPolicyHandler(GetPolicy());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMassTransit(x => 
{
    x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();
    x.AddConsumersFromNamespaceContaining<AuctionUpdateConsumer>();

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));
    
    x.UsingRabbitMq((context, cfg) => 
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host => 
        {
            host.Username(builder.Configuration.GetValue("RabbitMq:UserName", ""));
            host.Password(builder.Configuration.GetValue("RabbitMq:Password", ""));
        });
        cfg.ReceiveEndpoint("search-auction-created", e => 
        {
            e.UseMessageRetry(r => r.Interval(5, 5));
            e.ConfigureConsumer<AuctionCreatedConsumer>(context);
            e.ConfigureConsumer<AuctionUpdateConsumer>(context);
        });
        cfg.ConfigureEndpoints(context);
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:6001") // Allow only the gateway origin
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseCors("AllowSpecificOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Lifetime.ApplicationStarted.Register(async () => {
    try
    {
        await DBInitializer.InitDb(app);
    } 
    catch (Exception e)
    {
        Console.WriteLine(e);
    };
});

app.Run();

static IAsyncPolicy<HttpResponseMessage> GetPolicy() 
=> HttpPolicyExtensions
.HandleTransientHttpError()
.OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
.WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(3));