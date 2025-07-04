using cp_journey_backend.Api;
using System.Net.Http.Json;

namespace IntegrationTests {
    public static class Util {
        
        public static async Task<LocalModel> CreateLocalAsync(HttpClient httpClient) {

            var createModel = new LocalCreateModel() {
                City = "Brasilia",
                State = "Federal District",
                Country = "Brazil",
            };

            var createResponse = await httpClient.PostAsJsonAsync("/api/locals", createModel);
            createResponse.EnsureSuccessStatusCode();

            return await createResponse.Content.ReadFromJsonAsync<LocalModel>();
        }

        public static async Task<UniversityModel> CreateUniversityAsync(HttpClient httpClient, Guid? localId = null) {

            if(!localId.HasValue) {
                var local = await CreateLocalAsync(httpClient);
                localId = local.Id;
            }

            var createModel = new UniversityCreateModel() {
               Name = "University of Brasilia",
               Alias = "UnB",
               LocalId = localId,
            };
            var createResponse = await httpClient.PostAsJsonAsync("/api/universities", createModel);
            createResponse.EnsureSuccessStatusCode();
            return await createResponse.Content.ReadFromJsonAsync<UniversityModel>();
        }
        
        public static async Task<PersonModel> CreatePersonAsync(HttpClient httpClient, Guid? universityId = null) {
            if (!universityId.HasValue) {
                var university = await CreateUniversityAsync(httpClient);
                universityId = university.Id;
            }
            var createModel = new PersonCreateModel() {
                Name = "Maxwell Reis",
                Handle = "Maxwell01",
                UniversityId = universityId,
            };
            var createResponse = await httpClient.PostAsJsonAsync("/api/persons", createModel);
            createResponse.EnsureSuccessStatusCode();
            return await createResponse.Content.ReadFromJsonAsync<PersonModel>();
        }

        public static async Task<ContestModel> CreateContestAsync(HttpClient httpClient, Guid? localId = null) {
            if (!localId.HasValue) {
                var local = await CreateLocalAsync(httpClient);
                localId = local.Id;
            }

            var date = DateTime.UtcNow.ToUniversalTime();
            
            var createModel = new ContestCreateModel() {
                Name = "Maratona APC de Programação 2025.1",
                SiteUrl = "https://cic.unb.br",
                StartDate = date,
                EndDate = date.AddHours(5),
                LocalId = localId,
            };
            var createResponse = await httpClient.PostAsJsonAsync("/api/contests", createModel);
            createResponse.EnsureSuccessStatusCode();
            return await createResponse.Content.ReadFromJsonAsync<ContestModel>();
        }

        public static async Task<EventModel> CreateEventAsync(HttpClient httpClient, Guid? localId = null) {
            if (!localId.HasValue) {
                var local = await CreateLocalAsync(httpClient);
                localId = local.Id;
            }

            var date = DateTime.UtcNow.ToUniversalTime();
            
            var createModel = new EventCreateModel() {
                Name = "Unicamp - Summer Camp 2025",
                Start = date.Subtract(TimeSpan.FromMilliseconds(date.Millisecond)),
                End = date.AddHours(5).Subtract(TimeSpan.FromMilliseconds(date.Millisecond)),
                Description = "Summer camp for students interested in programming.",
                WebsiteUrl = "https://unicamp-summer.com",
                LocalId = localId,
                ParticipantIds = [],
            };
            var createResponse = await httpClient.PostAsJsonAsync("/api/events", createModel);
            createResponse.EnsureSuccessStatusCode();
            return await createResponse.Content.ReadFromJsonAsync<EventModel>();
        }

        public static async Task<ProblemModel> CreateProblemAsync(HttpClient httpClient, Guid? contestId = null, Guid? setterId = null) {
            if (!contestId.HasValue) {
                var contest = await CreateContestAsync(httpClient);
                contestId = contest.Id;
            }
            
            if (!setterId.HasValue) {
                var person = await CreatePersonAsync(httpClient);
                setterId = person.Id;
            }
            
            var createModel = new ProblemCreateModel() {
                Name = "110 Multiverse",
                Label = "A",
                Order = 1,
                ContestId = contestId.Value,
                SetterId = setterId,
            };
            var createResponse = await httpClient.PostAsJsonAsync("/api/problems", createModel);
            createResponse.EnsureSuccessStatusCode();
            return await createResponse.Content.ReadFromJsonAsync<ProblemModel>();
        }

        public static bool CompareDates(DateTime first, DateTime second) {
            return Math.Abs((first - second).TotalMilliseconds) < 1;
        }
        
    }
}
