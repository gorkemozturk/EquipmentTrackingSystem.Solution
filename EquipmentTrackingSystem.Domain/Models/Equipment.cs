using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EquipmentTrackingSystem.Domain.Models
{
    public class Equipment
    {
        public int Id { get; set; }

        [Required]
        public string EquipmentName { get; set; }
        public DateTime? SuppliedAt { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, Double.MaxValue)]
        public double UnitPrice { get; set; }

        [Required]
        [Range(0.0, 100.00)]
        public double Condition { get; set; }

        // Foreign Keys
        public int ClinicId { get; set; }

        // Relations
        [ForeignKey("ClinicId")]
        public virtual Clinic Clinic { get; set; }
    }
}
