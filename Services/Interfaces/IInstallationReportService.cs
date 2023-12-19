using Services.Interfaces;
using Services.Models;

namespace Services;

public interface IInstallationReportService : ICrudService<InstallationReportModel>
{
    Task<PageModel<InstallationReportModel>> GetAsync(int page, string query, double? tiltAngle, double? effiency);
}