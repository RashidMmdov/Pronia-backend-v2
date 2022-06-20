using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Back_End_Pronia.DAL;
using Back_End_Pronia.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Back_End_Pronia.Extensions;
using Back_End_Pronia.Utilities;

namespace Back_End_Pronia.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public SliderController(AppDbContext context,IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            List<Slider> slider = await _context.Sliders.ToListAsync();
            return View(slider);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid) return View();
            if(slider.Photo != null)
            {
                if (!slider.Photo.IsValid(1))
                {
                    ModelState.AddModelError("Photo", "Please choose right format");
                }



                slider.Image = await slider.Photo.PathFiles(_environment.WebRootPath,@"assets\images\website-images");
                await _context.Sliders.AddAsync(slider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("Photo", "Please choose file");
                return View();
            }
            
        }
        public async Task<IActionResult> Detail(int id)
        {
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(p => p.Id == id);
            if (slider == null) return NotFound();

            return View(slider);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(p => p.Id == id);
            if (slider == null) return NotFound();
            return View(slider);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Edit(int id,Slider slider)
        {
            Slider existedSlider = await _context.Sliders.FirstOrDefaultAsync(p => p.Id == id);
            if (existedSlider == null) return NotFound();
            if(id != slider.Id)
            {
                return BadRequest();
            }
           
            
            
            existedSlider.Title = slider.Title;
            existedSlider.SubTitle = slider.SubTitle;
            existedSlider.Photo = slider.Photo;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int id)
        {
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(p => p.Id == id);
            if (slider == null) return NotFound();
            return View(slider);
        }
       
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteSlider(int id)
        {
            Slider existedSlider = await _context.Sliders.FirstOrDefaultAsync(p => p.Id == id);
            if (existedSlider == null) return NotFound();

            _context.Remove(existedSlider);
            await _context.SaveChangesAsync();

            return View(existedSlider);
        }
    }
}
