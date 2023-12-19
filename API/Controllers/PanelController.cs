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
public class PanelController : CrudController<PanelModel, PanelRequest>
{
    public PanelController(IPanelService service) : base(service)
    {
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<PageModel<PanelModel>> GetAsync(double? powerRating,
        bool? status, int page = 1, string query = "", string name = "")
    {
        var panelService = service as IPanelService;
        return await panelService!.GetAsync(page, query, name, powerRating, status);
    }
}