using API.Requests;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Models;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
public class InstallationController : CrudController<InstallationModel, InstallationRequest>
{
    private readonly IIoTService _ioTService;
    public InstallationController(IInstallationService service, IIoTService ioTService) : base(service)
    {
        _ioTService = ioTService;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<PageModel<InstallationModel>> GetAsync(DateOnly? commissioningDate,
        bool? status, int page = 1, string name = "", string query = "")
    {
        var installationService = service as IInstallationService;
        return await installationService!.GetAsync(User, page, query, name, commissioningDate, status);
    }

    public override async Task<ActionResult<InstallationModel>> AddAsync(InstallationRequest request)
    {
        var installationService = service as IInstallationService;
        var model = request.Adapt<InstallationModel>();
        var result = await installationService!.AddAsync(model, User);
        return HandleCreatedResult(result);
    }

    public override async Task<IActionResult> EditAsync(int id, [FromBody] InstallationRequest request)
    {
        var installationService = service as IInstallationService;
        var model = request.Adapt<InstallationModel>();
        model.Id = id;
        var result = await installationService!.EditAsync(model, User);
        return HandleResult(result);
    }

    [HttpPatch("[action]")]
    public void On(int id) => _ioTService.On(id);
    [HttpPatch("[action]")]
    public void Off(int id) => _ioTService.Off(id);
    
}