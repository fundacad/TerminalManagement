using Ardalis.Result;
using Ardalis.SharedKernel;
using MediatR;
using System;
using System.Threading;
using UserManagement.Core.Entities;

namespace UserManagement.UseCases.TerminalUseCases.Create;

public record CreateTerminalCommand(string TerminalId, string? CreatedAt,int? BatchNumber = 0, int? MaxStan = 0,string? Status = "Active") : IRequest<Result<string>>;
