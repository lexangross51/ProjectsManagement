using FluentValidation;

namespace Projects.Logic.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeeCommandValidator()
        => RuleFor(cmd => cmd.Id).NotNull().NotEqual(Guid.Empty);
}