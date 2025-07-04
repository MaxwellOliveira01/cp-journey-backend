using cp_journey_backend.Api;
using System.Net.Http.Json;

namespace IntegrationTests {
    public class PersonControllerTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory> {
        private readonly HttpClient httpClient = factory.CreateClient();

        [Fact]
        public async Task Get_ReturnsPerson() {
            var person = await Util.CreatePersonAsync(httpClient);
            var response = await httpClient.GetAsync($"/api/persons/{person.Id}");
            response.EnsureSuccessStatusCode();
            var fetched = await response.Content.ReadFromJsonAsync<PersonFullModel>();
            Assert.Equivalent(person, fetched);
        }

        [Fact]
        public async Task Post_CreatesPerson() {
            var university = await Util.CreateUniversityAsync(httpClient);
            var createModel = new PersonCreateModel {
                Name = "Maxwell Reis",
                Handle = "Maxwell01",
                UniversityId = university.Id
            };
            var response = await httpClient.PostAsJsonAsync("/api/persons", createModel);
            response.EnsureSuccessStatusCode();
            var person = await response.Content.ReadFromJsonAsync<PersonModel>();
            Assert.Equal(createModel.Name, person.Name);
            Assert.Equal(createModel.Handle, person.Handle);
        }

        [Fact]
        public async Task Put_UpdatesPerson() {
            var person = await Util.CreatePersonAsync(httpClient);
            var university = await Util.CreateUniversityAsync(httpClient);
            var updateModel = new PersonUpdateModel {
                Id = person.Id,
                Name = "Maxwell2",
                Handle = "Maxwell01",
                UniversityId = university.Id
            };
            var response = await httpClient.PutAsJsonAsync("/api/persons", updateModel);
            response.EnsureSuccessStatusCode();
            var updated = await response.Content.ReadFromJsonAsync<PersonModel>();
            Assert.Equal(updateModel.Name, updated.Name);
            Assert.Equal(updateModel.Handle, updated.Handle);
        }

        [Fact]
        public async Task Delete_DeletesPerson() {
            var person = await Util.CreatePersonAsync(httpClient);
            var deleteResponse = await httpClient.DeleteAsync($"/api/persons/{person.Id}");
            deleteResponse.EnsureSuccessStatusCode();
            var getResponse = await httpClient.GetAsync($"/api/persons/{person.Id}");
            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, getResponse.StatusCode);
        }
    }
}