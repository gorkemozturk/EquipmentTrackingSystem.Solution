using EquipmentTrackingSystem.Domain.Models;
using EquipmentTrackingSystem.Extension.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentTrackingSystem.Extension.Interfaces
{
    public interface IEquipmentService : IBaseService<Equipment>
    {
        Task<PaginationService<EquipmentIndexViewModel>> GetEquipmentsAsync(ParameterCommonViewModel parameter);
        Task<Equipment> CreateEquipmentAsync(int clinicId, Equipment equipment);
        Task<Equipment> UpdateEquipmentAsync(int id, EquipmentUpdateViewModel equipment);
    }
}
