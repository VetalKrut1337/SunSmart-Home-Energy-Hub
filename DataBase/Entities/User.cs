using Microsoft.AspNetCore.Identity;

namespace DataBase.Entities;

public class User : IdentityUser
{
    public string Name { get; set; } = null!;
    public virtual List<Installation>? Installations { get; set; }
}