namespace DataBase.Entities;

public class InstallationReport : Entity
{
    public virtual Installation Installation { get; set; } = null!;
    public int InstallationId { get; set; }
    public DateTime Timestamp { get; set; }
    public double TiltAngle { get; set; }
    public double Efficiency { get; set; }

    public virtual List<PanelReport>? PanelReports { get; set; }
}