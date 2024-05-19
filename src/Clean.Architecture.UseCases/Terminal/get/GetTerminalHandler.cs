using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;
using UserManagement.Core.Entities;
using UserManagement.Core.Interfaces;

namespace UserManagement.UseCases.TerminalUseCases.Get;

public class GetTerminalHandler : IQueryHandler<GetTerminalQuery, Result<TerminalDTO>>
{
    private readonly ITerminalProfileRepository _terminalRepository;
    private readonly ILogger<GetTerminalHandler> _logger;

    public GetTerminalHandler(ITerminalProfileRepository terminalRepository, ILogger<GetTerminalHandler> logger)
    {
        _terminalRepository = terminalRepository;
        _logger = logger;
    }

    public async Task<Result<TerminalDTO>> Handle(GetTerminalQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var terminalProfile = await _terminalRepository.GetByIdAsync(request.TerminalId);
            
            if (terminalProfile == null)
            {
                return Result<TerminalDTO>.NotFound($"Terminal with ID {request.TerminalId} not found.");
            }

            var terminalDto = new TerminalDTO(
                terminalProfile.TerminalId,
                terminalProfile.BatchNumber,
                terminalProfile.Status.ToString(),
                terminalProfile.CreatedAt.ToString(),
                terminalProfile.MaxStan
            );

            return Result<TerminalDTO>.Success(terminalDto);
        }
        catch (InvalidOperationException ex)
        {
            // Specific exception for not found
            _logger.LogError(ex, "Terminal not found: {TerminalId}", request.TerminalId);
            return Result<TerminalDTO>.NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            // General exception handling
            _logger.LogError(ex, "An error occurred while retrieving terminal: {TerminalId}", request.TerminalId);
            return Result<TerminalDTO>.Error("An unexpected error occurred. Please try again later.");
        }
    }
}
