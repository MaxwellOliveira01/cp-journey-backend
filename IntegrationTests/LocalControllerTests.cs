using cp_journey_backend.Api;
using System.Net.Http.Json;

namespace IntegrationTests {
    
    public class LocalControllerTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory> {

        private readonly HttpClient httpClient = factory.CreateClient();
        
        [Fact]
        public async Task Get_ReturnsLocal() {

            // Arrange
            var local = await Util.CreateLocalAsync(httpClient);

            // Act
            
            var response = await httpClient.GetAsync($"/api/locals/{local.Id}");
            response.EnsureSuccessStatusCode();

            // TODO: test the other fields of fullModel, like universities and students
            var fetchedLocal = await response.Content.ReadFromJsonAsync<LocalFullModel>();

            // Assert
            
            Assert.Equivalent(local, fetchedLocal);
        }

        [Fact]
        public async Task Post_CreatesLocal() {

            // Arrange

            var createModel = new LocalCreateModel() {
                City = "Rio de Janeiro",
                State = "Rio de Janeiro",
                Country = "Brazil",
            };

            // Act

            var response = await httpClient.PostAsJsonAsync("/api/locals", createModel);
            response.EnsureSuccessStatusCode();
            
            var local = await response.Content.ReadFromJsonAsync<LocalModel>();
            
            // Assert

            Assert.NotNull(local);
            Assert.Equal(createModel.City, local.City);
            Assert.Equal(createModel.State, local.State);
            Assert.Equal(createModel.Country, local.Country);
        }

        [Fact]
        public async Task Put_UpdatesLocal() {

            // Arrange
            
            var local = await Util.CreateLocalAsync(httpClient);
            
            var updateModel = new LocalUpdateModel() {
                Id = local.Id,
                City = "Sao Paulo Updated",
                State = "Sao Paulo Updated",
                Country = "Brazil Updated",
            };

            // Act
            
            var updateResponse = await httpClient.PutAsJsonAsync("/api/locals", updateModel);
            updateResponse.EnsureSuccessStatusCode();
            var updatedLocal = await updateResponse.Content.ReadFromJsonAsync<LocalModel>();
            
            // Assert
            
            Assert.NotNull(updatedLocal);
            Assert.Equal(updateModel.City, updatedLocal.City);
            Assert.Equal(updateModel.State, updatedLocal.State);
            Assert.Equal(updateModel.Country, updatedLocal.Country);
        }

        [Fact]
        public async Task Delete_DeletesLocal() {

            // Arrange

            var local = await Util.CreateLocalAsync(httpClient);

            // Act

            var deleteResponse = await httpClient.DeleteAsync($"/api/locals/{local.Id}");
            deleteResponse.EnsureSuccessStatusCode();
            
            var getResponse = await httpClient.GetAsync($"/api/locals/{local.Id}");

            // Assert

            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, getResponse.StatusCode);

        }

    }
}
