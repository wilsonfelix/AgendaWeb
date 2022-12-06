using Microsoft.AspNetCore.Mvc;
using AgendaWeb.Presentation.Models;


namespace AgendaWeb.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Index(EventoPesquisaViewModel model)
        //{
        //    if(ModelState.IsValid)
        //    {

        //    }
            
        //    return View();
        //}
    }
}
