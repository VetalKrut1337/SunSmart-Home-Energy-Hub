using System.Security.Claims;
using FluentResults;
using Services.Models;

namespace Services.Interfaces;

public interface IUserService
{
    Task<Result> ChangePasswordAsync(string oldPassword, string newPassword, ClaimsPrincipal principal);
    Task<Result> ConfirmEmailAsync(string id, string code);
    Task<Result> DeleteAsync(string id);
    Task<Result> ForgotPasswordAsync(string email, string callbackUrl);
    Task<string?> GetRole(ClaimsPrincipal principal);
    Task<UserModel?> GetUser(ClaimsPrincipal principal);
    string? GetUserId(ClaimsPrincipal principal);
    Task<Result> LoginAsync(string email, string password);
    Task LogoutAsync();
    Task<Result> ResetPasswordAsync(string email, string password, string token);
    Task<PageModel<UserModel>> GetUsers(int page, string query);
    Task<Result<UserModel>> RegisterAsync(RegisterModel model, string callbackUrl);
}