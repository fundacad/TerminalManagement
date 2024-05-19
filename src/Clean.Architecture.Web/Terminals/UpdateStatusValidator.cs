using FluentValidation;

namespace Clean.Architecture.Web.Terminals;

  public class UpdateStatusValidator : AbstractValidator<GetTerminalRequest>
  {
      public UpdateStatusValidator()
      {
        RuleFor(x => x.TerminalId).NotEmpty().WithMessage("Terminal ID is required.");
      }
  }
