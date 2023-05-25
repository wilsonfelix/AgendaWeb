using AgendaWeb.Infra.Data.Entities;
using AgendaWeb.Infra.Data.Interfaces;
using AgendaWeb.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace AgendaWeb.Presentation.Controllers
{
    public class AgendaController : Controller
    {
        //atributo
        private readonly IEventoRepository _eventoRepository;
        private readonly IHistoricoRepository _historicoRepository;
        //construtor para inicializar o atributo
        public AgendaController(IEventoRepository eventoRepository, IHistoricoRepository historicoRepository)
        {
            _eventoRepository = eventoRepository;
            _historicoRepository = historicoRepository;
        }
        
             

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost] //Annotation indica que o método será executado no SUBMIT
        public IActionResult Cadastro(EventoCadastroViewModel model)
        {
            
            //verificar se todos os campos passaram nas regras de validação
            if (ModelState.IsValid)
            {
                try
                {
                    //Converte a data
                    var Data = Convert.ToDateTime(model.Data).ToString("dd/MM/yyyy");
                    var DataSys = DateTime.Now.ToString("dd/MM/yyyy");
                    var DataCadEvento = Convert.ToDateTime(Data);
                    var DataSysConv = Convert.ToDateTime(DataSys);

                    //Converte a hora
                    var Hora = Convert.ToDateTime(model.Hora).ToString("HH:mm:ss");
                    var HoraSys = DateTime.Now.ToString("HH:mm:ss");
                    var HoraCadEvento = Convert.ToDateTime(Hora);
                    var HoraSysConv = Convert.ToDateTime(HoraSys);

                    //verificando se a data de inicio é menor ou igual a data de fim
                    if (DataCadEvento < DataSysConv)
                    {
                        TempData["MensagemErro"] = "A data de início do evento não pode ser anterior a data atual.";
                    }
                    else if(HoraCadEvento < HoraSysConv)
                    {
                        TempData["MensagemErro"] = "Não se pode agendar um evento com a hora atual. Por favor selecione ao menos 1 minuto a mais.";
                    }
                    else
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

                        //gravando no banco de dados
                        _eventoRepository.Create(evento);

                        TempData["MensagemSucesso"] = $"Evento {evento.Nome}, cadastrado com sucesso.";
                        ModelState.Clear(); //limpando os campos do formulário (model)
                    }
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros de validação no preenchimento do formulário.";
            }

            return View();
        }

        public IActionResult Consulta()
        {
            return View();
        }

        [HttpPost] //Annotation indica que o método será executado no SUBMIT
        public IActionResult Consulta(EventoConsultaViewModel model)
        {
                        
            //verificar se todos os campos da model passaram nas validações
            if (ModelState.IsValid)
            {

                try
                {
                    //converter as datas
                    var DataMin = Convert.ToDateTime(model.DataMin);
                    var DataMax = Convert.ToDateTime(model.DataMax);

                    //verificando se a data de inicio é menor ou igual a data de fim
                    if (DataMin <= DataMax)
                    {
                        if (model.Ativo == 3) 
                        {
                            model.Eventos = _eventoRepository.GetAllNot(DataMin, DataMax);
                        }
                        else
                        {
                            //realizando a consulta de eventos
                            model.Eventos = _eventoRepository.GetByDatas(DataMin, DataMax, model.Ativo);
                        }
                        

                        //verificando se algum evento foi obtido
                        if (model.Eventos.Count > 0)
                        {
                            TempData["MensagemSucesso"] = $"{model.Eventos.Count} evento(s) obtido(s) para a pesquisa";
                        }
                        else
                        {
                            TempData["MensagemAlerta"] = "Nenhum evento foi encontrado para a pesquisa realizada.";
                        }
                    }
                    else
                    {
                        TempData["MensagemErro"] = "A data de início deve ser menor ou igual a data de término.";
                    }
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros de validação no preenchimento do formulário.";
            }

            //voltando para a página
            return View(model);
        }

        public IActionResult Edicao(Guid Id)
        {
            var model = new EventoEdicaoViewModel();

            try
            {
                //consultar o evento no banco de dados atraves do ID
                var evento = _eventoRepository.GetById(Id);

                //preencher os dados da classe model com as informações do evento
                model.Id = evento.Id;
                model.Nome = evento.Nome;
                model.Data = Convert.ToDateTime(evento.Data).ToString("dd-MM-yyyy");
                model.Hora = evento.Hora.ToString();
                model.Descricao = evento.Descricao;
                model.Prioridade = evento.Prioridade.ToString();
                model.Ativo = evento.Ativo;
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            //enviando o model para a página
            return View(model);
        }

        /*
         * Método para receber o SUBMIT da página de edição (POST)
         */
        [HttpPost]
        public IActionResult Edicao(EventoEdicaoViewModel model)
        {
            //verificar se todos os campos passaram nas regras de validação
            if (ModelState.IsValid)
            {
                try
                {
                    //Converte a data
                    var Data = Convert.ToDateTime(model.Data).ToString("dd/MM/yyyy");
                    var DataSys = DateTime.Now.ToString("dd/MM/yyyy");
                    var DataCadEvento = Convert.ToDateTime(Data);
                    var DataSysConv = Convert.ToDateTime(DataSys);

                    //Converte a hora
                    var Hora = Convert.ToDateTime(model.Hora).ToString("HH:mm:ss");
                    var HoraSys = DateTime.Now.ToString("HH:mm:ss");
                    var HoraCadEvento = Convert.ToDateTime(Hora);
                    var HoraSysConv = Convert.ToDateTime(HoraSys);

                    //verificando se a data de inicio é menor ou igual a data de fim
                    if (DataCadEvento < DataSysConv)
                    {
                        TempData["MensagemErro"] = "A data de início do evento não pode ser atualizada com a data anterior a atual.";
                    }
                    else if (DataCadEvento == DataSysConv && HoraCadEvento < HoraSysConv )
                    {
                        TempData["MensagemErro"] = "Não se pode atualizar um evento com a hora atual. Por favor selecione ao menos 1 minuto a mais.";
                    }
                    else
                    {
                        //obtendo os dados do evento no banco de dados..
                        var evento = _eventoRepository.GetById(model.Id);

                        //modificar os dados do evento
                        evento.Nome = model.Nome;
                        evento.Data = Convert.ToDateTime(model.Data);
                        evento.Hora = TimeSpan.Parse(model.Hora);
                        evento.Descricao = model.Descricao;
                        evento.Prioridade = Convert.ToInt32(model.Prioridade);
                        evento.Ativo = model.Ativo;
                        evento.DataAlteracao = DateTime.Now;

                        //atualizando no banco de dados
                        _eventoRepository.Update(evento);

                        TempData["MensagemSucesso"] = "Dados do evento atualizado com sucesso.";
                        return RedirectToAction("Consulta");
                        
                       
                    }
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros de validação no preenchimento do formulário.";
            }

            return View();
        }

        public IActionResult Exclusao(Guid id)
        {

            try
            {
                //obtendo os dados do evento no banco de dados..
                var evento = _eventoRepository.GetById(id);
                var historico = new Historico
                {
                    Id = Guid.NewGuid(),
                    Id_Evento = Convert.ToString(evento.Id),
                    Nome = evento.Nome,
                    Data = Convert.ToDateTime(evento.Data).ToString("dd-MM-yyyy"),
                    Hora = evento.Hora.ToString(),
                    Descricao = evento.Descricao,
                    Prioridade = evento.Prioridade,
                    DataInclusao = Convert.ToDateTime(evento.DataInclusao).ToString("dd-MM-yyyy"),
                    DataAlteracao = Convert.ToDateTime(evento.DataAlteracao).ToString("dd-MM-yyyy"),
                    DataExclusao = Convert.ToDateTime(DateTime.Now).ToString("dd-MM-yyyy"),
                    Ativo = evento.Ativo
                };
                //gravando no banco de dados
                _historicoRepository.Create(historico);

                //deleta registro de evento//
                _eventoRepository.Delete(evento);
                TempData["MensagemAlerta"] = $"Evento '{evento.Nome}' excluido com sucesso!";
            
                
                 
            }
            catch (Exception e)
            {

                TempData["MensagemAlerta"] = e.Message;

            }
            //redireciona para a pagina de consulta

            return RedirectToAction("Consulta");
        }

        public IActionResult Relatorio()
        {
            return View();
        }
        
    }
}
