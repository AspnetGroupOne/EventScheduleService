using Application.Domain.Forms;

namespace TestingServices;

public class ServTestData
{

    public static readonly AddScheduleForm[] ValidScheduleAddForm =
    {
        new AddScheduleForm
        {
            EventId = "1",
            GateOpenStart = "1000",
            GateOpenEnd = "1100",
            PreShowStart = "1200",
            PreShowEnd = "1300",
            CeremonyStart = "1400",
            CeremonyEnd = "1600",
            ConcertStart = "1700"
        },
        new AddScheduleForm
        {
            EventId = "2",
            GateOpenStart = "1000",
            GateOpenEnd = "1100",
            PreShowStart = "1200",
            PreShowEnd = "1300",
            CeremonyStart = "1400",
            CeremonyEnd = "1600",
            ConcertStart = "1700"
        },
    };
    public static readonly AddScheduleForm[] InvalidScheduleAddForm =
    {
        new AddScheduleForm
        {
            EventId = "1"
        },
        new AddScheduleForm
        {
            EventId = ""
        },
    };

    public static readonly UpdateScheduleForm[] ValidScheduleUpdateForm =
    {
        new UpdateScheduleForm
        {
            EventId = "1",
            GateOpenStart = "1337",
            GateOpenEnd = "1100",
            PreShowStart = "1200",
            PreShowEnd = "1300",
            CeremonyStart = "1400",
            CeremonyEnd = "1600",
            ConcertStart = "1700"
        },
        new UpdateScheduleForm
        {
            EventId = "2",
            GateOpenStart = "1337",
            GateOpenEnd = "1100",
            PreShowStart = "1200",
            PreShowEnd = "1300",
            CeremonyStart = "1400",
            CeremonyEnd = "1600",
            ConcertStart = "1700"
        },
    };
    public static readonly UpdateScheduleForm[] InvalidScheduleUpdateForm =
    {
        new UpdateScheduleForm
        {
            EventId = "1"
        },
        new UpdateScheduleForm
        {
            EventId = ""
        },
    };



}
