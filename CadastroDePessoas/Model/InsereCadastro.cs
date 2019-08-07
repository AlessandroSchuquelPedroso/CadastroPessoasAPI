using CadastroDePessoas.Model.BancoBd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDePessoas.Model
{
    /// <summary>
    /// classe para receber Objeto e fazer o insert na tabela
    /// </summary>
    public class InsereCadastro
    {
        private TabelaCadastro _cadastroPessoas = new TabelaCadastro();

        public InsereCadastro(TabelaCadastro cadastroPessoas)
        {
            this._cadastroPessoas = cadastroPessoas;
            // envia para a query objeto cadastro
            CadastroNoBanco();
        }

        private void CadastroNoBanco()
        {
            QueryBanco _query = new QueryBanco();
            _query.InsertCadastro(_cadastroPessoas);
        }
    }
}
