using Ardalis.Result;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Core.Interfaces;

namespace UserManagement.UseCases.TerminalUseCases.Delete;

  public class DeleteTerminalHandler(ITerminalProfileRepository terminalRepository) : IRequestHandler<DeleteTerminalCommand, Result<bool>>
  {
      private readonly ITerminalProfileRepository _terminalRepository = terminalRepository;

  public async Task<Result<bool>> Handle(DeleteTerminalCommand request, CancellationToken cancellationToken)
      {
          try
          {
              var terminalProfile = await _terminalRepository.GetByIdAsync(request.TerminalId);
              if (terminalProfile == null)
              {
                  return Result<bool>.NotFound();
              }

              await _terminalRepository.DeleteAsync(request.TerminalId);

              return Result<bool>.Success(true);
          }
          catch (Exception ex)
          {
              return Result<bool>.Error(ex.Message);
          }
      }
  }
