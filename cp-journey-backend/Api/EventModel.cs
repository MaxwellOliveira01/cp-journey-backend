    namespace cp_journey_backend.Api;

    public class EventModel {
        
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }
        
        public string? Description { get; set; }
        
        public string? WebsiteUrl { get; set; }
        
    }

    public class EventFullModel : EventModel {
        
        public LocalModel? Local { get; set; }
        
        public List<PersonModel> Participants { get; set; }
    }

    public class EventCreateModel {
        public string Name { get; set; }
        
        public DateTime? Start { get; set; }
        
        public DateTime? End { get; set; }
        
        public string? Description { get; set; }
        
        public string? WebsiteUrl { get; set; }
        
        public int? LocalId { get; set; }
        
        public List<int> ParticipantIds { get; set; }
        
    }

    public class EventUpdateModel : EventCreateModel {
        
        public int Id { get; set; }
        
    }
