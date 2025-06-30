using cp_journey_backend.Api;
using cp_journey_backend.Entities;
using cp_journey_backend.Repositories;

namespace cp_journey_backend.Services;

public interface IEventService {
    Task<Event> AddAsync(EventCreateModel eventModel);
    Task<Event> UpdateAsync(EventUpdateModel eventModel);
}

public class EventService(
    IEventRepository eventRepository
) : IEventService {

    public async Task<Event> AddAsync(EventCreateModel data) {
        
        var ev = new Event {
            Id = Guid.NewGuid(),
            Name = data.Name,
            Start = data.Start,
            End = data.End,
            Description = data.Description,
            WebsiteUrl = data.WebsiteUrl
        };

        ev.Participants = (data.ParticipantIds ?? []).Select(p => new EventParticipation {
            PersonId = p,
            EventId = ev.Id
        }).ToList();
        
        await eventRepository.AddAsync(ev);
        return ev;
    }

    public async Task<Event> UpdateAsync(EventUpdateModel data) {
        var ev = await eventRepository.GetRequiredAsync(data.Id);

        ev.Name = data.Name;
        ev.Start = data.Start;
        ev.End = data.End;
        ev.Description = data.Description;
        ev.WebsiteUrl = data.WebsiteUrl;

        ev.Participants = (data.ParticipantIds ?? []).Select(p => new EventParticipation {
            PersonId = p,
            EventId = ev.Id
        }).ToList();

        await eventRepository.UpdateAsync(ev);
        return ev;
    }
}