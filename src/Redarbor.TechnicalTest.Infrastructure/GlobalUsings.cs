global using System.Data;
global using Dapper;
global using Microsoft.Data.SqlClient;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Diagnostics;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;

global using Redarbor.TechnicalTest.Application.Interfaces.Data;
global using Redarbor.TechnicalTest.Application.Interfaces.Factories;
global using Redarbor.TechnicalTest.Domain.Interfaces.Repositories;
global using Redarbor.TechnicalTest.Domain.Models;
global using Redarbor.TechnicalTest.Domain.ValueObjects;
global using Redarbor.TechnicalTest.Infrastructure.Persistence;
global using Redarbor.TechnicalTest.Infrastructure.Persistence.Configurations.DapperHandlers;
global using Redarbor.TechnicalTest.Infrastructure.Persistence.Factories;
global using Redarbor.TechnicalTest.Infrastructure.Persistence.Interceptors;
global using Redarbor.TechnicalTest.Infrastructure.Repositories;
