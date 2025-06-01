using Application.Domain.Forms;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation_Swagger;

public class AddScheduleForm_Example : IExamplesProvider<AddScheduleForm>
{
    public AddScheduleForm GetExamples() => new()
    {
        EventId = "56f58514-7581-4b18-97f5-b6eb5ba7b9c9",
        GateOpenStart = "0900",
        GateOpenEnd = "1000",
        PreShowStart = "1100",
        PreShowEnd = "1200",
        CeremonyStart = "1300",
        CeremonyEnd = "1400",
        ConcertStart = "1500",
    };
}
