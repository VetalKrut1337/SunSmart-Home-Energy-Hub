using Services.Interfaces;
using Services.Models;

namespace Services;

public interface IPanelReportService : ICrudService<PanelReportModel>
{
    Task<PageModel<PanelReportModel>> GetAsync(int page, string query, double? voltage, bool? status);
}