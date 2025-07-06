using cp_journey_backend.Api;
using System.Net.Http.Json;

namespace IntegrationTests {
    public class ProblemControllerTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory> {
        private readonly HttpClient httpClient = factory.CreateClient();

        [Fact]
        public async Task Get_ReturnsProblem() {
            var problem = await Util.CreateProblemAsync(httpClient);
            var response = await httpClient.GetAsync($"/api/problems/{problem.Id}");
            response.EnsureSuccessStatusCode();
            var fetched = await response.Content.ReadFromJsonAsync<ProblemModel>();
            Assert.Equivalent(problem, fetched);
        }

        [Fact]
        public async Task Post_CreatesProblem() {
            var contest = await Util.CreateContestAsync(httpClient);
            var createModel = new ProblemCreateModel {
                Name = "Factorial",
                Label = "C",
                Order = 5,
                ContestId = contest.Id,
            };
            var response = await httpClient.PostAsJsonAsync("/api/problems", createModel);
            response.EnsureSuccessStatusCode();
            var problem = await response.Content.ReadFromJsonAsync<ProblemModel>();
            Assert.Equal(createModel.Name, problem.Name);
            Assert.Equal(createModel.Label, problem.Label);
            Assert.Equal(createModel.Order, problem.Order);
        }

        [Fact]
        public async Task Put_UpdatesProblem() {
            var problem = await Util.CreateProblemAsync(httpClient);
            var contest = await Util.CreateContestAsync(httpClient);
            var setter = await Util.CreatePersonAsync(httpClient);
            var updateModel = new ProblemUpdateModel {
                Id = problem.Id,
                Name = "Factorial 2",
                Label = "G",
                Order = 8,
                ContestId = contest.Id,
            };
            var response = await httpClient.PutAsJsonAsync("/api/problems", updateModel);
            response.EnsureSuccessStatusCode();
            var updated = await response.Content.ReadFromJsonAsync<ProblemModel>();
            Assert.Equal(updateModel.Name, updated.Name);
            Assert.Equal(updateModel.Label, updated.Label);
            Assert.Equal(updateModel.Order, updated.Order);
        }

        [Fact]
        public async Task Delete_DeletesProblem() {
            var problem = await Util.CreateProblemAsync(httpClient);
            var deleteResponse = await httpClient.DeleteAsync($"/api/problems/{problem.Id}");
            deleteResponse.EnsureSuccessStatusCode();
            var getResponse = await httpClient.GetAsync($"/api/problems/{problem.Id}");
            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, getResponse.StatusCode);
        }
    }
}