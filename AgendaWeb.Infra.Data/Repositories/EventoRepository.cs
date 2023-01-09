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
            var query = @"INSERT INTO EVENTO(ID, NOME, DATA, HORA, DESCRICAO, PRIORIDADE, DATAINCLUSAO, DATAALTERACAO, ATIVO) 
                                 VALUES (@Id, @Nome, @Data, @Hora, @Descricao, @Prioridade, @DataInclusao, @DataAlteracao, 1)";

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
            
            var query = @"SELECT * FROM EVENTO
                        ORDER BY DATA DESC, HORA DESC
            ";

            //Conecta no DB
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Evento>(query).ToList();
            }
        }
                
        public List<Evento> GetByDatas(DateTime? dataMin, DateTime? dataMax, int? ativo)
        {
            var query = @"SELECT * FROM EVENTO
                        WHERE ATIVO = @Ativo AND (DATA BETWEEN @dataMin AND @dataMax) 
                        ORDER BY DATA DESC, HORA DESC
            ";

            //Conecta no DB
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Evento>(query, new {ativo, dataMin, dataMax}).ToList();
            }
        }

        public Evento? GetById(Guid id)
        {
            var query = @"SELECT * FROM EVENTO
                        WHERE ID = @Id 
                        ORDER BY dataMin DESC
            ";

            //Conecta no DB
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Evento>(query, new {id}).FirstOrDefault();
            }
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
