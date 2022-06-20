using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End_Pronia.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PlantCategory> PlantCategories { get; set; }
    }
}
