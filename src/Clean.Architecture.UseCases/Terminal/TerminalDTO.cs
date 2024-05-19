namespace UserManagement.UseCases.TerminalUseCases;

  // Define a DTO for representing terminal data
  public record TerminalDTO(string TerminalId, int BatchNumber, string Status, string CreatedAt, int? MaxStan);
