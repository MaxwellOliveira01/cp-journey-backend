using cp_journey_backend.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests {
    public class UniversityControllerTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory> {
        
        private readonly HttpClient httpClient = factory.CreateClient();

        [Fact]
        public async Task Get_ReturnsUniversity() {

            // Arrange
            var university = await Util.CreateUniversityAsync(httpClient);

            // Act
            var response = await httpClient.GetAsync($"/api/universities/{university.Id}");
            response.EnsureSuccessStatusCode();

            var fetchedUniversity = await response.Content.ReadFromJsonAsync<UniversityFullModel>();

            // Assert

            Assert.Equivalent(university, fetchedUniversity);
        
        }

        [Fact]
        public async Task Post_CreatesUniversity() {

            // Arrange

            var local = await Util.CreateLocalAsync(httpClient);

            var createModel = new UniversityCreateModel() {
                Name = "University of Brasilia",
                Alias = "UnB",
                LocalId = local.Id,
            };

            // Act

            var response = await httpClient.PostAsJsonAsync("/api/universities", createModel);
            response.EnsureSuccessStatusCode();

            var university = await response.Content.ReadFromJsonAsync<UniversityFullModel>();

            // Assert

            Assert.NotNull(local);
            Assert.Equal(createModel.Name, university.Name);
            Assert.Equal(createModel.Alias, university.Alias);
        }

        [Fact]
        public async Task Put_UpdatesUniversity() {

            // Arrange

            var local = await Util.CreateLocalAsync(httpClient);
            var university = await Util.CreateUniversityAsync(httpClient, local.Id);

            var anotherLocal = await Util.CreateLocalAsync(httpClient);

            var updateModel = new UniversityUpdateModel() {
                Id = university.Id,
                Name = "University of São Paulo",
                Alias = "USP",
                LocalId = anotherLocal.Id
            };

            // Act

            var updateResponse = await httpClient.PutAsJsonAsync("/api/universities", updateModel);
            updateResponse.EnsureSuccessStatusCode();
            var updatedLocal = await updateResponse.Content.ReadFromJsonAsync<UniversityModel>();

            // Assert

            Assert.NotNull(updatedLocal);
            Assert.Equal(updateModel.Name, updatedLocal.Name);
            Assert.Equal(updateModel.Alias, updatedLocal.Alias);
        }

        [Fact]
        public async Task Delete_DeletesUniversity() {

            // Arrange

            var university = await Util.CreateUniversityAsync(httpClient);

            // Act

            var deleteResponse = await httpClient.DeleteAsync($"/api/universities/{university.Id}");
            deleteResponse.EnsureSuccessStatusCode();

            var getResponse = await httpClient.GetAsync($"/api/universities/{university.Id}");

            // Assert

            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, getResponse.StatusCode);

        }



    }
}
