using FluentValidation;

namespace Projects.Logic.Projects.Commands.UpdateProject;

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(cmd => cmd.Id).NotNull().NotEqual(Guid.Empty);
        RuleFor(cmd => cmd.ProjectName).NotNull().MaximumLength(256);
        RuleFor(cmd => cmd.CompanyCustomer).NotNull().MaximumLength(256);
        RuleFor(cmd => cmd.CompanyExecutor).NotNull().MaximumLength(256);
        RuleFor(cmd => cmd.Priority).NotNull();
        RuleFor(cmd => cmd.DateStart).NotNull();
        RuleFor(cmd => cmd.DateEnd).NotNull();
    }
}