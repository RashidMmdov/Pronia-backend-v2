using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Back_End_Pronia.Areas.ProniaAdmin.Controllers
{
    public class DashBoardController : Controller
    {
        [Area("ProniaAdmin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
