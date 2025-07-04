using cp_journey_backend.Api;
using System.Net.Http.Json;

namespace IntegrationTests {
    public class ContestControllerTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory> {
        private readonly HttpClient httpClient = factory.CreateClient();

        [Fact]
        public async Task Get_ReturnsContest() {
            var contest = await Util.CreateContestAsync(httpClient);
            var response = await httpClient.GetAsync($"/api/contests/{contest.Id}");
            response.EnsureSuccessStatusCode();
            var fetched = await response.Content.ReadFromJsonAsync<ContestFullModel>();
            Assert.Equal(contest.Name, fetched.Name);
            Assert.Equal(contest.SiteUrl, fetched.SiteUrl);
            Assert.True(Util.CompareDates(contest.StartDate.Value, fetched.StartDate.Value));
            Assert.True(Util.CompareDates(contest.EndDate.Value, fetched.EndDate.Value));
        }

        [Fact]
        public async Task Post_CreatesContest() {
            var local = await Util.CreateLocalAsync(httpClient);
            var createModel = new ContestCreateModel {
                Name = "Sample Contest2",
                SiteUrl = "https://contest2.com",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1),
                LocalId = local.Id
            };
            var response = await httpClient.PostAsJsonAsync("/api/contests", createModel);
            response.EnsureSuccessStatusCode();
            var contest = await response.Content.ReadFromJsonAsync<ContestModel>();
            Assert.Equal(createModel.Name, contest.Name);
            Assert.Equal(createModel.SiteUrl, contest.SiteUrl);
            Assert.Equal(createModel.StartDate, contest.StartDate);
            Assert.Equal(createModel.EndDate, contest.EndDate);
        }

        [Fact]
        public async Task Put_UpdatesContest() {
            var contest = await Util.CreateContestAsync(httpClient);
            var local = await Util.CreateLocalAsync(httpClient);
            var updateModel = new ContestUpdateModel {
                Id = contest.Id,
                Name = "Maratona UnBalloon 2023",
                SiteUrl = "https://cic2.unb.br",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(2),
                LocalId = local.Id
            };
            var response = await httpClient.PutAsJsonAsync("/api/contests", updateModel);
            response.EnsureSuccessStatusCode();
            var updated = await response.Content.ReadFromJsonAsync<ContestModel>();
            Assert.Equal(updateModel.Name, updated.Name);
            Assert.Equal(updateModel.SiteUrl, updated.SiteUrl);
            Assert.Equal(updateModel.StartDate, updated.StartDate);
            Assert.Equal(updateModel.EndDate, updated.EndDate);
        }

        [Fact]
        public async Task Delete_DeletesContest() {
            var contest = await Util.CreateContestAsync(httpClient);
            var deleteResponse = await httpClient.DeleteAsync($"/api/contests/{contest.Id}");
            deleteResponse.EnsureSuccessStatusCode();
            var getResponse = await httpClient.GetAsync($"/api/contests/{contest.Id}");
            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, getResponse.StatusCode);
        }
    }
}