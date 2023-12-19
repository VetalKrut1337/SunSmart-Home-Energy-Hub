using API.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Models;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
public abstract class PanelReportController : CrudController<PanelReportModel, PanelReportRequest>
{
    public PanelReportController(IPanelReportService service) : base(service)
    {
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<PageModel<PanelReportModel>> GetAsync(double? voltage, bool? status, int page = 1,
        string query = "")
    {
        var panelReportService = service as IPanelReportService;
        return await panelReportService!.GetAsync(page, query, voltage, status);
    }
}