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

    [DataType(DataType.Password)] 
    public string Password { get; init; } = string.Empty;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; init; } = string.Empty;

    public string Role { get; init; } = string.Empty;
}