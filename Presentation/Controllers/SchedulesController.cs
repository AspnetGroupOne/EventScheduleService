using Application.Domain.Forms;
using Application.External.Interfaces;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SchedulesController(IScheduleService scheduleService, IEventIdValidationService eventIdValidationService) : ControllerBase
{
    private readonly IScheduleService _scheduleService = scheduleService;
    private readonly IEventIdValidationService _eventValidation = eventIdValidationService;

    [HttpPost]
    public async Task<IActionResult> Create(AddScheduleForm addForm)
    {
        if (!ModelState.IsValid) { return BadRequest(); }

        // This validation is only needed when adding data if im not thinking wrong.
        var eventValidationResult = await _eventValidation.EventExistance(addForm.EventId);
        if (eventValidationResult.Success == false) { return BadRequest(eventValidationResult.Message); }

        var result = await _scheduleService.AddScheduleAsync(addForm);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateScheduleForm updateForm)
    {
        if (!ModelState.IsValid) { return BadRequest(); }

        var result = await _scheduleService.UpdateScheduleAsync(updateForm);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        if (id == null) { return BadRequest(); }

        var result = await _scheduleService.GetScheduleAsync(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null) { return BadRequest(); }

        var result = await _scheduleService.DeleteScheduleAsync(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
