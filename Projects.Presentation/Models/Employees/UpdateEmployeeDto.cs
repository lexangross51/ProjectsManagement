using System.ComponentModel.DataAnnotations;

namespace Projects.Presentation.Models.Employees;

public class UpdateEmployeeDto
{
    public Guid Id { get; init; }
    
    [MaxLength(256)]
    public string FirstName { get; init; } = string.Empty;
    
    [MaxLength(256)]
    public string MiddleName { get; init; } = string.Empty;
    
    [MaxLength(256)]
    public string LastName { get; init; } = string.Empty;

    [EmailAddress(ErrorMessage = "The email address is not valid.")]
    public string Mail { get; init; } = string.Empty;
}