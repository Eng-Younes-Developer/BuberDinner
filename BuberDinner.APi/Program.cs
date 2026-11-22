using BuberDinner.APi.Middleware;
using BuberDinner.Application;
using BuberDinner.Application.Authintication.Commands.Register;
using BuberDinner.Application.Authintication.ResponseAuth;
using BuberDinner.Application.Command.Behavior;
using BuberDinner.Application.Command.interfaces;
using BuberDinner.Application.Command.Persistence;
using BuberDinner.Application.Command.service;
using BuberDinner.Application.Authintication;
using BuberDinner.Contracts.Authintication;
using BuberDinner.infrastructure;
using BuberDinner.infrastructure.Authintication;
using BuberDinner.infrastructure.Persistence;
using BuberDinner.infrastructure.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using System.Reflection;
using System.Reflection.Metadata;
using Microsoft.Extensions.Logging.Abstractions;


var builder = WebApplication.CreateBuilder(args);

//use for di and add services to the container.
{
    builder.Services.AddControllers();
    builder.Services.AddSingleton<IJwtGenerator, JwtTokenGenerator>();
    builder.Services.AddSingleton<IdataTimeProvider, dataTimeProvider>();
    builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection(JwtSetting.SectionName)); // operation to bind the JwtSetting class with the values from appsettings.json file
    builder.Services.AddScoped<IUserRepositiory, UserRepositiory>();
    builder.Services.AddScoped<IPipelineBehavior<RegisterCommand, AuthinticationResult>, ValidationBehavior<RegisterCommand, AuthinticationResult>>(); //register the validation behavior for RegisterCommand
    builder.Services.AddValidatorsFromAssemblyContaining<RegisterCommandValidator>();   //register all validators in the assembly

    builder.Services.AddMediatR(x =>
    {
        x.RegisterServicesFromAssemblies(typeof(RegisterCommand).Assembly); //register all handlers in the assembly

    });
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "AdminPortal API",
            Version = "v1"
        });

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http, //type of security scheme is http
            Scheme = "bearer", // method of authentication is bearer token
            BearerFormat = "JWT", //format of the token is JWT
            Description = "Enter your JWT access token"
        });

        options.AddSecurityRequirement(document =>
            new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", document)] = []
            });
    });
}


//path request pipeline configuration
//Middleware configuration
var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();

        app.UseSwaggerUI();
    }

    app.UseMiddleware<ErrorHandlingMiddleware>(); //must be the first middleware in the pipeline to catch exceptions from other middlewares and controllers
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
