using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Componentes;
using SmartSchool.Aplicacao.Alunos.Adicionar;
using SmartSchool.Aplicacao.Alunos.Alterar;
using SmartSchool.Aplicacao.Alunos.Listar;
using SmartSchool.Aplicacao.Alunos.ObterPorId;
using SmartSchool.Aplicacao.Alunos.ObterPorMatricula;
using SmartSchool.Aplicacao.Alunos.ObterPorNome;
using SmartSchool.Aplicacao.Alunos.ObterHistorico;
using SmartSchool.Aplicacao.Alunos.RemoverAluno;
using SmartSchool.Dto.Alunos.Obter;
using SmartSchool.Dto.Dtos.TratamentoErros;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using SmartSchool.Aplicacao.Tccs.Aprovar;
using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Dto.Tccs;
using SmartSchool.Aplicacao.Alunos.Solicitar;

namespace SmartSchool.API.Controllers
{
	[Produces("application/json")]
	[Route("api/Alunos")]
	[EnableCors("PoliticaSmartSchool")]
	public class AlunoController : Controller
	{
		private readonly IMediator _mediator;

		public AlunoController(IMediator mediator)
		{
			this._mediator = mediator;
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
		public async Task<IActionResult> ObterTodos()
		{
			var response = await _mediator.Send(new ListarAlunosCommand());

			return this.ProcessResult(response);
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
		public async Task<IActionResult> ObterPorId([FromRoute(Name = "id")] Guid id)
		{
			var response = await _mediator.Send(new ObterAlunoCommand { Id = id });

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Obtém dados de um Aluno específico por Matrícula
		/// </summary>
		/// <returns>Dados do Aluno solicitado</returns>
		/// <response code="200">Obtem dados do Aluno solicitado</response>
		/// <response code="404">Aluno inexistente</response>
		/// <response code="500">Erro inesperado</response>
		[ProducesResponseType(200, Type = typeof(ObterAlunoDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		[HttpGet("matricula/{codigo}")]
		public async Task<IActionResult> ObterPorMatricula([FromRoute(Name = "codigo")] int matricula)
		{
			var response = await _mediator.Send(new ObterAlunoMatriculaCommand { Matricula = matricula });

			return this.ProcessResult(response);
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
		public async Task<IActionResult> ObterPorNomeSobrenomeParcial([FromRoute(Name = "parte-identificador")] string busca)
		{
			var response = await _mediator.Send(new ObterAlunoNomeCommand { Busca = busca });

			return this.ProcessResult(response);
		}

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
		public async Task<IActionResult> ObterHistoricoPorIdAluno([FromRoute(Name = "aluno-id")] Guid id, [FromQuery(Name = "periodo")] int? periodo = null)
		{
			var response = await _mediator.Send(new ObterHistoricoAlunoCommand { Id = id, Periodo = periodo });

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Cria um novo Aluno
		/// </summary>
		/// <returns>Http status 201(Created)</returns>
		/// <response code="200">Aluno criado com sucesso</response>
		/// <response code="400">Dados inconsistentes para criação do Aluno</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public async Task<IActionResult> CriarAluno([FromBody] AdicionarAlunoCommand aluno)
		{
			var response = await _mediator.Send(aluno);

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Efetua alteração de Aluno
		/// </summary>
		/// <returns>Http status 204(No Content)</returns>
		/// <response code="201">Aluno alterado com Sucesso</response>
		/// <response code="400">Dados para alteração de Aluno inconsistentes.</response>
		/// <response code="404">Aluno inexistente</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpPut("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public async Task<IActionResult> AlterarAluno(Guid id, [FromBody] AlterarAlunoCommand aluno)
		{
			if (aluno == null)
				throw new ArgumentNullException(null, "Objeto Aluno nulo (não foi informado).");

			if (id.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Identificador do Aluno é inválido ou nulo");

			aluno.ID = id;

			var response = await _mediator.Send(aluno);

			return this.ProcessResult(response);
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
		public async Task<IActionResult> RemoverAluno(Guid id)
		{
			var response = await this._mediator.Send(new RemoverAlunoCommand { ID = id });
			return this.ProcessResult(response);
		}

		/// <summary>
		/// Solicita Tcc
		/// </summary>
		/// <returns>Http status 204(No Content)</returns>
		/// <response code="204">Tcc solicitado com Sucesso</response>
		/// <response code="400">Dados para solicitação de Tcc inconsistentes.</response>
		/// <response code="401">Não autorizado</response>
		/// <response code="404">Tcc inexistente</response>
		/// <response code="422">Erro nas regras de negócio.</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpPut("tccs/solicitar")]
		[ProducesResponseType(204)]
		[ProducesResponseType(401, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(422, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public async Task<IActionResult> SolicitarTcc([FromBody] SolicitarTccDto tccDto)
		{
			var response = await _mediator.Send(new SolicitarTccCommand { TccId = tccDto.TccId, AlunosIds = tccDto.AlunosIds, ProfessorId = tccDto.ProfessorId, Solicitacao = tccDto.Solicitacao });

			return this.ProcessResult(response);
		}
	}
}
