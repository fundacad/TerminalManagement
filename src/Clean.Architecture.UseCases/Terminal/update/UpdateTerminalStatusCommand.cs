
using Ardalis.Result;
using MediatR;

namespace UserManagement.UseCases.TerminalUseCases.update;
    public record UpdateTerminalStatusCommand(string TerminalId) : IRequest<Result<TerminalDTO>>;
