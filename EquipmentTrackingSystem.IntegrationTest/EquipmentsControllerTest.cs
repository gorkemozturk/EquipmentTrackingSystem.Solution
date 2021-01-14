using EquipmentTrackingSystem.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EquipmentTrackingSystem.IntegrationTest
{
    public class EquipmentsControllerTest : IntegrationTest
    {
        [Fact]
        public async Task PostEquipmentEndpointShouldReturnCreatedObject()
        {
            // Arrange
            var clinicId = 30;

            var equipment = new Equipment() { 
                EquipmentName = "Test equipment",
                Condition = 98.72,
                Quantity = 35,
                UnitPrice = 675.85,
                ClinicId = clinicId
            };

            // Act
            var payload = new StringContent(JsonConvert.SerializeObject(equipment), Encoding.UTF8, "application/json");
            var request = await _client.PostAsync("api/equipments/" + clinicId, payload);

            // Assert
            request.EnsureSuccessStatusCode();

            var response = await request.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Equipment>(response);

            Assert.Equal(System.Net.HttpStatusCode.OK, request.StatusCode);
            Assert.NotNull(result);
            Assert.Equal(equipment.EquipmentName, result.EquipmentName);
        }
    }
}
