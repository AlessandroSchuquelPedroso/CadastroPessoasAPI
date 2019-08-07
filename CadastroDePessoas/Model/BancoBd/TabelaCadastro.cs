using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDePessoas.Model.BancoBd
{
    /// <summary>
    /// classe de propriedade da tabela do banco de dados[Cadastro]
    /// </summary>
    public class TabelaCadastro
    {
        public int IdCadastro { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Data { get; set; }
    }
}
