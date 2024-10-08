using Basket.API.Data;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

//Add services to container

var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config => {

    config.RegisterServicesFromAssembly(assembly);

    //Add validation behavior to mediator pipeline
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});

builder.Services.AddScoped<IBasketRepository, BasketRepository>(); 
builder.Services.Decorate<IBasketRepository,CachedBasketRepository>();

builder.Services.AddStackExchangeRedisCache(options => {

    options.Configuration = builder.Configuration.GetConnectionString("Redis");

});

//Register validators
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCarter();

builder.Services.AddMarten(options => {

    options.Connection(builder.Configuration.GetConnectionString("Database")!);

    options.Schema.For<ShoppingCart>().Identity(x => x.UserName);
    
}).UseLightweightSessions()
    ;

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

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

app.Run();
