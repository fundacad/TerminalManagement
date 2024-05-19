using UserManagement.Core.Entities;

namespace UserManagement.Core.Interfaces;

  public interface ITerminalProfileRepository
  {
      Task<TerminalProfile> GetByIdAsync(string terminalId);
      Task CreateAsync(TerminalProfile terminalProfile);
      Task UpdateAsync(TerminalProfile terminalProfile);
      Task DeleteAsync(string terminalId);
  }
