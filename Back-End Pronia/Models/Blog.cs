using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End_Pronia.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string AdminMessage { get; set; }
        public string AdminTitle { get; set; }
        public string AdminSubTitle { get; set; }
        public string AdminImage { get; set; }
    }
}
