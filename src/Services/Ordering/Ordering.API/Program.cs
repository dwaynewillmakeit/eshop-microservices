using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//Add services to container

builder.Services
    .AddAplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration); ;

var app = builder.Build();

// Configure HTTP request pipeline


app.Run();
