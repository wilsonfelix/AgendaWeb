using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaWeb.Infra.Data.Entities
{
    /// <summary>
    /// Classe de entidade para EventoEntity
    /// </summary>
    public class EventoEntity
    {
        #region
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Data { get; set; }
        public TimeSpan? Hora { get; set; }
        public string? Descricao { get; set; }
        public int? Prioridade { get; set; }
        public DateTime? DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int? Ativo { get; set; }
        #endregion
    }
}
