using API.Errors;
using API.Middleware;
using CoreData.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
// builder.Services.AddDbContext<StoreContext>(opt=>{
//     opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
// });

// builder.Services.AddScoped<IProductRepoistory,ProductRepository>();
// builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// builder.Services.Configure<ApiBehaviorOptions>(options =>{
//     options.InvalidModelStateResponseFactory = actionContext =>{
//         var errors = actionContext.ModelState
//         .Where(x=>x.Value.Errors.Count>0)
//         .SelectMany(x=>x.Value.Errors)
//         .Select(x=>x.ErrorMessage).ToArray();
//         var errorResponse = new ApiValidationErrorResponse
//         {
//             Errors = errors
//         };
//         return new BadRequestObjectResult(errorResponse);
//     };
// });

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithRedirects("/errors/{0}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast")
// .WithOpenApi();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    
    logger.LogError(ex,"An error occured during migration");
}


app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
