using EquipmentTrackingSystem.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EquipmentTrackingSystem.IntegrationTest
{
    public class ClinicsControllerTest : IntegrationTest
    {
        [Fact]
        public async Task GetClinicsEndpointShouldReturnOK()
        {
            // Act
            var request = await _client.GetAsync("api/clinics");

            // Assert
            request.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.OK, request.StatusCode);
        }

        [Fact]
        public async Task GetClinicEndpointShouldReturnRightObject()
        {
            // Arrange
            var id = 30;

            // Act
            var request = await _client.GetAsync("api/clinics/" + id);

            // Assert
            request.EnsureSuccessStatusCode();
            
            var response = await request.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Clinic>(response);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task PostClinicEndpointShouldReturnCreatedObject()
        {
            // Arrange
            var clinic = new Clinic() { ClinicName = "Test Clinic" };

            // Act
            var request = await _client.PostAsync("api/clinics", new StringContent(JsonConvert.SerializeObject(clinic), Encoding.UTF8, "application/json"));

            // Assert
            request.EnsureSuccessStatusCode();

            var response = await request.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Clinic>(response);

            Assert.Equal(System.Net.HttpStatusCode.Created, request.StatusCode);
            Assert.NotNull(result);
            Assert.Equal(clinic.ClinicName, result.ClinicName);
        }

        [Fact]
        public async Task PutClinicEndpointShouldReturnNoContent()
        {
            // Arrange
            var clinic = new Clinic() { Id = 29, ClinicName = "[Updated] Test Clinic" };

            // Act
            var request = await _client.PutAsync("api/clinics/" + clinic.Id, new StringContent(JsonConvert.SerializeObject(clinic), Encoding.UTF8, "application/json"));

            // Assert
            request.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.NoContent, request.StatusCode);
        }

        [Fact]
        public async Task PutClinicEndpointShouldReturnBadRequest()
        {
            // Arrange
            var clinic = new Clinic() { Id = 29, ClinicName = "[Updated] Test Clinic With An Error" };

            // Act
            var request = await _client.PutAsync("api/clinics/" + 5, new StringContent(JsonConvert.SerializeObject(clinic), Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, request.StatusCode);
        }

        [Fact]
        public async Task PutClinicEndpointShouldReturnInternalServerError()
        {
            // Arrange
            var clinic = new Clinic() { Id = 9999, ClinicName = "[Updated] Test Clinic With An Error" };

            // Act
            var request = await _client.PutAsync("api/clinics/" + 9999, new StringContent(JsonConvert.SerializeObject(clinic), Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, request.StatusCode);
        }

        [Fact]
        public async Task DeleteClinicEndpointShouldReturnNoContent()
        {
            // Arrange
            var clinic = new Clinic() { Id = 34 };

            // Act
            var request = await _client.DeleteAsync("api/clinics/" + clinic.Id);

            var response = await request.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Clinic>(response);

            // Assert
            request.EnsureSuccessStatusCode();

            Assert.Equal(System.Net.HttpStatusCode.NoContent, request.StatusCode);
        }
    }
}
