using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendaWeb.Infra.Data.Entities;
using AgendaWeb.Infra.Data.Interfaces;
using Dapper;

namespace AgendaWeb.Infra.Data.Repositories
{
    /// <summary>
    /// Classe para implementar as operações de banco de dados para Historico
    /// </summary>
    public class HistoricoRepository : IHistoricoRepository
    {
        //Atributo
        private readonly string _connectionString;
        //Método contrutor utilizado para que prove a passagem
        //de valor da connectionstring para a classe de repositório
        public HistoricoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create(Historico obj)
        {
            var query = @"INSERT INTO HISTORICO(ID,ID_EVENTO, NOME, DATA, HORA, DESCRICAO, PRIORIDADE, DATAINCLUSAO, DATAALTERACAO,DATAEXCLUSAO, ATIVO) 
                                 VALUES (@Id,@Id_Evento, @Nome, @Data, @Hora, @Descricao, @Prioridade, @DataInclusao, @DataAlteracao,@DataExclusao, @Ativo)";

            //Conecta no DB
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, obj);
            }

        }

        public Historico? GetById(Guid id)
        {
            throw new NotImplementedException();

        }

        public void Delete(Historico entity)
        {
            throw new NotImplementedException();
        }

        public Historico? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Historico entity)
        {
            throw new NotImplementedException();
        }

        public List<Historico>? GetAll(DateTime? DataMin, DateTime? DataMax, int? Ativo)
        {
            throw new NotImplementedException();
        }

        public List<Historico>? GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Historico> GetAll(DateTime? DataMin, DateTime? DataMax)
        {
            throw new NotImplementedException();
        }
    }
}
