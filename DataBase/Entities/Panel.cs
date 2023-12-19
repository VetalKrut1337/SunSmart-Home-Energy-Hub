namespace DataBase.Entities;

public class Panel : Entity
{
    public string Name { get; set; } = null!;
    public double MaxTiltAngle { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double PowerRating { get; set; }
    public bool Status { get; set; }

    public virtual Installation Installation { get; set; } = null!;
    public int InstallationId { get; set; }
    public virtual List<PanelReport>? PanelReports { get; set; }
}