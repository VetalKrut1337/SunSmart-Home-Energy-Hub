using System.ComponentModel.DataAnnotations;

namespace API.Requests;

public class InstallationRequest : IRequestBody
{
    [Required] public string Name { get; set; } = null!;

    [Required] public string Location { get; set; } = null!;

    [Required] public int Capacity { get; set; }

    [Required] public DateOnly CommissioningDate { get; set; }

    [Required] public bool Status { get; set; }
}