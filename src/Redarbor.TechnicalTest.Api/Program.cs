using Redarbor.TechnicalTest.Api;
using Redarbor.TechnicalTest.Application;
using Redarbor.TechnicalTest.Infrastructure;
using Redarbor.TechnicalTest.Infrastructure.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddWebServices(builder.Configuration);

var app = builder.Build();

app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();
