using EquipmentTrackingSystem.Domain.Models;
using EquipmentTrackingSystem.Extension.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentTrackingSystem.Extension.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        Task<PaginationService<T>> GetResourcesAsync(ParameterCommonViewModel parameter);
        Task<T> GetResourceAsync(int id);
        Task<T> CreateResourceAsync(T resource);
        Task<T> UpdateResourceAsync(T resource);
        Task<T> RemoveResourceAsync(int id);
    }
}
