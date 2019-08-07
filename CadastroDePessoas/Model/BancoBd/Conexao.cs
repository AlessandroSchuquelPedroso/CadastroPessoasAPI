using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDePessoas.Model.BancoBd
{
    /// <summary>
    /// classe para receber a string de conexão e liberar o acesso para conectar e desconectar e demais propriedades
    /// </summary>
    public class Conexao
    {
        private SqlConnection _conexao;
        public Conexao(string _connection)
        {
            this._conexao = new SqlConnection(_connection);
        }

        public SqlConnection Connection
        {
            get => _conexao;
            set => _conexao = value;
        }

        public void Conectar()
        {
            try
            {
                _conexao.Open();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public void Desconectar()
        {
            try
            {
                _conexao.Close();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
