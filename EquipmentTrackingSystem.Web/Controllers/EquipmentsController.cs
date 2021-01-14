using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EquipmentTrackingSystem.Domain.Models;
using EquipmentTrackingSystem.Extension.Interfaces;
using EquipmentTrackingSystem.Extension.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EquipmentTrackingSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentsController : ControllerBase
    {
        private readonly IEquipmentService _equipment;

        public EquipmentsController(IEquipmentService equipment)
        {
            _equipment = equipment;
        }

        [HttpGet]
        public async Task<IEnumerable<EquipmentIndexViewModel>> GetEquipments([FromQuery] ParameterCommonViewModel parameter)
        {
            try
            {
                var equipments = await _equipment.GetEquipmentsAsync(parameter);

                var meta = new
                {
                    equipments.Total,
                    equipments.Page,
                    equipments.Size,
                    equipments.Pages,
                    equipments.HasNext,
                    equipments.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(meta).ToLower());
                LoggerService.GetInstance.CreateLog("[GetEquipments] The get process has completed successfully.");

                return equipments;
            }
            catch (Exception e)
            {
                LoggerService.GetInstance.CreateLog("[GetEquipments]" + e.Message);
                
                throw new Exception(e.Message);
            }
        }

        [HttpPost("{clinicId}")]
        public async Task<ActionResult<Equipment>> PostEquipment([FromRoute] int clinicId, [FromBody] Equipment equipment)
        {
            if (clinicId != equipment.ClinicId)
            {
                LoggerService.GetInstance.CreateLog($"[PostEquipment: {clinicId}] The criteria did not match.");

                return BadRequest();
            }

            var result = await _equipment.CreateEquipmentAsync(clinicId, equipment);

            if (result == null)
            {
                LoggerService.GetInstance.CreateLog($"[PostEquipment: {clinicId}] The clinic not found.");

                return NotFound();
            }

            LoggerService.GetInstance.CreateLog($"[PostEquipment: {clinicId}] The post process has completed successfully.");

            return result;
        }

        [HttpGet("{id}", Name = "GetEquipment")]
        public async Task<ActionResult<Equipment>> GetEquipment([FromRoute] int id)
        {

            var equipment = await _equipment.GetResourceAsync(id);

            if (equipment == null)
            {
                LoggerService.GetInstance.CreateLog($"[GetEquipment: {id}] The equipment not found.");

                return NotFound();
            }

            LoggerService.GetInstance.CreateLog($"[GetEquipment: {id}] The get process has completed successfully.");

            return equipment;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipment([FromRoute] int id, [FromBody] EquipmentUpdateViewModel equipment)
        {
            if (id != equipment.Id)
            {
                LoggerService.GetInstance.CreateLog($"[PutEquipment: {id}] The criteria did not match.");

                return BadRequest();
            }

            var result = await _equipment.UpdateEquipmentAsync(id, equipment);

            if (result == null)
            {
                LoggerService.GetInstance.CreateLog($"[PutEquipment: {id}] The equipment not found.");

                return NotFound();
            }

            LoggerService.GetInstance.CreateLog($"[PutEquipment: {id}] The put process has completed successfully.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Equipment>> DeleteEquipment([FromRoute] int id)
        {
            try
            {
                var resource = await _equipment.RemoveResourceAsync(id);
                LoggerService.GetInstance.CreateLog($"[DeleteEquipment: {id}] The delete process has completed successfully.");
                
                return resource;
            }
            catch (Exception e)
            {
                LoggerService.GetInstance.CreateLog($"[DeleteEquipment: {id}] " + e.Message);
                
                throw new Exception(e.Message);
            }
        }
    }
}