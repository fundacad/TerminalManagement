
using Ardalis.Result;
using Ardalis.SharedKernel;
using MediatR;

namespace UserManagement.UseCases.TerminalUseCases.Get;

  public record GetTerminalQuery(string TerminalId) : IQuery<Result<TerminalDTO>>;
