using BuberDinner.Application;
using BuberDinner.infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using BuberDinner.Application.services.Authintication;
using BuberDinner.infrastructure.Authintication;
using BuberDinner.Application.Command.interfaces;
using BuberDinner.infrastructure.Services;
using BuberDinner.Application.Command.service;



var builder = WebApplication.CreateBuilder(args);

//use for di and add services to the container.
{
    builder.Services.AddControllers();
    builder.Services.AddScoped<IAuthinticationService, AuthinticationService>();
    builder.Services.AddSingleton<IJwtGenerator, JwtTokenGenerator>();
    builder.Services.AddSingleton<IdataTimeProvider, dataTimeProvider>();
    builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection(JwtSetting.SectionName)); // operation to bind the JwtSetting class with the values from appsettings.json file

}


//path request pipeline configuration
//Middleware configuration
var app = builder.Build();
{
    
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
