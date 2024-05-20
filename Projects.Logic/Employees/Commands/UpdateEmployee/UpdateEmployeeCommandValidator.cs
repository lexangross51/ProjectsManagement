using FluentValidation;

namespace Projects.Logic.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(cmd => cmd.Id).NotNull().NotEqual(Guid.Empty);
        RuleFor(cmd => cmd.FirstName).NotNull().MaximumLength(256);
        RuleFor(cmd => cmd.MiddleName).NotNull().MaximumLength(256);
        RuleFor(cmd => cmd.LastName).NotNull().MaximumLength(256);
        RuleFor(cmd => cmd.Mail).NotNull()
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
    }
}