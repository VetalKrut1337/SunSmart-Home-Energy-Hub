using System.Linq.Expressions;
using DataBase;
using DataBase.Entities;
using Microsoft.AspNetCore.Identity;
using Services.Models;

namespace Services;

public class PanelService : CrudService<PanelModel, Panel>, IPanelService
{
    private readonly UserManager<User> _userManager;

    public PanelService(ApplicationDbContext context, UserManager<User> userManager) : base(context)
    {
        _userManager = userManager;
    }

    public async Task<PageModel<PanelModel>> GetAsync(int page, string query, string name, double? powerRating,
        bool? status)
    {
        var expressions = new List<Expression<Func<Panel, bool>>>
        {
            x => x.Name.Contains(name)
        };
        if (powerRating is not null) expressions.Add(x => powerRating.Equals(x.PowerRating));

        if (status is not null) expressions.Add(x => status.Equals(x.Status));

        return await base.GetAsync(page,
            x => x.Id,
            false,
            expressions.ToArray()
        );
    }
}