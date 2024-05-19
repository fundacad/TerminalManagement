namespace Clean.Architecture.Web.Terminals;

  public record TerminalRecord(string TerminalId, int BatchNumber, string Status, string CreatedAt, int? MaxStan);
