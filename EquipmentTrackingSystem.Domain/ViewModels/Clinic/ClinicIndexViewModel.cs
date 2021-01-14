using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentTrackingSystem.Domain.Models
{
    public class ClinicIndexViewModel
    {
        public int Id { get; set; }
        public string ClinicName { get; set; }
        public int EquipmentCount { get; set; }
    }
}
