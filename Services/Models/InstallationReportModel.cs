namespace Services.Models;

public class InstallationReportModel : EntityModel
{
    public DateTime Timestamp { get; set; }
    public double TiltAngle { get; set; }
    public double Efficiency { get; set; }
    public int InstallationId { get; set; }
    public virtual List<PanelReportModel>? PanelReports { get; set; }
}