using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End_Pronia.Models
{
    public class PlantImage
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public bool? IsMain { get; set; }
        public int? PlantId { get; set; }
        public Plant Plant { get; set; }

       
    }
}
