using Redarbor.TechnicalTest.Api;
using Redarbor.TechnicalTest.Application;
using Redarbor.TechnicalTest.Infrastructure;
using Redarbor.TechnicalTest.Infrastructure.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();
