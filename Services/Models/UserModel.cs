﻿namespace Services.Models;

public class UserModel
{
    public string Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}