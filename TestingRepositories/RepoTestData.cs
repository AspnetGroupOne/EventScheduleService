using Application.Domain.Entities;

namespace TestingRepositories;

public class RepoTestData
{
    public static readonly ScheduleEntity[] ValidScheduleEntities =
    {
        new ScheduleEntity
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
        new ScheduleEntity
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
    public static readonly ScheduleEntity[] InvalidScheduleEntities =
    {
        new ScheduleEntity
        {
            EventId = "1"
        },
        new ScheduleEntity
        {
            EventId = ""
        },
    };



}
