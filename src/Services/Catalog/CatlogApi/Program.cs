

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
    .UseLightweightSessions() ;

builder.Services.AddExceptionHandler<CustomExceptionHandler>() ;

builder.Services.AddCarter();



var app = builder.Build();


//Configure the HTTP request pipeline

app.MapCarter();

app.UseExceptionHandler(options => { });


app.Run();
