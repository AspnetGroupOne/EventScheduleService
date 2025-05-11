using Application.Contexts;
using Application.Data.Repositories;
using Application.Domain.Forms;
using Application.Interfaces;
using Application.Internal.Services;
using Microsoft.EntityFrameworkCore;

namespace TestingServices;

public class ScheduleServiceTesting
{
    private readonly DataContext _context;
    private readonly IScheduleService _scheduleService;
    private readonly IScheduleRepository _scheduleRepository;

    public ScheduleServiceTesting()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new DataContext(options);
        _scheduleRepository = new ScheduleRepository(_context);
        _scheduleService = new ScheduleService(_scheduleRepository);
        _context.Database.EnsureCreated();
    }

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
    //CREATE
    [Fact]
    public async Task AddScheduleAsync_ShouldReturnTrue_IfValidAddForm()
    {
        //Arrange
        var entity = ValidScheduleAddForm[0];
        await _context.SaveChangesAsync();

        //Act
        var result = await _scheduleService.AddScheduleAsync(entity);

        //Assert
        Assert.True(result.Success);
    }
    [Fact]
    public async Task AddScheduleAsync_ShouldReturnFalse_IfInvalidAddForm()
    {
        //Arrange
        var entity = InvalidScheduleAddForm[0];
        await _context.SaveChangesAsync();

        //Act
        var result = await _scheduleService.AddScheduleAsync(entity);

        //Assert
        Assert.False(result.Success);
    }

    //READ
    [Fact]
    public async Task GetScheduleAsync_ShouldReturnTrue_IfValidEventId()
    {
        //Arrange
        await _scheduleService.AddScheduleAsync(ValidScheduleAddForm[0]);
        await _context.SaveChangesAsync();
        var validScheduleId = "1";

        //Act
        var result = await _scheduleService.GetScheduleAsync(validScheduleId);

        //Assert
        Assert.True(result.Success);
    }
    [Fact]
    public async Task GetScheduleAsync_ShouldReturnFalse_IfInvalidEventId()
    {
        //Arrange
        await _scheduleService.AddScheduleAsync(ValidScheduleAddForm[0]);
        await _context.SaveChangesAsync();
        var invalidScheduleId = "6";

        //Act
        var result = await _scheduleService.GetScheduleAsync(invalidScheduleId);

        //Assert
        Assert.False(result.Success);
    }
    //UPDATE
    [Fact]
    public async Task UpdateScheduleAsync_ShouldReturnFalse_IfInvalidUpdateForm()
    {
        //Arrange
        await _scheduleService.AddScheduleAsync(ValidScheduleAddForm[0]);
        await _context.SaveChangesAsync();

        //Act
        var result = await _scheduleService.UpdateScheduleAsync(InvalidScheduleUpdateForm[1]);

        //Assert
        Assert.False(result.Success);
    }
    //DELETE
    [Fact]
    public async Task DeleteScheduleAsync_ShouldReturnTrue_IfValidEventId()
    {
        //Arrange
        await _scheduleService.AddScheduleAsync(ValidScheduleAddForm[0]);
        await _context.SaveChangesAsync();
        var validId = "1";

        //Act
        var result = await _scheduleService.DeleteScheduleAsync(validId);

        //Assert
        Assert.True(result.Success);
    }
    [Fact]
    public async Task DeleteScheduleAsync_ShouldReturnFalse_IfInvalidEventId()
    {
        //Arrange
        await _scheduleService.AddScheduleAsync(ValidScheduleAddForm[0]);
        await _context.SaveChangesAsync();
        var invalidId = "69";

        //Act
        var result = await _scheduleService.DeleteScheduleAsync(invalidId);

        //Assert
        Assert.False(result.Success);
    }
}
