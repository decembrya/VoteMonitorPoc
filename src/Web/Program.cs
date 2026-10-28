using FastEndpoints;
using OpenTelemetry;
using OpenTelemetry.Trace;
using VoteMonitorPoc.Application.Common.Interfaces;
using VoteMonitorPoc.Application.Forms;
using VoteMonitorPoc.Infrastructure.Data;
using Wolverine;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Logging.AddOpenTelemetry(logging =>
{
    logging.IncludeFormattedMessage = true;
    logging.IncludeScopes = true;
});

var dsn = builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"];
using var tracerProvider = Sdk.CreateTracerProviderBuilder()
    .AddSource(builder.Environment.ApplicationName)
    .SetErrorStatusOnException()
    .AddHttpClientInstrumentation()
    .AddAspNetCoreInstrumentation()
    .AddSentryOtlpExporter(dsn)
    .AddConsoleExporter()
    .Build();

SentrySdk.Init(options =>
{
    options.Dsn = dsn;
    options.UseOtlp();
});

builder.AddServiceDefaults();

builder.AddKeyVaultIfConfigured();
builder.AddApplicationServices();
builder.AddInfrastructureServices();
builder.AddWebServices();

builder.Host.UseWolverine(opts =>
{
    opts.Discovery.IncludeAssembly(typeof(FormPublishedEventHandler).Assembly);

    // for lambda DI registrations https://wolverinefx.net/guide/codegen.html
    opts.CodeGeneration.AlwaysUseServiceLocationFor<IApplicationDbContext>();

    Console.WriteLine(opts.DescribeHandlerMatch(typeof(FormPublishedEventHandler)));
});
builder.Services.AddFastEndpoints();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors(static builder =>
    builder.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin());

app.UseFileServer();

app.MapOpenApi();
app.UseFastEndpoints();

app.UseExceptionHandler(options => { });

app.MapDefaultEndpoints();
app.MapEndpoints(typeof(Program).Assembly);


app.Run();
