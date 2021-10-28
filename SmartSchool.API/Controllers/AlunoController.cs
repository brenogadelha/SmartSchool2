using Microsoft.AspNetCore.Mvc;
using SmartSchool.Aplicacao.Alunos.Interface;
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
		public OkObjectResult ObterTodos() => Ok(_alunoServico.Obter());

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
			var aluno = this._alunoServico.ObterPorId(id);

			if (aluno == null)
				throw new Exception($"Não existe Aluno com o id {id} informado.");

			return Ok(aluno);
		}

		/// <summary>
		/// Obtem listagem de todos os Alunos por parte do Nome ou Sobrenome
		/// </summary>
		/// <returns>Lista de todos os Alunos encontrados</returns>
		/// <response code="200">Lista de Alunos encontrados</response> 
		/// <response code="500">Erro inesperado</response> 
		[HttpGet("{parte-identificador}/nome-a-definir")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<ObterAlunoDto>))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
		public OkObjectResult ObterPorNomeLoginParcial([FromRoute(Name = "parte-identificador")] string busca) => this.Ok(this._alunoServico.ObterPorNomeSobrenomeParcial(busca));

		/// <summary>
		/// Obtém dados do Histórico de um Aluno específico por ID
		/// </summary>
		/// <returns>Dados do Histórico do Aluno solicitadoa</returns>
		/// <response code="200">Obtem dados do Histórico do Aluno solicitado</response>
		/// <response code="404">Aluno inexistente</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpGet("{aluno-id}/historico")]
		[ProducesResponseType(200, Type = typeof(ObterHistoricoAlunoDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
		public OkObjectResult ObterHistoricoPorIdProjeto([FromRoute(Name = "aluno-id")] Guid id) => this.Ok(this._alunoServico.ObterHistoricoPorIdAluno(id));


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
		public StatusCodeResult Criar([FromBody] AlunoDto aluno)
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
		public StatusCodeResult Alterar(Guid id, [FromBody] AlterarAlunoDto alunoDto, [FromQuery(Name = "atualizarDisciplinas")] bool? atualizarDisciplinas = null)
		{
			if (alunoDto == null)
				throw new ArgumentNullException(null, "Objeto Usuário nulo (não foi informado).");

			if (id.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Identificador do Usuário é inválido ou nulo");

			alunoDto.ID = id;

			this._alunoServico.AlterarAluno(id, alunoDto, atualizarDisciplinas);

			return this.StatusCode((int)HttpStatusCode.Created);
		}

		/// <summary>
		/// Exclui um Aluno específico
		/// </summary>
		/// <response code="204">Aluno excluído com Sucesso</response>
		/// <response code="400">Dados para exclusão do Aluno inconsistentes.</response>
		/// <response code="404">Aluno inexistente</response>
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
