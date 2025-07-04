using cp_journey_backend.Api;
using cp_journey_backend.Repositories;
using cp_journey_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace cp_journey_backend.Controllers;

[ApiController]
[Route("api/events")]
public class EventController(
    IEventRepository eventRepository,
    IEventService eventService,
    IPersonRepository personRepository,
    ILocalRepository localRepository,
    ModelConverter modelConverter
) : ControllerBase {

    [HttpGet("{id}")]
    public async Task<EventFullModel> Get(int id) {
        var ev = await eventRepository.GetRequiredAsync(id);
        var participants = await personRepository.ListByEventAsync(ev.Id);
        var local = ev.LocalId.HasValue
            ? await localRepository.GetRequiredAsync(ev.LocalId.Value)
            : null;
        return modelConverter.ToFullModel(ev, local, participants);
    }

    [HttpPost]
    public async Task<EventModel> Create(EventCreateModel data) {
        var ev = await eventService.AddAsync(data);
        return modelConverter.ToModel(ev);
    }

    [HttpPut]
    public async Task<EventModel> UpdateAsync(EventUpdateModel data) {
        var ev = await eventService.UpdateAsync(data);
        return modelConverter.ToModel(ev);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id) {
        var ev = await eventRepository.GetRequiredAsync(id);
        await eventRepository.DeleteAsync(ev);
        return NoContent();
    }

    [HttpGet("list")] // TODO: implement pagination
    public async Task<List<EventModel>> ListAsync() {
        var events = await eventRepository.ListAsync();
        return [..events.ConvertAll(modelConverter.ToModel)];
    }
}