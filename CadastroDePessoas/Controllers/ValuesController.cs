using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroDePessoas.Model;
using CadastroDePessoas.Model.BancoBd;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDePessoas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly private QueryBanco _query = new QueryBanco();
        private const string STR_TOKEN = "ca891794-6663-4b72-ac7e-2767cc6e9dc1";

        // busca pessoa por nome ou email pela url
        // GET http://localhost:56526/api/values/busca
        [HttpGet("busca/{token}/{buscaPessoa}")]
        public ActionResult<List<TabelaCadastro>> Get(string token, string buscaPessoa)
        {
            if(token == STR_TOKEN)
            {
                try
                {
                    List<TabelaCadastro> _listTabela = new List<TabelaCadastro>();

                   return Ok(_listTabela = _query.BuscaNomeEmail(buscaPessoa));
                   
                }catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Token Inválido");
            }
        }

        //Lista Cadastro
        // GET http://localhost:56526/api/values/ListarCadastro
        [HttpGet("ListarCadastro/{token}")]
        public ActionResult<List<TabelaCadastro>> GetListar(string token)
        {
            if(token == STR_TOKEN)
            {
                try
                {
                    List<TabelaCadastro> _listaCadastro = new List<TabelaCadastro>();
                    return Ok(_listaCadastro = _query.ListaCadastro());
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
        
            }
            else
            {
                return BadRequest("Token inválido!");
            }

        }

        // cadastro
        // POST http://localhost:56526/api/values/cadastro
        //testar obejeto no postman em body e raw formato json
        [HttpPost("cadastro/{token}")]
        public IActionResult PostCadastro(string token, [FromBody]TabelaCadastro _TabelaCadastro)
        {
            if(token == STR_TOKEN)
            {
                try
                {
                    if (!ModelState.IsValid)
                        return BadRequest("Verifique com o administrador");

                        InsereCadastro _cadastro = new InsereCadastro(_TabelaCadastro);
                        return Ok("Objeto Cadastrado");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Token inválido!");
            }

        }

        // altera o cadastro enviando objeto com o idCadastro e retorna a linha cadastrada
        // testar pelo postman no body e raw com as informações para alterar e o id
        // PUT http://localhost:56526/api/values/alterarCadastro
        [HttpPut("alterarCadastro/{token}")]
        public IActionResult PutAlterar(string token, [FromBody]TabelaCadastro _cadastro)
        {
            if(token == STR_TOKEN)
            {
                try
                {
                    List<TabelaCadastro> _listCadastro = new List<TabelaCadastro>();
                   return Ok(_listCadastro = _query.UpdateCadastroId(_cadastro));

                }catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("token inválido");
            }
        }

        // deleta pelo IdConsulta passada na URL
        // DELETE http://localhost:56526/api/values/deletaCadastro
        [HttpDelete("deletaCadastro/{token}/{idCadastro}")]
        public IActionResult Delete(string token ,int idCadastro)
        {
            if(token == STR_TOKEN)
            {
                try
                {
                    _query.DeletaPeloId(idCadastro);
                    return Ok($"id excluído:{idCadastro}");
                }catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Token inválido");
            }
        }
    }
}
