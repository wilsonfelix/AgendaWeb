using System.ComponentModel.DataAnnotations;

namespace AgendaWeb.Presentation.Models
{
    public class EventoPesquisaViewModel
    {
        [Required (ErrorMessage = "Favor insira uma palavra para a pesquisa")]
        public string? Pesquisa { get; set; }
    }
}
