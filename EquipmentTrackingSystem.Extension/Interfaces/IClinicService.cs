using EquipmentTrackingSystem.Domain.Models;
using EquipmentTrackingSystem.Extension.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentTrackingSystem.Extension.Interfaces
{
    public interface IClinicService : IBaseService<Clinic>
    {
        Task<PaginationService<ClinicIndexViewModel>> GetClinicsAsync(ParameterCommonViewModel parameter);
        Task<Clinic> DeleteClinicAsync(int id);
        Task<bool> IsClinicExistsAsync(int id);
    }
}
