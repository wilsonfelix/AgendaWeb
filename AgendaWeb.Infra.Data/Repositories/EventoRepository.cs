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
    /// Classe para implementar as operações de banco de dados para Evento
    /// </summary>
    public class EventoRepository : IEventoRepository
    {
        //Atributo
        private readonly string _connectionString;
        //Método contrutor utilizado para que prove a passagem
        //de valor da connectionstring para a classe de repositório
        public EventoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create(Evento obj)
        {
            var query = @"INSERT INTO EVENTO(ID, NOME, DATA, HORA, DESCRICAO, PRIORIDADE, DATAINCLUSAO, DATAALTERACAO) 
                                 VALUES (@Id, @Nome, @Data, @Hora, @Descricao, @Prioridade, @DataInclusao, @DataAlteracao)";

            //Conecta no DB
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, obj);  
            }

        }

        public void Delete(Evento obj)
        {
            var query = @"DELETE FROM EVENTO WHERE ID = @Id";

            //Conecta no DB
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, obj);
            }
        }

        public List<Evento> GetAll()
        {
            throw new NotImplementedException();

            //var query = "SELECT * FROM EVENTO";

            ////Conecta no DB
            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    connection.Execute(GetAll, query);
            //}
        }
                
        public List<Evento> GetByDatas(DateTime dataMin, DateTime dataMax, int ativo)
        {
            throw new NotImplementedException();
        }

        public Evento GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Evento obj)
        {
            var query = @"UPDATE FROM EVENTO 
                            SET
                                NOME = @Nome, 
                                DATA = @Data, 
                                HORA = @Hora, 
                                DESCRICAO = @Descricao, 
                                PRIORIDADE = @Prioridade, 
                                ${DateTime.Now} = @DataAlteracao,
                                ATIVO = @Ativo
                            WHERE ID = @Id";

            //Conecta no DB
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, obj);
            }
        }
    }
}
