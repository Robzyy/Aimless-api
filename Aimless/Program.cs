using Serilog;
using Serilog.Core;

Log.Logger = ConfigureLogging();

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddSerilog();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync("Something went wrong...");
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
return;

Logger ConfigureLogging()
{
    return new LoggerConfiguration()
        .WriteTo.Console()
        .CreateLogger();
}