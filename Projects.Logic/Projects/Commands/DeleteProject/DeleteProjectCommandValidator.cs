using FluentValidation;

namespace Projects.Logic.Projects.Commands.DeleteProject;

public class DeleteProjectCommandValidator : AbstractValidator<DeleteProjectCommand>
{
    public DeleteProjectCommandValidator() 
        => RuleFor(cmd => cmd.Id).NotNull().NotEqual(Guid.Empty);
}