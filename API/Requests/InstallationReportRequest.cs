using System.ComponentModel.DataAnnotations;

namespace API.Requests;

public class InstallationReportRequest : IRequestBody
{
    [Required] public DateTime Timestamp { get; set; }

    [Required] public double TiltAngle { get; set; }

    [Required] public double Efficiency { get; set; }

    [Required] public int InstallationId { get; set; }

    [Required] public virtual List<PanelReportRequest>? PanelReports { get; set; }
}