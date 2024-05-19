using Ardalis.Result;
using Ardalis.SharedKernel;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Core.Entities;
using UserManagement.Core.Interfaces;

namespace UserManagement.UseCases.TerminalUseCases.Create;

  public class CreateTerminalHandler : IRequestHandler<CreateTerminalCommand, Result<string>>
  {
      private readonly ITerminalProfileRepository _terminalRepository;

      public CreateTerminalHandler(ITerminalProfileRepository terminalRepository)
      {
          _terminalRepository = terminalRepository;
      }

      public async Task<Result<string>> Handle(CreateTerminalCommand request, CancellationToken cancellationToken)
      {
          DateTime createdAt;
          if (!DateTime.TryParse(request.CreatedAt, out createdAt))
          {
              createdAt = DateTime.Now; 
          }

          TerminalStatus status;
          try
          {
              status = (TerminalStatus)Enum.Parse(typeof(TerminalStatus), request.Status ?? "Active");
          }
          catch (ArgumentException)
          {
              status = TerminalStatus.Active;
          }

          var newTerminal = new TerminalProfile(
              request.TerminalId,
              request.BatchNumber ?? 1,
              status,
              createdAt,
              request.MaxStan ?? 1
          );

          await _terminalRepository.CreateAsync(newTerminal);

          return Result<string>.Success(newTerminal.TerminalId);
      }
  }
