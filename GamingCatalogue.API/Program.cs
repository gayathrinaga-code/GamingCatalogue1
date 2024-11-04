using GamingCatalogue.API;
using GamingCatalogue.Infrastructure;
using GamingCatalogue.Services;
using GamingCatalogue.Services.Interface;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();


builder.Host.UseSerilog();

// cors enabled
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAllOrigin",
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:4200")
                                  .AllowAnyHeader()
                                  .AllowAnyMethod();
                      });
});

// Add services to the container.
builder.Services.AddInfraConfigServices(builder.Configuration);
builder.Services.AddScoped<IGameDetailService, GameDetailService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Register the middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
