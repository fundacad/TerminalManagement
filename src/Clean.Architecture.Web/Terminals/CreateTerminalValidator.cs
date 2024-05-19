using FluentValidation;
using UserManagement.Core.Entities;

namespace Clean.Architecture.Web.Terminals;

  public class CreateTerminalValidator : AbstractValidator<CreateTerminalRequest>
  {
      public CreateTerminalValidator()
      {
          RuleFor(x => x.TerminalId).NotEmpty();
          RuleFor(x => x.BatchNumber);
          RuleFor(x => x.Status)
              .Must(x => x == null || Enum.TryParse(typeof(TerminalStatus), x, out _))
              .WithMessage("Invalid TerminalStatus value. Must be a valid enum value or null.");
          RuleFor(x => x.CreatedAt);
      }
  }
