
using UserManagement.Core.Entities;
using UserManagement.Core.Interfaces;

namespace UserManagement.Core.Services;

  public class TerminalManagementService : ITerminalManagementService
  {
      private readonly ITerminalProfileRepository _repository;

      public TerminalManagementService(ITerminalProfileRepository repository)
      {
          _repository = repository;
      }

      public async Task<TerminalProfile> GetTerminalProfileAsync(string terminalId)
      {
          return await _repository.GetByIdAsync(terminalId);
      }

      public async Task UpdateTerminalStatusAsync(string terminalId, TerminalStatus newStatus)
      {
          var terminal = await _repository.GetByIdAsync(terminalId);
          if (terminal == null)
              throw new Exception("Terminal not found");

          terminal.UpdateStatus(newStatus);

          await _repository.UpdateAsync(terminal);
      }

      public async Task CreateTerminalAsync(TerminalProfile terminalProfile)
      {
          // Additional validation logic can be added here
          await _repository.CreateAsync(terminalProfile);
      }

      public async Task DeleteTerminalAsync(string terminalId)
      {
          await _repository.DeleteAsync(terminalId);
      }

    public Task UpdateTerminalStatusAsync(string terminalId, string newStatus)
    {
        throw new NotImplementedException();
    }
}
