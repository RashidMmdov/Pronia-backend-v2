using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End_Pronia.Models
{
    public class Setting
    {
        public int Id { get; set; }
        public string HeaderLogo { get; set; }
        public string FooterLogo { get; set; }
        public string SearchIcon { get; set; }
        public string AccountIcon { get; set; }
        public string WishListIcon { get; set; }
        public string BsketIcon { get; set; }
        public string Phone { get; set; }
        public string AdvertisementImage { get; set; }
        public List<Setting> Settings { get; set; } 
    }
}
