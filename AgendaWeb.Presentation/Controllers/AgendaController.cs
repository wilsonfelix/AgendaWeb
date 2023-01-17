﻿using AgendaWeb.Infra.Data.Entities;
using AgendaWeb.Infra.Data.Interfaces;
using AgendaWeb.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgendaWeb.Presentation.Controllers
{
    public class AgendaController : Controller
    {
        //atributo
        private readonly IEventoRepository _eventoRepository;

        //construtor para inicializar o atributo
        public AgendaController(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
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
                        //realizando a consulta de eventos
                        model.Eventos = _eventoRepository.GetByDatas(DataMin, DataMax, model.Ativo);

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
                model.Data = Convert.ToDateTime(evento.Data).ToString("yyyy-MM-dd");
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
                    //converter as datas
                    var Data = Convert.ToDateTime(model.Data).ToString("ddMMyyyy");
                    var DataSys = DateTime.Now.ToString("ddMMyyyy");
                    var testedata = Convert.ToDateTime(Data);
                    var testedatasys= Convert.ToDateTime(DataSys);

                    //verificando se a data de inicio é menor ou igual a data de fim
                    if (testedata <= testedatasys)
                    {
                        TempData["MensagemErro"] = "A data de início deve ser menor ou igual a data de término.";
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

        public IActionResult Relatorio()
        {
            return View();
        }
    }
}
