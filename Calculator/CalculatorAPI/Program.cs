using CalcApplication;
using CalcInfrastruture;
using CalcService;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenTelemetry().WithTracing(builder => builder
    .AddAspNetCoreInstrumentation()
    .AddSource("Calculator-API")
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Calculator-API"))
    .AddZipkinExporter(options =>
    {
        options.Endpoint = new Uri("http://zipkin:9411/api/v2/spans");
    }));


// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithEnvironmentUserName()
    .WriteTo.Seq("http://seq:5341")
    .WriteTo.Console()
    .CreateLogger();

// Add Serilog to the container
builder.Host.UseSerilog();

builder.Services.AddScoped<ICalculationService, CalculationService>();
builder.Services.AddScoped<ICalculationRepository, CalculationRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSerilogRequestLogging();

app.UseCors("AllowAnyOrigin");

app.MapControllers();

app.Run();
