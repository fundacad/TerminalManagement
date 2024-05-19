using Ardalis.Result;
using MediatR;
using UserManagement.Core.Entities;

namespace Clean.Architecture.Web.Terminals;

  public class GetTerminalRequest(string terminalId) : IRequest<Result<GetTerminalResponse>>
  {
  public string TerminalId { get; set; } = terminalId;
}
