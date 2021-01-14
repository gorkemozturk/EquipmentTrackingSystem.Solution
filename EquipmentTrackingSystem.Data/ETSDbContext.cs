using EquipmentTrackingSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentTrackingSystem.Data
{
    public class ETSDbContext : DbContext
    {
        public ETSDbContext(DbContextOptions<ETSDbContext> options) : base(options)
        {
        }

        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
    }
}
