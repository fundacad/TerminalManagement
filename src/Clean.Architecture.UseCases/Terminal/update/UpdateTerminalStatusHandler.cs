using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.SharedKernel;
using MediatR;
using UserManagement.Core.Entities;
using UserManagement.Core.Interfaces;
using UserManagement.UseCases.TerminalUseCases.update;

namespace UserManagement.UseCases.TerminalUseCases.Update;

  public class UpdateTerminalStatusHandler : IRequestHandler<UpdateTerminalStatusCommand, Result<TerminalDTO>>
  {
      private readonly ITerminalProfileRepository _terminalRepository;

      public UpdateTerminalStatusHandler(ITerminalProfileRepository terminalRepository)
      {
          _terminalRepository = terminalRepository;
      }

      public async Task<Result<TerminalDTO>> Handle(UpdateTerminalStatusCommand request, CancellationToken cancellationToken)
      {
          try
          {
              var terminalProfile = await _terminalRepository.GetByIdAsync(request.TerminalId);
              if (terminalProfile == null)
              {
                  return Result<TerminalDTO>.NotFound($"Terminal with ID '{request.TerminalId}' not found.");
              }

             terminalProfile.UpdateStatus(terminalProfile.Status == TerminalStatus.Active ? TerminalStatus.Inactive : TerminalStatus.Active);

              await _terminalRepository.UpdateAsync(terminalProfile);

              var terminalDto = new TerminalDTO(
                  terminalProfile.TerminalId,
                  terminalProfile.BatchNumber,
                  terminalProfile.Status.ToString(),
                  terminalProfile.CreatedAt.ToString(),
                  terminalProfile.MaxStan
              );

              return Result<TerminalDTO>.Success(terminalDto);
          }
          catch (Exception ex)
          {
              return Result<TerminalDTO>.Error(ex.Message);
          }
      }
  }
