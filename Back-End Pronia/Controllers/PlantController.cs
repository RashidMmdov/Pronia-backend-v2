using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back_End_Pronia.DAL;
using Back_End_Pronia.Models;
using Back_End_Pronia.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Back_End_Pronia.Controllers
{
    public class PlantController : Controller
    {
        private readonly AppDbContext _context;

        public PlantController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AddBasket(int id)
        {
            Plant plant = await _context.Plants.FirstOrDefaultAsync(p => p.Id == id);
            if (plant == null) return NotFound();
            string requestStr = HttpContext.Request.Cookies["Basket"];
            BasketVM basket;
            string itemStr;
            if (string.IsNullOrEmpty(requestStr))
            {
                basket = new BasketVM();
                BasketItemVM item = new BasketItemVM
                {
                    plant = plant,
                    Count = 1
                };
                basket.BasketItemVMs.Add(item);
                basket.TotalPrice = item.plant.Price;
                basket.Count = 1;
                itemStr = JsonConvert.SerializeObject(basket);
            }
            else
            {
                basket = JsonConvert.DeserializeObject<BasketVM>(requestStr);
                BasketItemVM existedItem = basket.BasketItemVMs.FirstOrDefault(p => p.plant.Id == id);
                if(existedItem == null)
                {
                    BasketItemVM item = new BasketItemVM
                    {
                        plant = plant,
                        Count = 1
                    };
                    
                    basket.BasketItemVMs.Add(item);
                }
                else
                {
                    existedItem.Count++;
                }
                decimal total = default;
                foreach(BasketItemVM itemVM in basket.BasketItemVMs)
                {
                    total += itemVM.plant.Price + itemVM.Count;
                }
                basket.TotalPrice = total;
                basket.Count = basket.BasketItemVMs.Count;
                itemStr = JsonConvert.SerializeObject(basket);
            }
            HttpContext.Response.Cookies.Append("Basket", itemStr);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult RemoveBasket(int id)
        {
            string reqStr = HttpContext.Request.Cookies["Basket"];
            BasketVM basket;

            if (!string.IsNullOrEmpty(reqStr))
            {
                basket = JsonConvert.DeserializeObject<BasketVM>(reqStr);
                BasketItemVM existed = basket.BasketItemVMs.FirstOrDefault(s => s.plant.Id == id);
                if(existed != null)
                {
                    existed.Count--;
                    if(existed.Count <= 1)
                    {
                        basket.BasketItemVMs.Remove(existed);
                    }
                }
                string itemStr  = JsonConvert.SerializeObject(basket);
            }

            HttpContext.Response.Cookies.Append("Basket",reqStr);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ShowBasket()
        {
            return Content(HttpContext.Request.Cookies["Basket"]);
        }
    }
}
