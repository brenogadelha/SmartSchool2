using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Componentes;
using SmartSchool.Aplicacao.Tccs.Adicionar;
using SmartSchool.Aplicacao.Tccs.Alterar;
using SmartSchool.Aplicacao.Tccs.Aprovar;
using SmartSchool.Aplicacao.Tccs.Listar;
using SmartSchool.Aplicacao.Tccs.ListarPorProfessor;
using SmartSchool.Aplicacao.Tccs.ObterPorAluno;
using SmartSchool.Aplicacao.Tccs.ObterPorId;
using SmartSchool.Aplicacao.Tccs.Remover;
using SmartSchool.Aplicacao.Tccs.Solicitar;
using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Dto.Dtos.TratamentoErros;
using SmartSchool.Dto.Tccs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSchool.API.Controllers
{
	[Produces("application/json")]
	[Route("api/Tccs")]
	[EnableCors("PoliticaSmartSchool")]
	public class TccController : Controller
	{
		private readonly IMediator _mediator;

		public TccController(IMediator mediator)
		{
			this._mediator = mediator;
		}

		/// <summary>
		/// Obtem listagem de todos os Temas de Tccs cadastrados
		/// </summary>
		/// <returns>Lista de todos os Temas de Tccs</returns>
		/// <response code="200">Lista de Temas de Tccs</response> 
		/// <response code="500">Erro inesperado</response>
		[ProducesResponseType(200, Type = typeof(IEnumerable<ObterTccsDto>))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		[HttpGet]
		public async Task<IActionResult> ObterTodos()
		{
			var response = await _mediator.Send(new ListarTccsQuery());

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Obtém dados do Tema de Tcc específico por ID
		/// </summary>
		/// <returns>Dados do Tema de Tcc solicitado</returns>
		/// <response code="200">Obtem dados do Tema de Tcc solicitado</response>
		/// <response code="404">Tema de Tcc inexistente</response>
		/// <response code="500">Erro inesperado</response>
		[ProducesResponseType(200, Type = typeof(ObterTccDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		[HttpGet("{id}")]
		public async Task<IActionResult> ObterPorId([FromRoute(Name = "id")] Guid id)
		{
			var response = await _mediator.Send(new ObterTccQuery { Id = id });

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Obtém lista de Solicitações de Temas de Tccs por Professor
		/// </summary>
		/// <returns>Lista de todos as solicitações de Temas de Tccs por Professor</returns>
		/// <response code="200">Lista de solicitações de Temas de Tccs</response>
		/// <response code="500">Erro inesperado</response>
		[ProducesResponseType(200, Type = typeof(ObterSolicitacoesTccsDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		[HttpGet("professores/{professor-id}")]
		public async Task<IActionResult> ObterPorProfessor([FromRoute(Name = "professor-id")] Guid id, [FromQuery] int status)
		{
			var response = await _mediator.Send(new ListarTccsPorProfessorQuery { ProfessorId = id, StatusTcc = (TccStatus)status });

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Obtém Tema de Tcc solicitado por Aluno
		/// </summary>
		/// <returns>Dados do Tema de Tcc solicitado</returns>
		/// <response code="200">Obtem dados do Tema de Tcc solicitado</response>
		/// <response code="404">Tema de Tcc inexistente</response>
		/// <response code="500">Erro inesperado</response>
		[ProducesResponseType(200, Type = typeof(ObterSolicitacoesTccsDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		[HttpGet("alunos/{aluno-id}")]
		public async Task<IActionResult> ObterPorAluno([FromRoute(Name = "aluno-id")] Guid id)
		{
			var response = await _mediator.Send(new ObterTccPorAlunoQuery { AlunoId = id });

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Cria um novo Tema para o Tcc
		/// </summary>
		/// <returns>Http status 201(Created)</returns>
		/// <response code="200">Tema para o Tcc criado com sucesso</response>
		/// <response code="400">Dados inconsistentes para criação do Tema de Tcc</response>
		/// <response code="422">Erro de Negócio</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(422, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public async Task<IActionResult> CriarTcc([FromBody] AdicionarTccCommand tccDto)
		{
			var response = await _mediator.Send(tccDto);

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Efetua alteração de Tema para o Tcc
		/// </summary>
		/// <returns>Http status 204(No Content)</returns>
		/// <response code="204">Tema de Tcc alterado com Sucesso</response>
		/// <response code="400">Dados para alteração de Tema do Tcc inconsistentes.</response>
		/// <response code="404">Tema de Tcc inexistente</response>
		/// <response code="422">Erro de Negócio</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpPut("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(422, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public async Task<IActionResult> AlterarTcc([FromRoute(Name = "id")] Guid id, [FromBody] AlterarTccCommand tccDto)
		{
			if (tccDto == null)
				throw new ArgumentNullException(null, "Objeto Curso nulo (não foi informado).");

			if (id.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Identificador de Curso é inválido ou nulo");

			tccDto.ID = id;

			var response = await _mediator.Send(tccDto);

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Aprova Tema de Tcc solicitado pelo Aluno
		/// </summary>
		/// <returns>Http status 204(No Content)</returns>
		/// <response code="204">Tema para o Tcc aprovado com Sucesso</response>
		/// <response code="400">Dados para aprovação de Tcc inconsistentes.</response>
		/// <response code="401">Não autorizado</response>
		/// <response code="404">Tema de Tcc inexistente</response>
		/// <response code="422">Erro de negócio.</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpPut("{professor-id}/professores/{aluno-id}/alunos/aprovar")]
		[ProducesResponseType(204)]
		[ProducesResponseType(401, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(422, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public async Task<IActionResult> AprovarTcc([FromRoute(Name = "aluno-id")] Guid alunoId, [FromRoute(Name = "professor-id")] Guid professorId, [FromBody] AprovarTccDto tccDto)
		{
			var response = await _mediator.Send(new AprovarTccCommand { AlunoId = alunoId, ProfessorId = professorId, StatusTcc = (TccStatus)tccDto.Status, RespostaSolicitacao = tccDto.RespostaSolicitacao });

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Exclui um Tema de Tcc específico
		/// </summary>
		/// <response code="204">Tema de Tcc excluído com Sucesso</response>
		/// <response code="400">Dados para exclusão do Tcc inconsistentes.</response>
		/// <response code="404">Tema de Tcc inexistente</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpDelete("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public async Task<IActionResult> RemoverTcc([FromRoute(Name = "id")] Guid id)
		{
			var response = await this._mediator.Send(new RemoverTccCommand { ID = id });
			return this.ProcessResult(response);
		}

		/// <summary>
		/// Solicita Tema para o Tcc
		/// </summary>
		/// <returns>Http status 204(No Content)</returns>
		/// <response code="204">Tema para o Tcc solicitado com Sucesso</response>
		/// <response code="400">Dados para solicitação de Tema para o Tcc inconsistentes.</response>
		/// <response code="401">Não autorizado</response>
		/// <response code="404">Tcc inexistente</response>
		/// <response code="422">Erro de negócio.</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpPut("solicitar")]
		[ProducesResponseType(204)]
		[ProducesResponseType(401, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(422, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public async Task<IActionResult> SolicitarTcc([FromBody] SolicitarTccCommand tccDto)
		{
			var response = await _mediator.Send(tccDto);

			return this.ProcessResult(response);
		}
	}
}
