using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Core.Entities;

namespace UserManagement.Infrastructure.Data.Configurations;

  public class TerminalProfileConfiguration : IEntityTypeConfiguration<TerminalProfile>
  {
      public void Configure(EntityTypeBuilder<TerminalProfile> builder)
      {
          builder.HasKey(e => e.TerminalId);

          builder.Property(e => e.TerminalId)
              .HasMaxLength(8)
              .IsRequired();

      }
  }
