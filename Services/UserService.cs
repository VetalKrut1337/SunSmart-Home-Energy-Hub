using System.Security.Claims;
using System.Text.Encodings.Web;
using DataBase;
using DataBase.Entities;
using FluentResults;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Services.Constants;
using Services.Interfaces;
using Services.Models;
using Services.Options;

namespace Services;

public class UserService : IUserService
{
    private readonly PaginationOptions _configuration;
    private readonly ApplicationDbContext _context;
    private readonly IEmailSender _emailSender;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;


    public UserService(UserManager<User> userManager,
        SignInManager<User> signInManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<PaginationOptions> configuration,
        ApplicationDbContext context,
        IOptions<AdminOptions> adminConfig,
        IEmailSender emailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        InitializeAsync(userManager, roleManager, adminConfig.Value).Wait();
        _emailSender = emailService;
        _configuration = configuration.Value;
    }

    public async Task<Result> ChangePasswordAsync(string oldPassword, string newPassword, ClaimsPrincipal principal)
    {
        var user = await _userManager.GetUserAsync(principal);
        var result = await _userManager.ChangePasswordAsync(user!, oldPassword, newPassword);
        return HandleResult(result);
    }

    public async Task<Result> ConfirmEmailAsync(string id, string code)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
            return Result.Fail(Errors.NotFound);
        var result = await _userManager.ConfirmEmailAsync(user, code);
        return HandleResult(result);
    }

    public async Task<Result> ForgotPasswordAsync(string email, string callbackUrl)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null || !await _userManager.IsEmailConfirmedAsync(user)) return Result.Fail(Errors.NotFound);
        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        callbackUrl += $"?email={email}&code={UrlEncoder.Default.Encode(code)}";
        _ = _emailSender.SendEmailAsync(email, EmailTemplates.ForgotPassword, callbackUrl);
        return Result.Ok();
    }

    public async Task<Result> ResetPasswordAsync(string email, string password, string token)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            return Result.Fail(Errors.NotFound);
        var result = await _userManager.ResetPasswordAsync(user, token, password);
        return HandleResult(result);
    }

    public async Task<Result> DeleteAsync(string id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
            return Result.Fail(Errors.NotFound);
        var result = await _userManager.DeleteAsync(user);
        return HandleResult(result);
    }


    public async Task<Result> LoginAsync(string email, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, true, false);
        if (result.IsNotAllowed)
            return Result.Fail(Errors.Forbidden);
        return result.Succeeded ? Result.Ok() : Result.Fail(Errors.InvalidCredentials);
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<Result<UserModel>> RegisterAsync(RegisterModel model, string callbackUrl)
    {
        var user = model.Adapt<User>();
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded) return HandleResult(result);
        result = await _userManager.AddToRoleAsync(user, Roles.User);
        if (!result.Succeeded) return HandleResult(result);
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        callbackUrl += $"?id={user.Id}&code={UrlEncoder.Default.Encode(code)}";
        _ = _emailSender.SendEmailAsync(model.Email, EmailTemplates.Register, callbackUrl);
        return Result.Ok(user.Adapt<UserModel>());
    }

    public async Task<UserModel?> GetUser(ClaimsPrincipal principal)
    {
        var user = await _userManager.GetUserAsync(principal);
        return user!.Adapt<UserModel?>();
    }

    public async Task<string?> GetRole(ClaimsPrincipal principal)
    {
        var user = await _userManager.GetUserAsync(principal);
        var roles = await _userManager.GetRolesAsync(user!);
        return roles.FirstOrDefault();
    }

    public string? GetUserId(ClaimsPrincipal principal)
    {
        return _userManager.GetUserId(principal);
    }

    public async Task<PageModel<UserModel>> GetUsers(int page, string query)
    {
        var users = _context.Set<User>().Where(x => x.UserName!.Contains(query) || x.Email!.Contains(query));
        var itemsPerPage = Convert.ToInt32(_configuration.ItemsPerPage);
        var entities = await users.OrderByDescending(x => x.UserName)
            .Skip((page - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .ToListAsync();
        var models = entities.Adapt<List<UserModel>>();

        return new PageModel<UserModel>(models, users.Count());
    }

    private static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
        AdminOptions configuration)
    {
        var adminEmail = configuration.Email;
        var adminPassword = configuration.Password;
        await AddRole(roleManager, Roles.Admin);
        await AddRole(roleManager, Roles.User);
        if (await userManager.FindByEmailAsync(adminEmail) is null)
        {
            var admin = new User
            {
                Email = adminEmail,
                UserName = adminEmail
            };
            var result = await userManager.CreateAsync(admin, adminPassword);
            if (result.Succeeded)
            {
                var token = await userManager.GenerateEmailConfirmationTokenAsync(admin);
                await userManager.ConfirmEmailAsync(admin, token);
                await userManager.AddToRoleAsync(admin, Roles.Admin);
            }
        }

        static async Task AddRole(RoleManager<IdentityRole> roleManager, string role)
        {
            if (await roleManager.FindByNameAsync(role) is null)
                await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    private static Result HandleResult(IdentityResult result)
    {
        return result.Succeeded
            ? Result.Ok()
            : Result.Fail(result.Errors.Select(e => e.Description));
    }
}