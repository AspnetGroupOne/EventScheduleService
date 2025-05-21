using Application.External.Interfaces;
using Application.External.Models;
using Application.External.Response;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Application.External.Services;

public class EventIdValidationService : IEventIdValidationService
{
    // Method made with help from chatgpt.
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;

    public EventIdValidationService(HttpClient httpClient, IOptions<EventSettings> options)
    {
        _httpClient = httpClient;
        _apiUrl = options.Value.Url;
    }

    public async Task<ExternalResponse> EventExistance(string eventId)
    {
        var response = await _httpClient.GetAsync(_apiUrl);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var events = JsonSerializer.Deserialize<List<EventPOCO>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (events == null) { return new ExternalResponse() { StatusCode = 400, Success = false, Message = "Events returned to validation are null." }; }
        if (!events.Any(e => e.Id.ToString() == eventId)) { return new ExternalResponse() { StatusCode = 404, Success = false, Message = "EventId not found among events." }; }

        return new ExternalResponse() { StatusCode = 200, Success = true };
    }

}
