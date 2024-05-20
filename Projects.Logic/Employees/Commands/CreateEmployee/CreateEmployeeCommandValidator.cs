using FluentValidation;

namespace Projects.Logic.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(cmd => cmd.FirstName).NotNull().MaximumLength(256)
            .WithMessage("The first name is too long");
        RuleFor(cmd => cmd.MiddleName).NotNull().MaximumLength(256)
            .WithMessage("The middle name is too long");;
        RuleFor(cmd => cmd.LastName).NotNull().MaximumLength(256)
            .WithMessage("The last name is too long");;
        RuleFor(cmd => cmd.Mail).NotNull()
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
            .WithMessage("Mail has the wrong format");
    }
}