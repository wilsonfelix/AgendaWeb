using Microsoft.AspNetCore.Mvc;
using AgendaWeb.Presentation.Models;
using AgendaWeb.Infra.Data.Interfaces;
using AgendaWeb.Infra.Data.Entities;

namespace AgendaWeb.Presentation.Controllers
{
    public class AgendaController : Controller
    {
        //Atributo
        private readonly IEventoRepository _eventoRepository;

        //Construtor para inicializar o atributo
        public AgendaController(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        public IActionResult Cadastro()
        {
            return View();
        }
        [HttpPost] //Anotation indica que o método será executado no SUBMIT
        public IActionResult Cadastro(EventoCadastroViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    var evento = new Evento
                    {
                        Id = Guid.NewGuid(),
                        Nome = model.Nome,
                        Data = Convert.ToDateTime(model.Data),
                        Hora = TimeSpan.Parse(model.Hora), 
                        Descricao = model.Descricao,
                        Prioridade = Convert.ToInt32(model.Prioridade),
                        DataInclusao = DateTime.Now,
                        DataAlteracao = DateTime.Now
                        
                    };
                    //Gravando no BD
                    _eventoRepository.Create(evento);
                    TempData["Mensagem"] = $"Evento {evento.Nome}, Cadastro realizado com sucesso!";
                    ModelState.Clear(); //Limpa os campos do formulário (model)

                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                    
                }
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
