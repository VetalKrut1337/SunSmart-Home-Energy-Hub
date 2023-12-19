using API.Requests.User;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Constants;
using Services.Interfaces;
using Services.Models;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : BaseController
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
    {
        var result = await userService.LoginAsync(request.Email, request.Password);
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<UserModel>> RegisterAsync(RegisterRequest request, string callbackUrl)
    {
        var model = request.Adapt<RegisterModel>();
        var result = await userService.RegisterAsync(model, callbackUrl);
        return HandleCreatedResult(result);
    }

    [HttpDelete]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async void LogoutAsync()
    {
        await userService.LogoutAsync();
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserModel?>> InfoAsync()
    {
        return await userService.GetUser(User);
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<string?>> RoleAsync()
    {
        return await userService.GetRole(User);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ConfirmEmailAsync(string id, string code)
    {
        var result = await userService.ConfirmEmailAsync(id, code);
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        var result = await userService.DeleteAsync(id);
        return HandleResult(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest model, string callbackUrl)
    {
        var result = await userService.ForgotPasswordAsync(model.Email, callbackUrl);
        return result.IsSuccess ? Ok() : NotFound();
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest model)
    {
        var result = await userService.ResetPasswordAsync(model.Email, model.Password, model.Code);
        return HandleResult(result);
    }

    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<PageModel<UserModel>>> GetUsers(int page = 1, string query = "")
    {
        return await userService.GetUsers(page, query);
    }
}