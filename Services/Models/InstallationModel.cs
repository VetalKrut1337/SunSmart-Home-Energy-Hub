namespace Services.Models;

public class InstallationModel : EntityModel
{
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;
    public int Capacity { get; set; }
    public DateOnly CommissioningDate { get; set; }
    public bool Status { get; set; }
    public string UserId { get; set; } = null!;
    public virtual List<PanelModel>? Panels { get; set; }
}