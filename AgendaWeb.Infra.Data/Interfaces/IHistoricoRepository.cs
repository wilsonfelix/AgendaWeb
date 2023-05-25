using AgendaWeb.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaWeb.Infra.Data.Interfaces
{
    /// <summary>
    /// Interface de repositório para a entidade Historico
    /// </summary>
    public interface IHistoricoRepository : IBaseRepository<Historico>
    {

        /// <summary>
        /// Método para retornar todos os eventos dentro de um período de data na tabela de HISTORICO
        /// </summary>
        /// <param name="DataMin">Data de início do período</param>
        /// <param name="DataMax">Data de término do período</param>
        /// <param name="Ativo">Flag 0 para inativo ou 1 para ativo</param>
        /// <returns>Lista de eventos</returns>
        //List<Evento> GetByDatas(DateTime? DataMin, DateTime? DataMax, int? Ativo);
        //List<Evento> GetAll(DateTime? DataMin, DateTime? DataMax, int? Ativo);
        Historico? GetById(Guid Id);
        List<Historico> GetAll(DateTime? DataMin, DateTime? DataMax);
    }
}
