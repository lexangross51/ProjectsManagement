using FluentValidation;
using Projects.Logic.Employees.Queries.GetEmployee;

namespace Projects.Logic.Projects.Queries.GetProject;

public class GetProjectQueryValidator : AbstractValidator<GetEmployeeQuery>
{
    public GetProjectQueryValidator() 
        => RuleFor(cmd => cmd.Id).NotNull().NotEqual(Guid.Empty);
}