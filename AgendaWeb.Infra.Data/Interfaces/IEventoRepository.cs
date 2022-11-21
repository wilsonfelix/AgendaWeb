using AgendaWeb.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaWeb.Infra.Data.Interfaces
{
    /// <summary>
    /// Interface de repositório para a entidade Evento
    /// </summary>
    public interface IEventoRepository : IBaseRepository<Evento>
    {
         
        /// <summary>
        /// Método para retornar todos os eventos dentro de um período de data
        /// </summary>
        /// <param name="dataMin">Data de início do período</param>
        /// <param name="dataMax">Data de término do período</param>
        /// <param name="ativo">Flag 0 para inativo ou 1 para ativo</param>
        /// <returns>Lista de eventos</returns>
        List<Evento> GetByDatas(DateTime dataMin, DateTime dataMax, int ativo);
    }
}
