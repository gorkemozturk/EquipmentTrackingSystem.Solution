using EquipmentTrackingSystem.Data;
using EquipmentTrackingSystem.Domain.Models;
using EquipmentTrackingSystem.Extension.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentTrackingSystem.Extension.Services
{
    public class ClinicService : BaseService<Clinic>, IClinicService
    {
        public ClinicService(ETSDbContext context) : base(context)
        {
        }

        public async Task<Clinic> DeleteClinicAsync(int id)
        {
            var clinic = await GetClinic(id);

            if (clinic.Equipments.Count() > 0)
            {
                throw new Exception("ERR007: Cannot delete the clinic since there existed an equipment which belongs to it.");
            }

            _context.Clinics.Remove(clinic);
            await _context.SaveChangesAsync();

            return clinic;
        }

        public async Task<PaginationService<ClinicIndexViewModel>> GetClinicsAsync(ParameterCommonViewModel parameter)
        {
            var clinics = await _context.Clinics.Include(c => c.Equipments).Select(c => new ClinicIndexViewModel 
            { 
                Id = c.Id, 
                ClinicName = c.ClinicName,
                EquipmentCount = c.Equipments.Count()
            })
            .AsNoTracking()
            .OrderByDescending(c => c.Id)
            .ToListAsync();

            if (!string.IsNullOrEmpty(parameter.Keyword))
            {
                clinics = clinics.Where(c => c.ClinicName.ToLower().Trim().Contains(parameter.Keyword.ToLower().Trim())).ToList();
            }

            return PaginationService<ClinicIndexViewModel>.ToPagedList(clinics, parameter.Page, parameter.Size);
        }

        public async Task<bool> IsClinicExistsAsync(int id)
        {
            return await _context.Clinics.AnyAsync(c => c.Id == id);
        }

        private async Task<Clinic> GetClinic(int id)
        {
            return await _context.Clinics.Include(c => c.Equipments).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
