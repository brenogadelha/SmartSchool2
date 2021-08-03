using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Aplicacao.Alunos.Interface;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dto.Alunos;
using SmartSchool.Dto.Alunos.Obter;
using SmartSchool.Dto.Dtos.TratamentoErros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : Controller
    {
        private readonly IAlunoServico _alunoServico;

        public AlunoController(IAlunoServico alunoServico)
        {
            this._alunoServico = alunoServico;
        }

        /// <summary>
		/// Obtem listagem de todos os Alunos cadastrados
		/// </summary>
		/// <returns>Lista de todos os Alunos</returns>
		/// <response code="200">Lista de Alunos</response> 
		/// <response code="500">Erro inesperado</response>
        [ProducesResponseType(200, Type = typeof(IEnumerable<ObterAlunoDto>))]
        [ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
        [HttpGet]
        public OkObjectResult ObterTodos()
        {
            return Ok(_alunoServico.Obter());
        }

        /// <summary>
		/// Obtém dados de um Aluno específico por ID
		/// </summary>
		/// <returns>Dados do Aluno solicitado</returns>
		/// <response code="200">Obtem dados do Aluno solicitado</response>
		/// <response code="404">Aluno inexistente</response>
		/// <response code="500">Erro inesperado</response>
        [ProducesResponseType(200, Type = typeof(ObterAlunoDto))]
        [ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
        [ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
        [HttpGet("{id}")]
        public OkObjectResult ObterPorId(Guid id)
        {
            var aluno = _alunoServico.ObterPorId(id);

            if (aluno == null)
                throw new Exception($"Não existe aluno com o id {id} informado.");

            return Ok(aluno);
        }

        //[HttpGet("ByName")]
        //public IActionResult ObterPorNome(string nome, string sobrenome)
        //{
        //    var aluno = _alunoServico.ObterTodosAlunos().FirstOrDefault(a => a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome));

        //    if (aluno == null)
        //        throw new Exception($"Não existe aluno com o nome {nome} informado.");

        //    return Ok(aluno);
        //}
        //// POST api/<AlunoController>


        /// <summary>
        /// Cria um novo Aluno
        /// </summary>
        /// <returns>Http status 201(Created)</returns>
        /// <response code="201">Aluno criado com sucesso</response>
        /// <response code="400">Dados inconsistentes para criação do Aluno</response>
        /// <response code="500">Erro inesperado</response> 
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
        [ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
        public StatusCodeResult Criar([FromBody]AlunoDto aluno)
        {
            this._alunoServico.CriarAluno(aluno);

            return this.StatusCode((int)HttpStatusCode.Created);
        }

        /// <summary>
		/// Efetua alteração de Aluno
		/// </summary>
		/// <returns>Http status 204(No Content)</returns>
		/// <response code="204">Aluno alterado com Sucesso</response>
		/// <response code="400">Dados para alteração de Aluno inconsistentes.</response>
		/// <response code="404">Aluno inexistente</response>
		/// <response code="500">Erro inesperado</response> 
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
        [ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
        [ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
        public StatusCodeResult Alterar(Guid id, [FromBody]AlterarAlunoDto alunoDto)
        {
            if (alunoDto == null)
                throw new ArgumentNullException(null, "Objeto Usuário nulo (não foi informado).");

            if (id.Equals(Guid.Empty))
                throw new ArgumentNullException(null, "Identificador do Usuário é inválido ou nulo");

            alunoDto.ID = id;

            this._alunoServico.AlterarAluno(id, alunoDto);

            return this.StatusCode((int)HttpStatusCode.Created);
        }

        /// <summary>
        /// Exclui um Aluno específico
        /// </summary>
        /// <response code="204">Aluno excluído com Sucesso</response>
        /// /// <response code="400">Dados para exclusão do Usuário inconsistentes.</response>
        /// <response code="404">Usuário inexistente</response>
        /// <response code="500">Erro inesperado</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
        [ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
        [ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
        public StatusCodeResult ExcluirAluno(Guid id)
        {
            this._alunoServico.Remover(id);
            return this.StatusCode(204);
        }
    }
}
