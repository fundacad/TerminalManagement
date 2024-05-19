using System.ComponentModel.DataAnnotations;

namespace Clean.Architecture.Web.Terminals;

public class CreateTerminalRequest
{
  [Required(ErrorMessage = "TerminalId is required")]
  public required string TerminalId { get; set; }

  public int BatchNumber { get; set; }

  public string? Status { get; set; }

  public string? CreatedAt { get; set; }

  public int? MaxStan { get; set; }
}
