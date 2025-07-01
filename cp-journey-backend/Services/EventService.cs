using cp_journey_backend.Api;
using cp_journey_backend.Entities;
using cp_journey_backend.Repositories;

namespace cp_journey_backend.Services;

public interface IEventService {
    Task<Event> AddAsync(EventCreateModel eventModel);
    Task<Event> UpdateAsync(EventUpdateModel eventModel);
}

public class EventService(
    IEventRepository eventRepository,
    ILocalRepository localRepository
) : IEventService {

    public async Task<Event> AddAsync(EventCreateModel data) {
        var ev = new Event { Id = Guid.NewGuid() };
        await updateFields(ev, data);
        await eventRepository.AddAsync(ev);
        return ev;
    }

    public async Task<Event> UpdateAsync(EventUpdateModel data) {
        var ev = await eventRepository.GetRequiredAsync(data.Id);
        await updateFields(ev, data);
        await eventRepository.UpdateAsync(ev);
        return ev;
    }

    private async Task updateFields(Event ev, EventCreateModel data) {
        
        var local = data.LocalId.HasValue
            ? await localRepository.GetRequiredAsync(data.LocalId.Value)
            : null;
        
        ev.Name = data.Name;
        ev.Start = data.Start;
        ev.End = data.End;
        ev.Description = data.Description;
        ev.WebsiteUrl = data.WebsiteUrl;
        ev.LocalId = data.LocalId;
        ev.Local = local;

        ev.Participants = (data.ParticipantIds ?? []).Select(p => new EventParticipation {
            PersonId = p,
            EventId = ev.Id
        }).ToList();
    }
    
}