// Data/Repositories/TerminalProfileRepository.cs
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Core.Entities;
using UserManagement.Core.Interfaces;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Data.Repositories;

  public class TerminalProfileRepository(dbContext context) : ITerminalProfileRepository
  {
      private readonly dbContext _context = context;

  public async Task<TerminalProfile> GetByIdAsync(string terminalId)
{
    var terminal = await _context.TerminalProfiles.FirstOrDefaultAsync(t => t.TerminalId == terminalId) ?? throw new InvalidOperationException($"Terminal with ID '{terminalId}' not found.");
    return terminal;
}


      public async Task CreateAsync(TerminalProfile terminalProfile)
      {
          _context.TerminalProfiles.Add(terminalProfile);
          await _context.SaveChangesAsync();
      }

      public async Task UpdateAsync(TerminalProfile terminalProfile)
      {
          _context.TerminalProfiles.Update(terminalProfile);
          await _context.SaveChangesAsync();
      }

      public async Task DeleteAsync(string terminalId)
      {
          var terminal = await GetByIdAsync(terminalId);
          if (terminal != null)
          {
              _context.TerminalProfiles.Remove(terminal);
              await _context.SaveChangesAsync();
          }
      }
  }
