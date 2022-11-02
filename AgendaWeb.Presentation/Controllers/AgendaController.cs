using Microsoft.AspNetCore.Mvc;
using AgendaWeb.Presentation.Models;

namespace AgendaWeb.Presentation.Controllers
{
    public class AgendaController : Controller
    {
        public IActionResult Cadastro()
        {
            return View();
        }
        [HttpPost] //Anotation indica que o método será executado no SUBMIT
        public IActionResult Cadastro(EventoCadastroViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }

        
        public IActionResult Consulta()
        {
            return View();
        }
        [HttpPost] //Anotation indica que o método será executado no SUBMIT
        public IActionResult Consulta(EventoCadastroViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }

               
        public IActionResult Relatorio()
        {
            return View();
        }
        [HttpPost] //Anotation indica que o método será executado no SUBMIT
        public IActionResult Relatorio(EventoCadastroViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }


        public IActionResult Edicao()
        {
            return View();
        }
        [HttpPost] //Anotation indica que o método será executado no SUBMIT
        public IActionResult Edicao(EventoCadastroViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
            
        }
    }

}
