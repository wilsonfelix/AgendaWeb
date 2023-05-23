﻿using Microsoft.VisualBasic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgendaWeb.Presentation.Models
{
    public class EventoCadastroViewModel
    {
        [MinLength(6, ErrorMessage ="Por favor, digite no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage ="Por favor, digite no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome do evento.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data do evento.")]
        public string? Data { get; set; }

        [Required(ErrorMessage = "Por favor, informe a hora do evento.")]
        public string? Hora { get; set; }

        [MaxLength(500, ErrorMessage = "Por favor, digite no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a descrição do evento.")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Qual a prioridade?")]
        public string? Prioridade { get; set; }
    }
}
