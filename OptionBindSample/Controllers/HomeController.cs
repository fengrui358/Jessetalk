using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace OptionBindSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly Class _myClass;

        public HomeController(IOptions<Class> classAccess)
        {
            _myClass = classAccess.Value;
        }

        public IActionResult Index()
        {
            return View(_myClass);
        }
    }
}