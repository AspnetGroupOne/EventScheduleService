using Application.Domain.Forms;
using Application.External.Interfaces;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Documentation_Swagger;
using Presentation.Extensions.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Controllers;

[UseApiKey]
[Consumes("application/json")]
[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class SchedulesController(IScheduleService scheduleService, IEventIdValidationService eventIdValidationService) : ControllerBase
{
    private readonly IScheduleService _scheduleService = scheduleService;
    private readonly IEventIdValidationService _eventValidation = eventIdValidationService;

    [HttpPost]
    [SwaggerOperation(Summary = "Adds an event schedule.")]
    [SwaggerResponse(200, "Added the specific event schedule.")]
    [SwaggerResponse(400, "The addscheduleform was either containing invalid or missing properties.")]
    [SwaggerRequestExample(typeof(AddScheduleForm), typeof(AddScheduleForm_Example))]
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
    [SwaggerOperation(Summary = "Updates an event schedule.")]
    [SwaggerResponse(200, "Updated the specific event schedule.")]
    [SwaggerResponse(400, "The updatescheduleform was either containing invalid or missing properties.")]
    [SwaggerRequestExample(typeof(UpdateScheduleForm), typeof(UpdateScheduleForm_Example))]
    public async Task<IActionResult> Update(UpdateScheduleForm updateForm)
    {
        if (!ModelState.IsValid) { return BadRequest(); }

        var result = await _scheduleService.UpdateScheduleAsync(updateForm);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Gets a specific events schedule.")]
    [SwaggerResponse(200, "Returns the specific events schedule.")]
    [SwaggerResponse(400, "The id which you sent was null.")]
    //[SwaggerResponseExample]
    public async Task<IActionResult> Get(string id)
    {
        if (id == null) { return BadRequest(); }

        var result = await _scheduleService.GetScheduleAsync(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Deletes a specific events schedule.")]
    [SwaggerResponse(200, "Event schedule was successfully deleted.")]
    [SwaggerResponse(400, "The id which you sent was null.")]
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null) { return BadRequest(); }

        var result = await _scheduleService.DeleteScheduleAsync(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
