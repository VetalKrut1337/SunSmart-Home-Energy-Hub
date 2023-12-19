namespace Services.Models;

public class RegisterModel
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string UserName => Email;
    public string Password { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}