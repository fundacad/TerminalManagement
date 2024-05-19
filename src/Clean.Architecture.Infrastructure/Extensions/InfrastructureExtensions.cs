
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Core.Interfaces;
using UserManagement.Infrastructure.Data;
using UserManagement.Infrastructure.Data.Repositories;

namespace UserManagement.Infrastructure.Extensions;

  public static class InfrastructureExtensions
  {
      public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
      {
          services.AddScoped<ITerminalProfileRepository, TerminalProfileRepository>();
          
          services.AddDbContext<dbContext>(options =>
            options.UseSqlite(connectionString));

          return services;
      }
  }