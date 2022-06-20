using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Back_End_Pronia.DAL;
using Back_End_Pronia.Models;
using Back_End_Pronia.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Back_End_Pronia.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;

        public LayoutService(AppDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public async Task<Setting> GetData()
        {
            Setting setting = await _context.Settings.FirstOrDefaultAsync();
            return setting;
        }

        public BasketVM GetBasket()
        {
            string basketStr = _accessor.HttpContext.Request.Cookies["Basket"];
            if (!string.IsNullOrEmpty(basketStr))
            {
                BasketVM basketData = JsonConvert.DeserializeObject<BasketVM>(basketStr);
                return basketData;
            }
            else
            {
                return null;
            }
        }

        
    }
}
