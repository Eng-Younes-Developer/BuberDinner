using BuberDinner.APi.Middleware;
using BuberDinner.Application.Authintication.Commands.Register;
using BuberDinner.Application.Authintication.ResponseAuth;
using BuberDinner.Application.Command.Behavior;
using BuberDinner.Application.Command.interfaces;
using BuberDinner.Application.Command.Persistence;
using BuberDinner.Application.Command.service;
using BuberDinner.infrastructure.Authintication;
using BuberDinner.infrastructure.Persistence;
using BuberDinner.infrastructure.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//use for di and add services to the container.
{
    builder.Services.AddControllers();
    builder.Services.AddSingleton<IJwtGenerator, JwtTokenGenerator>();
    builder.Services.AddSingleton<IdataTimeProvider, dataTimeProvider>();
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

    var jwtSetting = builder.Configuration.GetSection("JwtSetting").Get<JwtSetting>(); // operation to bind the JwtSetting class with the values from appsettings.json file 
    builder.Services.AddSingleton(Options.Create(jwtSetting));
    builder.Services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSetting.Issuer,
            ValidAudience = jwtSetting.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Secret))
        });


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
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}