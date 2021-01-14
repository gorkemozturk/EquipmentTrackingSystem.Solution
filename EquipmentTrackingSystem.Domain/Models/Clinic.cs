using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EquipmentTrackingSystem.Domain.Models
{
    public class Clinic
    {
        public int Id { get; set; }

        [Required]
        public string ClinicName { get; set; }

        // Relations
        public virtual ICollection<Equipment> Equipments { get; set; }
    }
}
