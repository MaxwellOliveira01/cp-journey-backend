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


    }
}
