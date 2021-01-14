using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentTrackingSystem.Domain.Models
{
    public class ParameterCommonViewModel
    {
        public string Keyword { get; set; }

        // Pagination
        private const int MaxSize = 50;
        private int ActualSize = 5;

        public int Page { get; set; } = 1;
        public int Size
        { 
            get
            {
                return ActualSize;
            }
            
            set
            {
                ActualSize = (value > MaxSize) ? MaxSize : value;
            }
        }
    }
}
