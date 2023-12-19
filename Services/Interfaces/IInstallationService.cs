using System.Security.Claims;
using FluentResults;
using Services.Interfaces;
using Services.Models;

namespace Services;

public interface IInstallationService : ICrudService<InstallationModel>
{
    Task<Result<InstallationModel>> AddAsync(InstallationModel model, ClaimsPrincipal claimsPrincipal);

    Task<Result> EditAsync(InstallationModel model, ClaimsPrincipal claimsPrincipal);

    Task<PageModel<InstallationModel>> GetAsync(ClaimsPrincipal claimsPrincipal, int page, string query, string name,
        DateOnly? commissioningDate,
        bool? status);
}