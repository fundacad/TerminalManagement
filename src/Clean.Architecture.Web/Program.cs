using System.Configuration;
using System.Reflection;
using Ardalis.ListStartupServices;
using Ardalis.SharedKernel;
using Clean.Architecture.Infrastructure.Data;
using FastEndpoints;
using FastEndpoints.Swagger;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Extensions.Logging;
using UserManagement.Core.Entities;
using UserManagement.Core.Interfaces;
using UserManagement.Infrastructure.Data;
using UserManagement.Infrastructure.Data.Repositories;
using UserManagement.UseCases.TerminalUseCases.Create;
using UserManagement.UseCases.TerminalUseCases.Delete;
using UserManagement.UseCases.TerminalUseCases.Get;
using UserManagement.UseCases.TerminalUseCases.update;
using UserManagement.UseCases.TerminalUseCases.Update;

var logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .CreateLogger();

logger.Information("Starting web host");

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));
var microsoftLogger = new SerilogLoggerFactory(logger)
    .CreateLogger<Program>();


// Configure Web Behavior
builder.Services.Configure<CookiePolicyOptions>(options =>
{
  options.CheckConsentNeeded = context => true;
  options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddFastEndpoints()
  .SwaggerDocument(o =>
  {
    o.ShortSchemaNames = true;
  });

ConfigureServices(builder.Services,builder.Configuration);

ConfigureMediatR();


if (builder.Environment.IsDevelopment())
{
  // Use a local test email server
  // See: https://ardalis.com/configuring-a-local-test-email-server/

  builder.Services.AddScoped<ITerminalProfileRepository, TerminalProfileRepository>();

  // Otherwise use this:
  //builder.Services.AddScoped<IEmailSender, FakeEmailSender>();
  AddShowAllServicesSupport();
}
else
{
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseShowAllServicesMiddleware(); // see https://github.com/ardalis/AspNetCoreStartupServices
}
else
{
  app.UseDefaultExceptionHandler(); // from FastEndpoints
  app.UseHsts();
}

app.UseFastEndpoints()
  .UseSwaggerGen(); // Includes AddFileServer and static files middleware

app.UseHttpsRedirection();

SeedDatabase(app);

app.Run();

static void SeedDatabase(WebApplication app)
{
  using var scope = app.Services.CreateScope();
  var services = scope.ServiceProvider;

  try
  {
    var context = services.GetRequiredService<dbContext>();
    // You can choose to migrate or ensure created based on your needs
    // context.Database.Migrate();
    context.Database.EnsureCreated();
    SeedData.Initialize(services);
  }
  catch (Exception ex)
  {
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
  }
}

void ConfigureMediatR()
{
  var mediatRAssemblies = new[]
  {
    Assembly.GetAssembly(typeof(TerminalProfile)), // Core
    Assembly.GetAssembly(typeof(CreateTerminalCommand)), // UseCase
    Assembly.GetAssembly(typeof(CreateTerminalHandler)), // UseCase Handler
    Assembly.GetAssembly(typeof(UpdateTerminalStatusCommand)), // UseCase
    Assembly.GetAssembly(typeof(UpdateTerminalStatusHandler)), // UseCase Handler
    Assembly.GetAssembly(typeof(DeleteTerminalCommand)), //UseCase
    Assembly.GetAssembly(typeof(DeleteTerminalHandler)), // UseCase Handler
    Assembly.GetAssembly(typeof(GetTerminalQuery)), // UseCase
    Assembly.GetAssembly(typeof(GetTerminalHandler)), // UseCase Handler
    Assembly.GetAssembly(typeof(ITerminalProfileRepository)),
  };
  builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));
  builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
  builder.Services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();
}

void AddShowAllServicesSupport()
{
  // add list services for diagnostic purposes
}

void ConfigureServices(IServiceCollection services,IConfiguration configuration)
{
    // Add DbContext
    services.AddDbContext<dbContext>(options =>
        options.UseSqlite(configuration.GetConnectionString("SqliteConnection")));

    // Add other services
    services.AddScoped<ITerminalProfileRepository, TerminalProfileRepository>();

    // Add controllers and other services
    services.AddControllers();
}