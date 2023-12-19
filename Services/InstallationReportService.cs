using System.Linq.Expressions;
using DataBase;
using DataBase.Entities;
using Microsoft.AspNetCore.Identity;
using Services.Models;

namespace Services;

public class InstallationReportService : CrudService<InstallationReportModel, InstallationReport>,
    IInstallationReportService
{
    public InstallationReportService(ApplicationDbContext context, UserManager<User> userManager) : base(context)
    {
    }

    public async Task<PageModel<InstallationReportModel>> GetAsync(int page, string query, double? tiltAngle,
        double? effiency)
    {
        var expressions = new List<Expression<Func<InstallationReport, bool>>>();

        if (tiltAngle is not null) expressions.Add(x => tiltAngle.Equals(x.TiltAngle));
        if (effiency is not null) expressions.Add(x => effiency.Equals(x.Efficiency));

        return await base.GetAsync(page,
            x => x.Id,
            false,
            expressions.ToArray()
        );
    }
}