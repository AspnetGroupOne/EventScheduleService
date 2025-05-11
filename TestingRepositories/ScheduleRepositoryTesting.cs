using Application.Contexts;
using Application.Data.Repositories;
using Application.Domain.Entities;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TestingRepositories;

public class ScheduleRepositoryTesting
{
    private readonly DataContext _context;
    private readonly IScheduleRepository _scheduleRepository;

    public ScheduleRepositoryTesting()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new DataContext(options);
        _scheduleRepository = new ScheduleRepository(_context);
        _context.Database.EnsureCreated();
    }

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

    //CREATE
    [Fact]
    public async Task CreateAsync_ShouldReturnTrue_IfValidEntity()
    {
        //Arrange
        var entity = ValidScheduleEntities[0];
        await _context.SaveChangesAsync();

        //Act
        var result = await _scheduleRepository.CreateAsync(entity);

        //Assert
        Assert.True(result.Success);
    }
    [Fact]
    public async Task CreateAsync_ShouldReturnFalse_IfInvalidEntity()
    {
        //Arrange
        var entity = InvalidScheduleEntities[0];
        await _context.SaveChangesAsync();

        //Act
        var result = await _scheduleRepository.CreateAsync(entity);

        //Assert
        Assert.False(result.Success);
    }


    //READ
    [Fact]
    public async Task ExistsAsync_ShouldReturnTrue_IfValidPredicate()
    {
        //Arrange
        _context.Schedules.AddRange(ValidScheduleEntities);
        await _context.SaveChangesAsync();
        var validScheduleId = "1";

        //Act
        var result = await _scheduleRepository.ExistsAsync(schedule => schedule.EventId == validScheduleId);

        //Assert
        Assert.True(result.Success);
    }
    [Fact]
    public async Task ExistsAsync_ShouldReturnFalse_IfInvalidPredicate()
    {
        //Arrange
        _context.Schedules.AddRange(ValidScheduleEntities);
        await _context.SaveChangesAsync();
        var inValidScheduleId = "";

        //Act
        var result = await _scheduleRepository.ExistsAsync(schedule => schedule.EventId == inValidScheduleId);

        //Assert
        Assert.False(result.Success);
    }


    [Fact]
    public async Task GetAsync_ShouldReturnTrue_IfValidPredicate()
    {
        //Arrange
        _context.Schedules.AddRange(ValidScheduleEntities);
        await _context.SaveChangesAsync();
        var validScheduleId = "1";

        //Act
        var result = await _scheduleRepository.GetAsync(entity => entity.EventId == validScheduleId);

        //Assert
        Assert.True(result.Success);
    }
    [Fact]
    public async Task GetAsync_ShouldReturnFalse_IfInvalidPredicate()
    {
        //Arrange
        _context.Schedules.AddRange(ValidScheduleEntities);
        await _context.SaveChangesAsync();
        var validScheduleId = "8585";

        //Act
        var result = await _scheduleRepository.GetAsync(entity => entity.EventId == validScheduleId);

        //Assert
        Assert.False(result.Success);
    }


    //UPDATE
    [Fact]
    public async Task UpdateAsync_ShouldReturnTrue_IfValidEntity()
    {
        //Arrange
        _context.Schedules.AddRange(ValidScheduleEntities);
        await _context.SaveChangesAsync();
        var validScheduleId = "1";

        //Act
        var entity = await _scheduleRepository.GetAsync(entity => entity.EventId == validScheduleId);
        var updateEntity = entity.Content;
        updateEntity.PreShowStart = "1030";

        var result = await _scheduleRepository.UpdateAsync(updateEntity);

        //Assert
        Assert.True(result.Success);
    }
    [Fact]
    public async Task UpdateAsync_ShouldReturnFalse_IfInvalidEntity()
    {
        //Arrange
        _context.Schedules.AddRange(ValidScheduleEntities);
        await _context.SaveChangesAsync();
        var inValidEntity = new ScheduleEntity
        {
            EventId = "5",
            GateOpenStart = "1100",
            GateOpenEnd = "1100",
            PreShowStart = "1200",
            PreShowEnd = "1300",
            CeremonyStart = "1400",
            CeremonyEnd = "1600",
            ConcertStart = "1700"
        };

        //Act
        var result = await _scheduleRepository.UpdateAsync(inValidEntity);

        //Assert
        Assert.False(result.Success);
    }


    //DELETE
    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_IfValidEntity()
    {
        //Arrange
        _context.Schedules.AddRange(ValidScheduleEntities);
        await _context.SaveChangesAsync();
        var validEntity = ValidScheduleEntities[0];

        //Act
        var result = await _scheduleRepository.DeleteAsync(validEntity);

        //Assert
        Assert.True(result.Success);
    }
    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_IfInvalidEntity()
    {
        //Arrange
        _context.Schedules.AddRange(ValidScheduleEntities);
        await _context.SaveChangesAsync();
        var invalidEntity = InvalidScheduleEntities[1];

        //Act
        var result = await _scheduleRepository.DeleteAsync(invalidEntity);

        //Assert
        Assert.False(result.Success);
    }
}
