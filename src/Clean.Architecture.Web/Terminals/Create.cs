using FastEndpoints;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.UseCases.TerminalUseCases.Create;

namespace Clean.Architecture.Web.Terminals;

  public class CreateTerminal(IMediator mediator) : Endpoint<CreateTerminalRequest, CreateTerminalResponse>
  {
      private readonly IMediator _mediator = mediator;

  public override void Configure()
      {
          Post("/Terminals");
          AllowAnonymous();
          Summary(s => s.Summary = "Create a new terminal");
      }

      public override async Task HandleAsync(CreateTerminalRequest request, CancellationToken cancellationToken)
      {
          var result = await _mediator.Send(new CreateTerminalCommand(request.TerminalId, request.CreatedAt,request.BatchNumber,request.MaxStan,request.Status), cancellationToken);

          if (result.IsSuccess)
          {
              Response = new CreateTerminalResponse(request.TerminalId);
          }
          else
          {
              // Handle error cases
          }
      }
  }
