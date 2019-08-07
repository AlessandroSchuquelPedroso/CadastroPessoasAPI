using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDePessoas.Model.BancoBd
{
    /// <summary>
    /// classe de acesso as querys do banco.
    /// </summary>
    public class QueryBanco
    {
        //string de conexão do banco
        private const string STR_CONNECTION = @"Data Source=DESKTOP-KTB5OBK\SQLEXPRESS;Initial Catalog=master;Integrated Security=True";

        //FAZ O INSERT NO CADASTRO
        internal void InsertCadastro(TabelaCadastro cadastroPessoas)
        {
            //verifica se não estiver na regra manda uma exceção
            if (cadastroPessoas.Nome == null && cadastroPessoas.Email == null && cadastroPessoas.Data == null) throw new Exception("Objeto nulo, não será realizado o insert na tabela");

            // FAZ A CONEXAO
            Conexao _connection = new Conexao(STR_CONNECTION);
            using (_connection.Connection)
            {
                _connection.Conectar();
                string queryInsert = "INSERT INTO  Cadastro (nome,email,data) VALUES (@nome,@email,@data)";
                // faz o comando
                using (SqlCommand _cmdInsert = new SqlCommand(queryInsert, _connection.Connection))
                {
                    _cmdInsert.Parameters.AddWithValue("@nome", cadastroPessoas.Nome);
                    _cmdInsert.Parameters.AddWithValue("@email", cadastroPessoas.Email);
                    _cmdInsert.Parameters.AddWithValue("@data", cadastroPessoas.Data);
                    _cmdInsert.ExecuteNonQuery();
                }
                    _connection.Desconectar();
            }
        }

        // FAZ A BUSCA POR NOME OU EMAI
        internal List<TabelaCadastro> BuscaNomeEmail(string buscaPessoa)
        {
            Conexao _connection = new Conexao(STR_CONNECTION);
            TabelaCadastro _tblCadastro = null;
            List<TabelaCadastro> _lisTabela = new List<TabelaCadastro>();
            using (_connection.Connection)
            {
                _connection.Conectar();

                string queryBusca = $"select [IdCadastro],[nome],[email],[data] from Cadastro where (nome like '%{buscaPessoa}%') or ( email like '%{buscaPessoa}%')";

                using (SqlCommand _cmdBuscar = new SqlCommand(queryBusca, _connection.Connection))
                {
                    SqlDataReader reader = _cmdBuscar.ExecuteReader();

                    while (reader.Read())
                    {
                        _tblCadastro = new TabelaCadastro
                        {
                            IdCadastro = reader["IdCadastro"] == DBNull.Value ? 0 : (Int32)reader["IdCadastro"],
                            Nome = reader["nome"] == DBNull.Value ? String.Empty : (String)reader["nome"],
                            Email = reader["email"] == DBNull.Value ? String.Empty : (String)reader["email"],
                            Data = reader["data"] == DBNull.Value ? String.Empty : Convert.ToDateTime(reader["data"]).ToString("dd/MM/yyyy")
                        };
                        _lisTabela.Add(_tblCadastro);
                    }
                    reader.Close();

                    if (_lisTabela == null) throw new Exception("Não encontrado as informações");
                }
                    _connection.Desconectar();
            }

            return _lisTabela;
          
        }

        // faz alteração e retorna a linha alterada
        internal List<TabelaCadastro> UpdateCadastroId(TabelaCadastro cadastro)
        {
            Conexao _connection = new Conexao(STR_CONNECTION);
            TabelaCadastro _tblCadastro = null;
            List<TabelaCadastro> _lisTabela = new List<TabelaCadastro>();
            using (_connection.Connection)
            {
                _connection.Conectar();
                string queryUpdate = $"update Cadastro set nome = @nome, email = @email, data = @data where IdCadastro = {cadastro.IdCadastro}";

                using (SqlCommand _cmdUpdate = new SqlCommand(queryUpdate, _connection.Connection))
                {
                    _cmdUpdate.Parameters.AddWithValue("@nome",cadastro.Nome);
                    _cmdUpdate.Parameters.AddWithValue("@email", cadastro.Email);
                    _cmdUpdate.Parameters.AddWithValue("@data", cadastro.Data);
                    _cmdUpdate.ExecuteNonQuery();
                }

                string queryAlterada = $"select [IdCadastro],[nome],[email],[data] from Cadastro where IdCadastro = {cadastro.IdCadastro}";
                //lista a linha alterada com a alteração feita
                using (SqlCommand _cmdLista = new SqlCommand(queryAlterada, _connection.Connection))
                {
                    SqlDataReader _reader = _cmdLista.ExecuteReader();
                    while (_reader.Read())
                    {
                        _tblCadastro = new TabelaCadastro
                        {
                            IdCadastro = _reader["IdCadastro"] == DBNull.Value ? 0 : (Int32)_reader["IdCadastro"],
                            Nome = _reader["nome"] == DBNull.Value ? String.Empty : (String)_reader["nome"],
                            Email = _reader["email"] == DBNull.Value ? String.Empty : (String)_reader["email"],
                            Data = _reader["data"] == DBNull.Value ? String.Empty : Convert.ToDateTime(_reader["data"]).ToString("dd/MM/yyyy")
                        };
                        

                        _lisTabela.Add(_tblCadastro);
                    }
                    _reader.Close();

                    if (_tblCadastro == null) throw new Exception("Revisar a função updateCadastroId");
                }

                    _connection.Desconectar();
            }

            return _lisTabela;
        }

        // faz o delete pelo do Cadastro
        internal void DeletaPeloId(int idCadastro)
        {
            Conexao _connection = new Conexao(STR_CONNECTION);

            using (_connection.Connection)
            {
                _connection.Conectar();

                string queryDelet = $"DELETE Cadastro where IdCadastro = @id";

                using(SqlCommand _cmdDelete = new SqlCommand(queryDelet, _connection.Connection))
                {
                    _cmdDelete.Parameters.AddWithValue("@id", idCadastro);
                    _cmdDelete.ExecuteNonQuery();
                }

                _connection.Desconectar();
            }
        }

        // LISTA O CADASTRO
        internal List<TabelaCadastro> ListaCadastro()
        {
            Conexao _connection = new Conexao(STR_CONNECTION);
            TabelaCadastro _tblCadastro = null;
            List<TabelaCadastro> _lisTabela = new List<TabelaCadastro>();
            using (_connection.Connection)
            {
                _connection.Conectar();

                string queryListar = "select [IdCadastro],[nome],[email],[data] from Cadastro";

                using (SqlCommand _cmdListar = new SqlCommand(queryListar, _connection.Connection))
                {
                    SqlDataReader _reader = _cmdListar.ExecuteReader();
                    while (_reader.Read())
                    {
                        _tblCadastro = new TabelaCadastro
                        {
                            IdCadastro = _reader["IdCadastro"] == DBNull.Value? 0 : (Int32)_reader["IdCadastro"],
                            Nome = _reader["nome"] == DBNull.Value ? String.Empty : (String)_reader["nome"],
                            Email = _reader["email"] == DBNull.Value ? String.Empty : (String)_reader["email"],
                            Data = _reader["data"] == DBNull.Value ? String.Empty : Convert.ToDateTime(_reader["data"]).ToString("dd/MM/yyyy")
                        };
                       

                        _lisTabela.Add(_tblCadastro);
                    }
                    _reader.Close();
                    if (_tblCadastro == null) throw new Exception("Revisar a função ListaCadastro");
                }

                    _connection.Desconectar();
            }

            return _lisTabela;
        }
    }
}
