using EquipmentTrackingSystem.Data;
using EquipmentTrackingSystem.Domain.Models;
using EquipmentTrackingSystem.Extension.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentTrackingSystem.Extension.Services
{
    public class EquipmentService : BaseService<Equipment>, IEquipmentService
    {
        protected readonly IClinicService _clinic;

        public EquipmentService(ETSDbContext context, IClinicService clinic) : base(context)
        {
            _clinic = clinic;
        }

        public async Task<Equipment> CreateEquipmentAsync(int clinicId, Equipment equipment)
        {
            if (!await _clinic.IsClinicExistsAsync(clinicId))
            {
                return null;
            }

            _context.Equipments.Add(equipment);
            await _context.SaveChangesAsync();

            return equipment;
        }

        public async Task<PaginationService<EquipmentIndexViewModel>> GetEquipmentsAsync(ParameterCommonViewModel parameter)
        {
            var equipments = await _context.Equipments.Include(e => e.Clinic).Select(e => new EquipmentIndexViewModel 
            { 
                Id = e.Id, 
                EquipmentName = e.EquipmentName,
                ClinicName = e.Clinic.ClinicName,
                Condition = e.Condition,
                Quantity = e.Quantity,
                UnitPrice = e.UnitPrice,
                SuppliedAt = e.SuppliedAt
            })
            .AsNoTracking()
            .OrderByDescending(e => e.Id)
            .ToListAsync();

            if (!string.IsNullOrEmpty(parameter.Keyword))
            {
                equipments = equipments
                    .Where(c => c.ClinicName.ToLower().Trim().Contains(parameter.Keyword.ToLower().Trim()) ||
                                c.EquipmentName.ToLower().Trim().Contains(parameter.Keyword.ToLower().Trim()))
                    .ToList();
            }

            return PaginationService<EquipmentIndexViewModel>.ToPagedList(equipments, parameter.Page, parameter.Size);
        }

        public async Task<Equipment> UpdateEquipmentAsync(int id, EquipmentUpdateViewModel equipment)
        {
            var resource = await GetResourceAsync(id);

            resource.EquipmentName = equipment.EquipmentName;
            resource.Quantity = equipment.Quantity;
            resource.SuppliedAt = equipment.SuppliedAt;
            resource.UnitPrice = equipment.UnitPrice;
            resource.Condition = equipment.Condition;

            await _context.SaveChangesAsync();

            return resource;
        }
    }
}
