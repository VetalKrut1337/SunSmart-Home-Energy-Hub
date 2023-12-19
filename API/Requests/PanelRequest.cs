using System.ComponentModel.DataAnnotations;

namespace API.Requests;

public class PanelRequest : IRequestBody
{
    [Required] public string Name { get; set; } = null!;

    [Required] public double MaxTiltAngle { get; set; }

    [Required] public double Latitude { get; set; }

    [Required] public double Longitude { get; set; }

    [Required] public double PowerRating { get; set; }

    [Required] public bool Status { get; set; }

    [Required] public int InstallationId { get; set; }
}