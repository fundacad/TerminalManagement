using UserManagement.Core.Entities;

namespace UserManagement.Core.Interfaces;

  public interface ITerminalManagementService
  {
      Task<TerminalProfile> GetTerminalProfileAsync(string terminalId);
      Task UpdateTerminalStatusAsync(string terminalId, string newStatus);
      Task CreateTerminalAsync(TerminalProfile terminalProfile);
      Task DeleteTerminalAsync(string terminalId);
  }
