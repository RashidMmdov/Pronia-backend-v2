using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Back_End_Pronia.Models
{
    public class Plant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "decimal(7,3)")]
        public decimal Price { get; set; }
        public string Description { get; set; }
        [Required]
        public string SKU { get; set; }
        public string Shipping { get; set; }
        [Required]
        public string Request { get; set; }
        [Required]
        public string Guarantee { get; set; }
        
        public int? ColorId { get; set; }
        public Color color { get; set; }

        public int? SizeId { get; set; }
        public Size size { get; set; }
        public List<PlantImage> PlantImage { get; set; }
        public List<PlantCategory> PlantCategories { get; set; }
        [NotMapped]

        public IFormFile IsMain { get; set; }
        [NotMapped]
        public List<IFormFile> AnotherImages { get; set; }
        [NotMapped]

        public List<int> ImageIds { get; set; }
        [NotMapped]
        public int MainIds { get; set; }
        [NotMapped]
        public List<int> CategoryIds { get; set; }

    }
}
