
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back_End_Pronia.DAL;
using Back_End_Pronia.Models;
using Back_End_Pronia.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Back_End_Pronia.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            List<Slider> sliders = _context.Sliders.OrderBy(s => s.Order).Take(3).ToList();
            List<Client> clients = _context.Clients.ToList();
            List<Blog> blogs = _context.Blogs.ToList();
            List<Plant> plants = _context.Plants.ToList();
            List<PlantImage> plantImages = _context.PlantImages.ToList();
            HomeVM model = new HomeVM
            {
                Clients = clients,
                Sliders = sliders,
                Blogs = blogs,
                PlantImages = plantImages,
                Plants = plants
            };
            return View(model);
        }
    }
}
