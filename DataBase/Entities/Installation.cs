namespace DataBase.Entities;

public class Installation : Entity
{
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;
    public int Capacity { get; set; }
    public DateOnly CommissioningDate { get; set; }
    public bool Status { get; set; }

    public virtual User User { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public virtual List<InstallationReport>? InstallationReports { get; set; }
    public virtual List<Panel>? Panels { get; set; }
}