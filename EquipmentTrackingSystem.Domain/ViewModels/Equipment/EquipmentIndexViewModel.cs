using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentTrackingSystem.Domain.Models
{
    public class EquipmentIndexViewModel
    {
        public int Id { get; set; }
        public string EquipmentName { get; set; }
        public string ClinicName { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Condition { get; set; }
        public DateTime? SuppliedAt { get; set; }
    }
}
