using BuberDinner.Application;
using BuberDinner.infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using BuberDinner.Application.services.Authintication;

var builder = WebApplication.CreateBuilder(args);

//use for di and add services to the container.
{
    builder.Services.AddControllers();
    builder.Services.AddScoped<IAuthinticationService, AuthinticationService>();
    
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
