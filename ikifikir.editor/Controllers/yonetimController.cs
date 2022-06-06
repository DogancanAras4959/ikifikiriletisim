using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikir.editor.Controllers
{
    public class yonetimController : Controller
    {
        [Authorize]
        public IActionResult anasayfa()
        {
            return View();
        }
    }
}
