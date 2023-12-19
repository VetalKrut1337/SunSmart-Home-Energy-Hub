using System.ComponentModel.DataAnnotations;

namespace API.Requests;

public class PanelReportRequest : IRequestBody
{
    [Required] public double Temperature { get; set; }

    [Required] public double Intensity { get; set; }

    [Required] public double Voltage { get; set; }

    [Required] public double Current { get; set; }

    [Required] public bool Status { get; set; }

    [Required] public int PanelId { get; set; }
}