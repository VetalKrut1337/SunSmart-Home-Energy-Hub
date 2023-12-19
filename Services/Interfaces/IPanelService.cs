using Services.Interfaces;
using Services.Models;

namespace Services;

public interface IPanelService : ICrudService<PanelModel>
{
    Task<PageModel<PanelModel>> GetAsync(int page, string query, string name, double? powerRating,
        bool? status);
}