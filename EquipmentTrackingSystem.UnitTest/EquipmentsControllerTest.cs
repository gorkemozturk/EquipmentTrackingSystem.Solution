using EquipmentTrackingSystem.Domain.Models;
using EquipmentTrackingSystem.Extension.Interfaces;
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
    public class EquipmentsControllerTest
    {
        private readonly Mock<IEquipmentService> _mock = new Mock<IEquipmentService>();

        [Fact]
        public async Task PostEquipmentShouldReturnEquipmentWhenCriteriaIsFetching()
        {
            // Arrange
            var clinicId = 5;
            var equipment = new Equipment() 
            {
                Id = 1,
                EquipmentName = "Test Equipment",
                Condition = 98.52,
                Quantity = 25,
                UnitPrice = 75.25,
                SuppliedAt = DateTime.Now,
                ClinicId = clinicId
            };

            _mock.Setup(c => c.CreateEquipmentAsync(clinicId, equipment)).ReturnsAsync(equipment);
            var controller = new EquipmentsController(_mock.Object);

            // Act
            var request = await controller.PostEquipment(clinicId, equipment);

            // Assert
            var result = Assert.IsType<ActionResult<Equipment>>(request);
            var model = Assert.IsAssignableFrom<Equipment>(result.Value);

            Assert.Equal(clinicId, model.ClinicId);
        }

        [Fact]
        public async Task PostEquipmentShouldReturnExeptionWhenCriteriaIsNotFetching()
        {
            // Arrange
            var clinicId = 5;
            var equipment = new Equipment()
            {
                Id = 1,
                EquipmentName = "Test Equipment",
                Condition = 98.52,
                Quantity = 25,
                UnitPrice = 75.25,
                SuppliedAt = DateTime.Now,
                ClinicId = 10
            };

            _mock.Setup(c => c.CreateEquipmentAsync(clinicId, equipment)).ReturnsAsync(equipment);
            var controller = new EquipmentsController(_mock.Object);

            // Act
            var request = await controller.PostEquipment(clinicId, equipment);

            // Assert
            var result = Assert.IsType<ActionResult<Equipment>>(request);
            var model = Assert.IsAssignableFrom<Equipment>(result.Value);

            Assert.NotEqual(clinicId, equipment.ClinicId);
        }
    }
}
