using System.ComponentModel.DataAnnotations;

namespace Application.Domain.Forms;

public class UpdateScheduleForm
{
    [Required]
    public string EventId { get; set; } = null!;

    [Required]
    public string GateOpenStart { get; set; } = null!;

    [Required]
    public string GateOpenEnd { get; set; } = null!;

    public string? PreShowStart { get; set; }
    public string? PreShowEnd { get; set; }
    public string? CeremonyStart { get; set; }
    public string? CeremonyEnd { get; set; }

    [Required]
    public string ConcertStart { get; set; } = null!;
}
