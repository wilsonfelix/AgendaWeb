using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaWeb.Infra.Data.Entities
{
    /// <summary>
    /// Classe de entidade para Historico
    /// </summary>
    public class Historico
    {
        #region
        public Guid Id { get; set; }
        public string? Id_Evento { get; set; }
        public string? Nome { get; set; }
        public string? Data { get; set; }
        public string? Hora { get; set; }
        public string? Descricao { get; set; }
        public int? Prioridade { get; set; }
        public string? DataInclusao { get; set; }
        public string? DataAlteracao { get; set; }
        public string? DataExclusao { get; set; }
        public int? Ativo { get; set; }
        #endregion
    }
}
