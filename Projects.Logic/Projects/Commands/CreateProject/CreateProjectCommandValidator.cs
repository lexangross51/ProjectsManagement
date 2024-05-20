using FluentValidation;

namespace Projects.Logic.Projects.Commands.CreateProject;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(cmd => cmd.ProjectName).NotNull().MaximumLength(256);
        RuleFor(cmd => cmd.CompanyCustomer).NotNull().MaximumLength(256);
        RuleFor(cmd => cmd.CompanyExecutor).NotNull().MaximumLength(256);
        RuleFor(cmd => cmd.Priority).NotNull();
        RuleFor(cmd => cmd.DateStart).NotNull();
        RuleFor(cmd => cmd.DateEnd).NotNull();
    }
}