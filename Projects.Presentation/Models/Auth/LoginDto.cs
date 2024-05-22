using System.ComponentModel.DataAnnotations;

namespace Projects.Presentation.Models.Auth;

public class LoginDto
{
    [Required]
    public string UserName { get; init; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; init; } = string.Empty;

    public bool RememberMe { get; init; }
}