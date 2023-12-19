using API.Requests;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Models;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
public class InstallationReportController : BaseController
{
    private readonly IInstallationReportService service;

    public InstallationReportController(IInstallationReportService service)
    {
        this.service = service;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<PageModel<InstallationReportModel>> GetAsync(double? tiltAngle, double? efficiency, int page = 1,
        string query = "")
    {
        return await service!.GetAsync(page, query, tiltAngle, efficiency);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InstallationReportModel>> AddAsync([FromBody] InstallationReportRequest request)
    {
        Console.WriteLine(request.ToString());
        var model = request.Adapt<InstallationReportModel>();
        var result = await service.AddAsync(model);
        return HandleCreatedResult(result);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await service.DeleteAsync(id);
        return HandleResult(result);
    }
}