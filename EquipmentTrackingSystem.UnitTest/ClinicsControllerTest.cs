using EquipmentTrackingSystem.Domain.Models;
using EquipmentTrackingSystem.Extension.Interfaces;
using EquipmentTrackingSystem.Extension.Services;
using EquipmentTrackingSystem.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EquipmentTrackingSystem.UnitTest
{
    public class ClinicsControllerTest
    {
        private readonly Mock<IClinicService> _mock = new Mock<IClinicService>();

        [Fact]
        public async Task GetClinicShouldReturnRightObjectWhenClinicExists()
        {
            // Arrange
            var id = 1;
            var clinic = new Clinic() { Id = id, ClinicName = "Test Clinic" };

            _mock.Setup(c => c.GetResourceAsync(id)).ReturnsAsync(clinic);
            var controller = new ClinicsController(_mock.Object);

            // Act
            var request = await controller.GetClinic(id);

            // Assert
            var result = Assert.IsType<ActionResult<Clinic>>(request);
            var model = Assert.IsAssignableFrom<Clinic>(result.Value);

            Assert.Equal(id, model.Id);
        }
    }
}
