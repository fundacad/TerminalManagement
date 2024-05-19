using Clean.Architecture.Infrastructure.Data; // Replace with your namespace if different
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Core.Entities;
using UserManagement.Infrastructure.Data;

namespace Clean.Architecture.Infrastructure.Data;

  public static class SeedData
  {
      public static void Initialize(IServiceProvider serviceProvider)
      {
    using var tdbContext = new dbContext(
        serviceProvider.GetRequiredService<DbContextOptions<dbContext>>());
    if (!tdbContext.TerminalProfiles.Any()) // Only seed if no data exists (optional)
    {
      PopulateTestData(tdbContext);
    }
  }

      public static void PopulateTestData(dbContext dbContext)
      {
          // Clear existing data (optional)
          // dbContext.TerminalProfiles.RemoveRange(dbContext.TerminalProfiles);
        dbContext.TerminalProfiles.Add(new TerminalProfile
        (
            "T1234",
            45,
            TerminalStatus.Active,
            DateTime.Now, 
            2563
        ));

        dbContext.TerminalProfiles.Add(new TerminalProfile
        (
            "T5678",
            69,
            TerminalStatus.Inactive,
            DateTime.Now,
            3574
        ));


          dbContext.SaveChanges();
      }
  }
