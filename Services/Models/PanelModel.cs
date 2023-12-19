namespace Services.Models;

public class PanelModel : EntityModel
{
    public string Name { get; set; } = null!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double PowerRating { get; set; }
    public bool Status { get; set; }
    public int InstallationId { get; set; }
}