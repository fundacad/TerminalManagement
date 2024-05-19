using Ardalis.Result;
using FastEndpoints;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.UseCases.TerminalUseCases;
using UserManagement.UseCases.TerminalUseCases.update;
using UserManagement.UseCases.TerminalUseCases.Update;

namespace Clean.Architecture.Web.Terminals;

  public class UpdateTerminalEndpoint : Endpoint<UpdateTerminalStatusCommand, TerminalDTO>
  {
      private readonly IMediator _mediator;

      public UpdateTerminalEndpoint(IMediator mediator)
      {
          _mediator = mediator;
      }

      public override void Configure()
      {
          Put("/Terminals/Update");
          AllowAnonymous();
          Summary(s => s.Summary = "Update Terminal Status by TerminalId");
      }

      public override async Task HandleAsync(UpdateTerminalStatusCommand request, CancellationToken cancellationToken)
      {
          var result = await _mediator.Send(request, cancellationToken);

          if (result.IsSuccess)
          {
              var terminalDTO = result.Value;
              await SendOkAsync(terminalDTO, cancellation: cancellationToken);
          }
          else
          {
              var errorMessage = result.Errors.FirstOrDefault();
              if (result.Status == ResultStatus.NotFound)
              {
                  await SendNotFoundAsync(cancellation: cancellationToken);
              }
              else
              {
                  await SendErrorsAsync(400 , cancellation: cancellationToken);
              }
          }
      }
  }
