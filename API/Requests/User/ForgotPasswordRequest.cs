using System.ComponentModel.DataAnnotations;

namespace API.Requests.User;

public class ForgotPasswordRequest
{
    [EmailAddress] [Required] public string Email { get; set; } = null!;
}