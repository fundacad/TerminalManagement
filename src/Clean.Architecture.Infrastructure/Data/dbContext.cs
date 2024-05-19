using Microsoft.EntityFrameworkCore;
using UserManagement.Core.Entities;

namespace UserManagement.Infrastructure.Data;

  public class dbContext(DbContextOptions<dbContext> options) : DbContext(options)
  {
  public DbSet<TerminalProfile> TerminalProfiles { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
          // Configure TerminalProfile entity
          modelBuilder.Entity<TerminalProfile>(entity =>
          {
              // Primary key
              entity.HasKey(e => e.TerminalId);

              // Configure properties
              entity.Property(e => e.TerminalId)
                  .HasMaxLength(8)
                  .IsRequired();

              entity.Property(e => e.BatchNumber)
                  .IsRequired();

              entity.Property(e => e.Status)
                  .IsRequired()
                  .HasDefaultValue(TerminalStatus.Active);

              entity.Property(e => e.CreatedAt)
                  .IsRequired();

              entity.Property(e => e.MaxStan)
                  .IsRequired();
          });
      }
  }
