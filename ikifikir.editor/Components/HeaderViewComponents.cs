using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikir.editor.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        [Authorize]
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
