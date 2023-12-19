namespace Services.Models;

public class PanelReportModel : EntityModel
{
    public double Temperature { get; set; }
    public double Intensity { get; set; }
    public double Voltage { get; set; }
    public double Current { get; set; }
    public bool Status { get; set; }
    public int PanelId { get; set; }
    public int InstallationReportId { get; set; }
}