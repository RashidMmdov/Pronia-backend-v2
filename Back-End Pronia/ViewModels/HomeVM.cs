using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back_End_Pronia.Models;

namespace Back_End_Pronia.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Client> Clients { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Plant> Plants { get; set; }
        public List<Color> Colors { get; set; }
        public List<PlantImage> PlantImages { get; set; }
        public List<Size> Sizes { get; set; }
        public List<Category> Categories { get; set; }
        public List<PlantCategory> PlantCategories { get; set; }
        public List<Setting> Settings { get; set; }
        public List<SocialMedia> SocialMedias { get; set; }
    }
}
