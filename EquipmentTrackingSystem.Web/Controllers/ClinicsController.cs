using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentTrackingSystem.Domain.Models;
using EquipmentTrackingSystem.Extension.Interfaces;
using EquipmentTrackingSystem.Extension.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EquipmentTrackingSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicsController : ControllerBase
    {
        private readonly IClinicService _clinic;

        public ClinicsController(IClinicService clinic)
        {
            _clinic = clinic;
        }

        [HttpGet]
        public async Task<IEnumerable<ClinicIndexViewModel>> GetClinics([FromQuery] ParameterCommonViewModel parameter)
        {
            try
            {
                var clinics = await _clinic.GetClinicsAsync(parameter);

                var meta = new
                {
                    clinics.Total,
                    clinics.Page,
                    clinics.Size,
                    clinics.Pages,
                    clinics.HasNext,
                    clinics.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(meta).ToLower());
                LoggerService.GetInstance.CreateLog("[GetClinics] The get process has completed successfully.");

                return clinics;
            }
            catch (Exception e)
            {
                LoggerService.GetInstance.CreateLog("[GetClinics]" + e.Message);
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Clinic>> PostClinic([FromBody] Clinic clinic)
        {
            try
            {
                var resource = await _clinic.CreateResourceAsync(clinic);
                LoggerService.GetInstance.CreateLog("[PostClinic] The post process has completed successfully.");

                return CreatedAtRoute(nameof(GetClinic), new { id = resource.Id }, resource);
            }
            catch (Exception e)
            {
                LoggerService.GetInstance.CreateLog("[PostClinic]" + e.Message);
                throw new Exception(e.Message);
            }
        }

        [HttpGet("{id}", Name = "GetClinic")]
        public async Task<ActionResult<Clinic>> GetClinic([FromRoute] int id)
        {
            var clinic = await _clinic.GetResourceAsync(id);

            if (clinic == null)
            {
                LoggerService.GetInstance.CreateLog($"[GetClinic: {id}] Clinic not found.");
                
                return NotFound();
            }

            return clinic;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutClinic([FromRoute] int id, [FromBody] Clinic clinic)
        {
            if (id != clinic.Id)
            {
                LoggerService.GetInstance.CreateLog("[PutClinic] The criteria not matched.");
                
                return BadRequest();
            }

            try
            {
                await _clinic.UpdateResourceAsync(clinic);
                LoggerService.GetInstance.CreateLog($"[PutClinic: {id}] The put process has completed successfully.");
            }
            catch (Exception e)
            {
                LoggerService.GetInstance.CreateLog($"[PutClinic: {id}] " + e.Message);
                
                throw new Exception(e.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Clinic>> DeleteClinic([FromRoute] int id)
        {
            var clinic = await _clinic.GetClinicWithEquipments(id);

            if (clinic == null)
            {
                LoggerService.GetInstance.CreateLog($"[DeleteClinic: {id}] The clinic not found.");
                
                return NotFound();
            }

            if (clinic.Equipments.Count() > 0)
            {
                LoggerService.GetInstance.CreateLog($"[DeleteClinic: {id}] The clinic cannot delete since it has equipments.");
                
                return BadRequest();
            }

            try
            {
                var resource = await _clinic.RemoveResourceAsync(id);
                LoggerService.GetInstance.CreateLog($"[DeleteClinic: {id}] The delete process has completed successfully.");
            }
            catch (Exception e)
            {
                LoggerService.GetInstance.CreateLog($"[DeleteClinic: {id}] " + e.Message);
                
                throw new Exception(e.Message);
            }

            return NoContent();
        }
    }
}