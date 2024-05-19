using Ardalis.Result;
using MediatR;

namespace UserManagement.UseCases.TerminalUseCases.Delete;

  public record DeleteTerminalCommand(string TerminalId) : IRequest<Result<bool>>;
