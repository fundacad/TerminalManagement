using UserManagement.Core.Entities;
using UserManagement.UseCases.TerminalUseCases;

namespace Clean.Architecture.Web.Terminals;

  public record GetTerminalResponse(TerminalDTO Terminal);
