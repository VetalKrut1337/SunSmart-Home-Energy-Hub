using System.Linq.Expressions;
using System.Security.Claims;
using DataBase;
using DataBase.Entities;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Services.Models;

namespace Services;

public class InstallationService : CrudService<InstallationModel, Installation>, IInstallationService
{
    private readonly UserManager<User> _userManager;

    public InstallationService(ApplicationDbContext context, UserManager<User> userManager) : base(context)
    {
        _userManager = userManager;
    }

    public async Task<PageModel<InstallationModel>> GetAsync(ClaimsPrincipal claimsPrincipal, int page, string query,
        string name, DateOnly? commissioningDate,
        bool? status)
    {
        var userId = _userManager.GetUserId(claimsPrincipal)!;
        var expressions = new List<Expression<Func<Installation, bool>>>
        {
            x => x.UserId == userId && x.Name.Contains(name)
        };
        if (commissioningDate is not null) expressions.Add(x => commissioningDate == x.CommissioningDate);

        if (status is not null) expressions.Add(x => status == x.Status);

        return await base.GetAsync(page,
            x => x.Id,
            false,
            expressions.ToArray()
        );
    }

    public Task<Result<InstallationModel>> AddAsync(InstallationModel model, ClaimsPrincipal claimsPrincipal)
    {
        model.UserId = _userManager.GetUserId(claimsPrincipal)!;
        return base.AddAsync(model);
    }

    public Task<Result> EditAsync(InstallationModel model, ClaimsPrincipal claimsPrincipal)
    {
        model.UserId = _userManager.GetUserId(claimsPrincipal)!;
        return base.EditAsync(model);
    }
}