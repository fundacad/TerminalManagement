using FluentValidation;

namespace Clean.Architecture.Web.Terminals;

  public class GetTerminalValidator : AbstractValidator<GetTerminalRequest>
  {
      public GetTerminalValidator()
      {
          RuleFor(x => x.TerminalId).NotEmpty().WithMessage("Terminal ID is required.");
      }
  }
