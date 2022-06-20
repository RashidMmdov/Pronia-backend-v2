using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back_End_Pronia.DAL;
using Back_End_Pronia.Extensions;
using Back_End_Pronia.Models;
using Back_End_Pronia.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_End_Pronia.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class PlantController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public PlantController(AppDbContext context,IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            List<Plant> plants = await _context.Plants.Include(p => p.PlantImage).ToListAsync();
            return View(plants);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Colors = await _context.Colors.ToListAsync();
            ViewBag.Sizes = await _context.Sizes.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Create(Plant plant)
        {
            ViewBag.Colors = await _context.Colors.ToListAsync();
            ViewBag.Sizes = await _context.Sizes.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();

            if (ModelState.IsValid) return View();
            if(plant.IsMain == null || plant.AnotherImages == null)
            {
                ModelState.AddModelError("", "Please enter image format");
                return View();
            }
            if (!plant.IsMain.IsValid(1))
            {
                ModelState.AddModelError("IsMain", "Please choose image file");
                return View();
            }

            plant.PlantImage = new List<PlantImage>();

            foreach(var another in plant.AnotherImages)
            {
                if (!another.IsValid(1))
                {
                    ModelState.AddModelError("AnotherImages", "Please choose image file");
                    return View();
                }
            }

           

            PlantImage newPlant = new PlantImage
            {
                ImagePath = await plant.IsMain.PathFiles(_environment.WebRootPath, @"assets\images\website-images"),
                IsMain = true,
                Plant = plant
            };

            plant.PlantImage.Add(newPlant);

            foreach(var another in plant.AnotherImages)
            {
                PlantImage anotherPlant = new PlantImage
                {
                    ImagePath = await another.PathFiles(_environment.WebRootPath, @"assets\images\website-images"),
                    IsMain = false,
                    Plant = plant
                };

                plant.PlantImage.Add(anotherPlant);
            }

            await _context.Plants.AddAsync(plant);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Colors = await _context.Colors.ToListAsync();
            ViewBag.Sizes = await _context.Sizes.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();

            Plant plant = await _context.Plants.Include(p => p.PlantImage).Include(p => p.PlantCategories).FirstOrDefaultAsync(p => p.Id == id);
            return View(plant);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Edit(int id,Plant plant)
        {
            ViewBag.Colors = await _context.Colors.ToListAsync();
            ViewBag.Sizes = await _context.Sizes.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();

            Plant existed = await _context.Plants.Include(p => p.PlantImage).Include(p => p.PlantCategories).FirstOrDefaultAsync(p => p.Id == id);
            if (existed == null) return NotFound();

            if(plant.AnotherImages == null && plant.ImageIds == null)
            {
                ModelState.AddModelError("", "You cannot delete all files such added new file");
                return View(existed);
            }
            if (plant.IsMain == null && plant.MainIds == null)
            {
                ModelState.AddModelError("", "You cannot delete all files such added new file");
                return View(existed);
            }

            List<PlantCategory> removaCategory = await _context.PlantCategories.Where(pc => !plant.CategoryIds.Contains(pc.CategoryId)).ToListAsync();
            existed.PlantCategories.RemoveAll(pc => removaCategory.Any(rc => rc.Id == pc.Id));

            List<PlantImage> removeable = await _context.PlantImages.Where(p => p.IsMain == false && !plant.ImageIds.Contains(p.Id)).ToListAsync();

            List<PlantImage> mainImage = await _context.PlantImages.Where(p => p.IsMain == true && plant.MainIds != p.Id).ToListAsync();

            existed.PlantImage.RemoveAll(p => mainImage.Any(pr => pr.Id == p.Id));


            existed.PlantImage.RemoveAll(p => removeable.Any(ri => ri.Id == p.Id));

            foreach(var mainimage in mainImage)
            {
                FileUtilities.FileDelete(_environment.WebRootPath, @"assets\images\website-images", mainimage.ImagePath);
            }

            foreach(var image in removeable)
            {
                FileUtilities.FileDelete(_environment.WebRootPath, @"assets\images\website-images", image.ImagePath);
            }

            foreach(var item in plant.CategoryIds)
            {
                PlantCategory exitedCategory = existed.PlantCategories.FirstOrDefault(p => p.Id == item);
                if(exitedCategory == null)
                {
                    PlantCategory plantCategory = new PlantCategory
                    {
                        PlantId = existed.Id,
                        CategoryId = item
                    };
                    existed.PlantCategories.Add(plantCategory);
                }
            }

            if(plant.AnotherImages != null)
            {
                foreach(var image in plant.AnotherImages)
                {
                    PlantImage plantimage = new PlantImage
                    {
                        ImagePath = await image.PathFiles(_environment.WebRootPath, @"assets\images\website-images"),
                        IsMain = false,
                        PlantId = existed.Id
                    };
                    existed.PlantImage.Add(plantimage);
                }
            }
            if(plant.IsMain != null)
            {
                PlantImage image = new PlantImage
                {
                    ImagePath = await plant.IsMain.PathFiles(_environment.WebRootPath, @"assets\images\website-images"),
                    IsMain = true,
                    PlantId = existed.Id
                };
                existed.PlantImage.Add(image);
            }

            _context.Entry(existed).CurrentValues.SetValues(plant);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            ViewBag.Colors = await _context.Colors.ToListAsync();
            ViewBag.Sizes = await _context.Sizes.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();

            Plant plant = await _context.Plants.Include(p => p.PlantImage).FirstOrDefaultAsync(p => p.Id == id);

            return View(plant);
        }
        public async Task<IActionResult> Delete(int id)
        {
            ViewBag.Colors = await _context.Colors.ToListAsync();
            ViewBag.Sizes = await _context.Sizes.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();

            Plant plant = await _context.Plants.Include(p => p.PlantImage).FirstOrDefaultAsync(p => p.Id == id);
            

            if (plant == null)
            {
                return NotFound();
            }
            return View(plant);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeletePlant(int id)
        {
            ViewBag.Colors = await _context.Colors.ToListAsync();
            ViewBag.Sizes = await _context.Sizes.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();   

            Plant plant = await _context.Plants.Include(p => p.PlantImage).FirstOrDefaultAsync(p => p.Id == id);

            if (plant == null) return NotFound();

            _context.Plants.Remove(plant);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
