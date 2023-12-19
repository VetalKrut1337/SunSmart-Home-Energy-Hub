namespace Services.Options;

public class AdminOptions
{
    public const string Section = "Admin";
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
}