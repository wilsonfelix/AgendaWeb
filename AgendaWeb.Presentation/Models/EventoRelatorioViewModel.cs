using AgendaWeb.Infra.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace AgendaWeb.Presentation.Models
{
    public class EventoRelatorioViewModel
    {

        [Required(ErrorMessage = "Por favor, informe a data de início.")]
        public string? DataMin { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de término.")]
        public string? DataMax { get; set; }

        [Required(ErrorMessage = "Por favor, marque Ativo, Inativo ou Todos.")]
        public int? Ativo { get; set; }

        [Required(ErrorMessage = "Por favor, selecione o formato de arquivo desejado.")]
        public int? Formato { get; set;}

        [Required(ErrorMessage = "Por favor, selecione a prioridade.")]
        public int? Prioridade { get; set; }

        //Lista de eventos que será utilizado para exibir
        //na página o resultado da consulta feita no banco
        public List<Evento>? Eventos { get; set; }


    }
}
