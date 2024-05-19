using FluentValidation;

namespace Clean.Architecture.Web.Terminals;

  public class DeleteTerminalValidator : AbstractValidator<GetTerminalRequest>
  {
      public DeleteTerminalValidator()
      {
        RuleFor(x => x.TerminalId).NotEmpty().WithMessage("Terminal ID is required.");
      }
  }
