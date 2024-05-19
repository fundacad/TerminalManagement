using Ardalis.Result;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.UseCases.TerminalUseCases.Delete;

namespace Clean.Architecture.Web.Terminals;

  public class DeleteTerminalEndpoint : Endpoint<DeleteTerminalCommand>
  {
      private readonly IMediator _mediator;

      public DeleteTerminalEndpoint(IMediator mediator)
      {
          _mediator = mediator;
      }

      public override void Configure()
      {
          Delete("/Terminals/{TerminalId}");
          AllowAnonymous();
          Summary(s => s.Summary = "Delete Terminal by TerminalId");
      }

      public override async Task HandleAsync(DeleteTerminalCommand request, CancellationToken cancellationToken)
      {
          var command = new DeleteTerminalCommand(request.TerminalId);
          var result = await _mediator.Send(command, cancellationToken);

          if (result.IsSuccess)
          {
              await SendOkAsync(cancellation: cancellationToken);
          }
          else if (result.Status == ResultStatus.NotFound)
          {
              await SendNotFoundAsync(cancellation: cancellationToken);
          }
          else
          {
              await SendErrorsAsync(400, cancellationToken);
          }
      }
  }
