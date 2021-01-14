using EquipmentTrackingSystem.Data;
using EquipmentTrackingSystem.Domain.Models;
using EquipmentTrackingSystem.Extension.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EquipmentTrackingSystem.Extension.Services
{
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly ETSDbContext _context;

        public BaseService(ETSDbContext context)
        {
            _context = context;
        }

        public async Task<T> CreateResourceAsync(T resource)
        {
            _context.Set<T>().Add(resource);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("ERR001: An error occurred while creating the resource.");
            }

            return resource;
        }

        public async Task<T> GetResourceAsync(int id)
        {
            var resource = await _context.Set<T>().FindAsync(id);

            if (resource == null)
            {
                return null;
            }

            return resource;
        }

        public async Task<PaginationService<T>> GetResourcesAsync(ParameterCommonViewModel parameter)
        {
            var resources = await _context.Set<T>().ToListAsync();

            return PaginationService<T>.ToPagedList(resources, parameter.Page, parameter.Size);
        }

        public async Task<T> RemoveResourceAsync(int id)
        {
            var resource = await GetResourceAsync(id);
            _context.Set<T>().Remove(resource);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("ERR003: An error occurred while removing the resource.");
            }

            return resource;
        }

        public async Task<T> UpdateResourceAsync(T resource)
        {
            _context.Entry(resource).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("ERR004: An error occurred while updating the resource.");
            }

            return resource;
        }
    }
}
