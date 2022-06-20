using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Back_End_Pronia.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [StringLength(maximumLength:40)]
        public string Title { get; set; }
        [StringLength(maximumLength:100)]
        public string SubTitle { get; set; }
        public string Image { get; set; }
        [Range(0, 100)]
        public byte DisCount { get; set; }
        public string DiscoverUrl { get; set; }
        [Range(0,10)]
        public byte Order { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }

    }
}
