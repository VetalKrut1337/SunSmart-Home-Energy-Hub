namespace DataBase.Entities;

public class PanelReport : Entity
{
    public virtual Panel Panel { get; set; } = null!;
    public int PanelId { get; set; }
    public double Temperature { get; set; }
    public double Intensity { get; set; }
    public double Voltage { get; set; }
    public double Current { get; set; }
    public bool Status { get; set; }
    public virtual InstallationReport InstallationReport { get; set; } = null!;
    public int InstallationReportId { get; set; }
}