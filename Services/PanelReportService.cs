using System.Linq.Expressions;
using DataBase;
using DataBase.Entities;
using Microsoft.AspNetCore.Identity;
using Services.Models;

namespace Services;

public class PanelReportService : CrudService<PanelReportModel, PanelReport>, IPanelReportService
{
    public PanelReportService(ApplicationDbContext context, UserManager<User> userManager) : base(context)
    {
    }

    public async Task<PageModel<PanelReportModel>> GetAsync(int page, string query, double? voltage, bool? status)
    {
        var expressions = new List<Expression<Func<PanelReport, bool>>>();

        if (voltage is not null) expressions.Add(x => voltage.Equals(x.Voltage));
        if (status is not null) expressions.Add(x => status.Equals(x.Status));

        return await base.GetAsync(page,
            x => x.Id,
            false,
            expressions.ToArray()
        );
    }
}