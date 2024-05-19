using Ardalis.Result;
using FastEndpoints;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.UseCases.TerminalUseCases.Get;

namespace Clean.Architecture.Web.Terminals;

  public class GetById : Endpoint<GetTerminalRequest, GetTerminalResponse>
  {
      private readonly IMediator _mediator;

      public GetById(IMediator mediator)
      {
          _mediator = mediator;
      }

      public override void Configure()
      {
          Get("/Terminals/{TerminalId}");
          AllowAnonymous();
          Summary(s => s.Summary = "Get Terminal by TerminalId");
      }

      public override async Task HandleAsync(GetTerminalRequest request, CancellationToken cancellationToken)
      {
          var query = new GetTerminalQuery(request.TerminalId);
          var result = await _mediator.Send(query, cancellationToken);

          if (result.IsSuccess)
          {
              var terminalDTO = result.Value;
              var response = new GetTerminalResponse(terminalDTO);
              await SendOkAsync(response, cancellation: cancellationToken);
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
