namespace UserManagement.Core.Entities;

  public class TerminalProfile(string terminalId, int batchNumber, TerminalStatus status, DateTime createdAt, int? maxStan)
{
  public string TerminalId { get; private set; } = terminalId;
  public int BatchNumber { get; private set; } = batchNumber;
  public TerminalStatus Status { get; private set; } = status;
  public DateTime CreatedAt { get; private set; } = createdAt;
  public int? MaxStan { get; private set; } = maxStan;

  public void UpdateStatus(TerminalStatus newStatus)
      {
          Status = newStatus;
      }
  }
