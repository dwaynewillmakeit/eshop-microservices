


using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

//Add services to  container

var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config => {

    config.RegisterServicesFromAssembly(assembly);

    //Add validation behavior to mediator pipeline
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});

//Register validators
builder.Services. AddValidatorsFromAssembly(assembly);


builder.Services.AddMarten(
    options => { 
        options.Connection(builder.Configuration.GetConnectionString("Database")!); 
    })
    .UseLightweightSessions()    
    ;

if (builder.Environment.IsDevelopment()) {

    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddExceptionHandler<CustomExceptionHandler>() ;

builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCarter();



var app = builder.Build();


//Configure the HTTP request pipeline

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Carter API V1");
    c.RoutePrefix = string.Empty;  // Set the Swagger UI at the app's root
});

app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health",new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions {

    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse 
});


app.Run();
