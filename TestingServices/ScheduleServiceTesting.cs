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

    //CREATE
    [Fact]
    public async Task AddScheduleAsync_ShouldReturnTrue_IfValidAddForm()
    {
        //Arrange
        var entity = ServTestData.ValidScheduleAddForm[0];
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
        var entity = ServTestData.InvalidScheduleAddForm[0];
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
        await _scheduleService.AddScheduleAsync(ServTestData.ValidScheduleAddForm[0]);
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
        await _scheduleService.AddScheduleAsync(ServTestData.ValidScheduleAddForm[0]);
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
        await _scheduleService.AddScheduleAsync(ServTestData.ValidScheduleAddForm[0]);
        await _context.SaveChangesAsync();

        //Act
        var result = await _scheduleService.UpdateScheduleAsync(ServTestData.InvalidScheduleUpdateForm[1]);

        //Assert
        Assert.False(result.Success);
    }
    //DELETE
    [Fact]
    public async Task DeleteScheduleAsync_ShouldReturnTrue_IfValidEventId()
    {
        //Arrange
        await _scheduleService.AddScheduleAsync(ServTestData.ValidScheduleAddForm[0]);
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
        await _scheduleService.AddScheduleAsync(ServTestData.ValidScheduleAddForm[0]);
        await _context.SaveChangesAsync();
        var invalidId = "69";

        //Act
        var result = await _scheduleService.DeleteScheduleAsync(invalidId);

        //Assert
        Assert.False(result.Success);
    }
}
