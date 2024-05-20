using FluentValidation;

namespace Projects.Logic.Employees.Queries.GetEmployee;

public class GetEmployeeQueryValidator : AbstractValidator<GetEmployeeQuery>
{
    public GetEmployeeQueryValidator() 
        => RuleFor(cmd => cmd.Id).NotNull().NotEqual(Guid.Empty);
}