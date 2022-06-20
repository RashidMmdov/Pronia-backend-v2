using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back_End_Pronia.DAL;
using Back_End_Pronia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_End_Pronia.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class ColorController : Controller
    {
        private readonly AppDbContext _context;

        public ColorController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Color> colors = await _context.Colors.ToListAsync();
            return View(colors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(Color color)
        {
            
            if (!ModelState.IsValid) return NotFound();
            await _context.AddAsync(color);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int id)
        {
            Color color = await _context.Colors.FirstOrDefaultAsync(x => x.Id == id);
            if (color == null) return NotFound();
            return View(color);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Color color = await _context.Colors.FirstOrDefaultAsync(x => x.Id == id);
            if (color == null) return NotFound();
            return View(color);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Edit(int id,Color color)
        {
            Color exitedColor = await _context.Colors.FirstOrDefaultAsync(x => x.Id == id);
            if (exitedColor == null) return NotFound();
            if (id != color.Id) return BadRequest();

            exitedColor.Name = color.Name;

            await _context.SaveChangesAsync();

            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            Color color = await _context.Colors.FirstOrDefaultAsync(x => x.Id == id);
            if (color == null) return NotFound();

            return View(color);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteColor(int id)
        {
            Color color = await _context.Colors.FirstOrDefaultAsync(x => x.Id == id);
            if (color == null) return NotFound();

            _context.Colors.Remove(color);

            await _context.SaveChangesAsync();

            return View(color);

        }
    }
}
