using Basket.API.Data;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Discount.Grpc;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

//Add services to container

//Application Services
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{

    config.RegisterServicesFromAssembly(assembly);

    //Add validation behavior to mediator pipeline
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddCarter();


//Data Services
builder.Services.AddMarten(options =>
{

    options.Connection(builder.Configuration.GetConnectionString("Database")!);

    options.Schema.For<ShoppingCart>().Identity(x => x.UserName);

}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{

    options.Configuration = builder.Configuration.GetConnectionString("Redis");

});

//Grpc Services

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(
    options => {

        options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);


    }).ConfigurePrimaryHttpMessageHandler(() => {

        if(builder.Environment.IsProduction())
            return new HttpClientHandler();

        var handler = new HttpClientHandler {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };


        return handler;
    
    });

//Cross Cutting Services

//Register validators
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!)
    ;



var app = builder.Build();


//Configure HTTP Request pipeline

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Carter API V1");
    c.RoutePrefix = string.Empty;  // Set the Swagger UI at the app's root
});

app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{

    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


app.Run();
