using System.ComponentModel.DataAnnotations;
using Services.Constants;

namespace API.Requests.User;

public class RegisterRequest
{
    [Required] public string Name { get; set; } = null!;

    [Required] [EmailAddress] public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = Errors.PasswordAreNotTheSame)]
    public string ConfirmPassword { get; set; } = null!;
}