﻿using MediatR;
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
		/// Obtem listagem de todos os Tccs cadastrados
		/// </summary>
		/// <returns>Lista de todos os Tccs</returns>
		/// <response code="200">Lista de Tccs</response> 
		/// <response code="500">Erro inesperado</response>
		[ProducesResponseType(200, Type = typeof(IEnumerable<ObterTccsDto>))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		[HttpGet]
		public async Task<IActionResult> ObterTodos()
		{
			var response = await _mediator.Send(new ListarTccsCommand());

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Obtém dados do Tcc específico por ID
		/// </summary>
		/// <returns>Dados do Tcc solicitado</returns>
		/// <response code="200">Obtem dados do Tcc solicitado</response>
		/// <response code="404">Tcc inexistente</response>
		/// <response code="500">Erro inesperado</response>
		[ProducesResponseType(200, Type = typeof(ObterTccDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		[HttpGet("{id}")]
		public async Task<IActionResult> ObterPorId([FromRoute(Name = "id")] Guid id)
		{
			var response = await _mediator.Send(new ObterTccCommand { Id = id });

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Obtém lista de Tccs por Professor
		/// </summary>
		/// <returns>Lista de todos os Tccs por Professor</returns>
		/// <response code="200">Lista de Tccs</response>
		/// <response code="404">Tcc inexistente</response>
		/// <response code="500">Erro inesperado</response>
		[ProducesResponseType(200, Type = typeof(ObterSolicitacoesTccsDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		[HttpGet("professores/{professor-id}")]
		public async Task<IActionResult> ObterPorProfessor([FromRoute(Name = "professor-id")] Guid id, [FromQuery] int status)
		{
			var response = await _mediator.Send(new ListarTccsPorProfessorCommand { ProfessorId = id, StatusTcc = (TccStatus)status });

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Obtém Tcc solicitado por Aluno
		/// </summary>
		/// <returns>Dados do Tcc solicitado</returns>
		/// <response code="200">Obtem dados do Tcc solicitado</response>
		/// <response code="404">Tcc inexistente</response>
		/// <response code="500">Erro inesperado</response>
		[ProducesResponseType(200, Type = typeof(ObterSolicitacoesTccsDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		[HttpGet("alunos/{aluno-id}")]
		public async Task<IActionResult> ObterPorAluno([FromRoute(Name = "aluno-id")] Guid id)
		{
			var response = await _mediator.Send(new ObterTccPorAlunoCommand { AlunoId = id });

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Cria um novo Tcc
		/// </summary>
		/// <returns>Http status 201(Created)</returns>
		/// <response code="200">Tcc criado com sucesso</response>
		/// <response code="400">Dados inconsistentes para criação do Tcc</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public async Task<IActionResult> CriarTcc([FromBody] AdicionarTccCommand tccDto)
		{
			var response = await _mediator.Send(tccDto);

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Efetua alteração de Tcc
		/// </summary>
		/// <returns>Http status 204(No Content)</returns>
		/// <response code="204">Tcc alterado com Sucesso</response>
		/// <response code="400">Dados para alteração de Tcc inconsistentes.</response>
		/// <response code="404">Tcc inexistente</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpPut("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
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
		/// Aprova Tcc solicitado pelo Aluno
		/// </summary>
		/// <returns>Http status 204(No Content)</returns>
		/// <response code="204">Tcc aprovado com Sucesso</response>
		/// <response code="400">Dados para aprovação de Tcc inconsistentes.</response>
		/// <response code="401">Não autorizado</response>
		/// <response code="404">Tcc inexistente</response>
		/// <response code="422">Erro nas regras de negócio.</response>
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
		/// Exclui um Tcc específico
		/// </summary>
		/// <response code="204">Tcc excluído com Sucesso</response>
		/// <response code="400">Dados para exclusão do Tcc inconsistentes.</response>
		/// <response code="404">Tcc inexistente</response>
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
		/// Solicita Tcc
		/// </summary>
		/// <returns>Http status 204(No Content)</returns>
		/// <response code="204">Tcc solicitado com Sucesso</response>
		/// <response code="400">Dados para solicitação de Tcc inconsistentes.</response>
		/// <response code="401">Não autorizado</response>
		/// <response code="404">Tcc inexistente</response>
		/// <response code="422">Erro nas regras de negócio.</response>
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
