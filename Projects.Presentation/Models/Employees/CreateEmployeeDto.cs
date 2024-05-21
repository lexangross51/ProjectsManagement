using System.ComponentModel.DataAnnotations;

namespace Projects.Presentation.Models.Employees;

public class CreateEmployeeDto
{
    [MaxLength(256)]
    public string FirstName { get; init; } = string.Empty;
    
    [MaxLength(256)]
    public string MiddleName { get; init; } = string.Empty;
    
    [MaxLength(256)]
    public string LastName { get; init; } = string.Empty;

    [EmailAddress]
    public string Mail { get; init; } = string.Empty;
}