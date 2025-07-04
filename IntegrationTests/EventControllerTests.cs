using cp_journey_backend.Api;
using System.Net.Http.Json;

namespace IntegrationTests {
    public class EventControllerTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory> {
        private readonly HttpClient httpClient = factory.CreateClient();

        [Fact]
        public async Task Get_ReturnsEvent() {
            var eventModel = await Util.CreateEventAsync(httpClient);
            var response = await httpClient.GetAsync($"/api/events/{eventModel.Id}");
            response.EnsureSuccessStatusCode();
            var fetched = await response.Content.ReadFromJsonAsync<EventFullModel>();
            Assert.Equal(eventModel.Name, fetched.Name);
            Assert.Equal(eventModel.Description, fetched.Description);
            Assert.Equal(eventModel.WebsiteUrl, fetched.WebsiteUrl);
            Assert.True(Util.CompareDates(eventModel.Start.Value, fetched.Start.Value));
            Assert.True(Util.CompareDates(eventModel.End.Value, fetched.End.Value));
        }

        [Fact]
        public async Task Post_CreatesEvent() {
            var local = await Util.CreateLocalAsync(httpClient);
            var date = DateTime.UtcNow.ToUniversalTime();
            var createModel = new EventCreateModel {
                Name = "Summer camp 2023",
                Start = date,
                End = date.AddHours(5),
                Description = "summer camp for students",
                WebsiteUrl = "https://summer.ic.unicamp.com",
                LocalId = local.Id,
                ParticipantIds = [],
            };
            var response = await httpClient.PostAsJsonAsync("/api/events", createModel);
            response.EnsureSuccessStatusCode();
            var eventModel = await response.Content.ReadFromJsonAsync<EventModel>();
            Assert.Equal(createModel.Name, eventModel.Name);
            Assert.Equal(createModel.Description, eventModel.Description);
            Assert.Equal(createModel.WebsiteUrl, eventModel.WebsiteUrl);
            Assert.True(Util.CompareDates(createModel.Start, eventModel.Start.Value));
            Assert.True(Util.CompareDates(createModel.End, eventModel.End.Value));
        }

        [Fact]
        public async Task Put_UpdatesEvent() {
            var eventModel = await Util.CreateEventAsync(httpClient);
            var local = await Util.CreateLocalAsync(httpClient);
            var updateModel = new EventUpdateModel {
                Id = eventModel.Id,
                Name = "Updated Event",
                Start = DateTime.UtcNow.ToUniversalTime(),
                End = DateTime.UtcNow.AddDays(2).ToUniversalTime(),
                Description = "Updated Description",
                WebsiteUrl = "https://updated.com",
                LocalId = local.Id,
                ParticipantIds = []
            };
            var response = await httpClient.PutAsJsonAsync("/api/events", updateModel);
            response.EnsureSuccessStatusCode();
            var updated = await response.Content.ReadFromJsonAsync<EventModel>();
            Assert.Equal(updateModel.Name, updated.Name);
            Assert.Equal(updateModel.Description, updated.Description);
            Assert.Equal(updateModel.WebsiteUrl, updated.WebsiteUrl);
            Assert.True(Util.CompareDates(updated.Start.Value, updateModel.Start));
            Assert.True(Util.CompareDates(updated.End.Value, updateModel.End));
        }

        [Fact]
        public async Task Delete_DeletesEvent() {
            var eventModel = await Util.CreateEventAsync(httpClient);
            var deleteResponse = await httpClient.DeleteAsync($"/api/events/{eventModel.Id}");
            deleteResponse.EnsureSuccessStatusCode();
            var getResponse = await httpClient.GetAsync($"/api/events/{eventModel.Id}");
            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, getResponse.StatusCode);
        }
    }
}