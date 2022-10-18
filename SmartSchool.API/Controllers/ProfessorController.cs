using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Componentes;
using SmartSchool.Aplicacao.Professores.Adicionar;
using SmartSchool.Aplicacao.Professores.Alterar;
using SmartSchool.Aplicacao.Professores.Listar;
using SmartSchool.Aplicacao.Professores.ObterPorId;
using SmartSchool.Aplicacao.Professores.Remover;
using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Dto.Dtos.Professores;
using SmartSchool.Dto.Dtos.TratamentoErros;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSchool.API.Controllers
{
	[Produces("application/json")]
	[Route("api/Professores")]
	[EnableCors("PoliticaSmartSchool")]
	public class ProfessorController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ProfessorController(IMediator mediator) => this._mediator = mediator;

		/// <summary>
		/// Obtem listagem de todos os Professores cadastrados
		/// </summary>
		/// <returns>Lista de todos os Professores</returns>
		/// <response code="200">Lista de Professores</response> 
		/// <response code="500">Erro inesperado</response>
		[ProducesResponseType(200, Type = typeof(IEnumerable<ObterProfessorDto>))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		[HttpGet]
		public async Task<IActionResult> ObterTodos()
		{
			var response = await _mediator.Send(new ListarProfessoresQuery());

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Obtém dados de um Professor específico por ID
		/// </summary>
		/// <returns>Dados do Professor solicitado</returns>
		/// <response code="200">Obtem dados do Professor solicitado</response>
		/// <response code="404">Aluno inexistente</response>
		/// <response code="500">Erro inesperado</response>
		[ProducesResponseType(200, Type = typeof(ObterProfessorDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		[HttpGet("{id}")]
		public async Task<IActionResult> ObterPorId([FromRoute(Name = "id")] Guid id)
		{
			var response = await _mediator.Send(new ObterProfessorQuery { Id = id });

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Cria um novo Professor
		/// </summary>
		/// <returns>Http status 201(Created)</returns>
		/// <response code="200">Professor criado com sucesso</response>
		/// <response code="400">Dados inconsistentes para criação do Professor</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public async Task<IActionResult> CriarProfessor([FromBody] AdicionarProfessorCommand professorDto)
		{
			var response = await _mediator.Send(professorDto);

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Efetua alteração de Professor
		/// </summary>
		/// <returns>Http status 204(No Content)</returns>
		/// <response code="204">Professor alterado com Sucesso</response>
		/// <response code="400">Dados para alteração de Professor inconsistentes.</response>
		/// <response code="404">Professor inexistente</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpPut("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public async Task<IActionResult> AlterarProfessor([FromRoute(Name = "id")] Guid id, [FromBody] AlterarProfessorCommand professorDto)
		{
			if (professorDto == null)
				throw new ArgumentNullException(null, "Objeto Professor nulo (não foi informado).");

			if (id.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Identificador do Professor é inválido ou nulo");

			professorDto.ID = id;

			var response = await _mediator.Send(professorDto);

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Efetua alteração de Disponibilidade do Professor para o TCC
		/// </summary>
		/// <returns>Http status 200(No Content)</returns>
		/// <response code="200">Disponibilidade para o TCC alterado com Sucesso</response>
		/// <response code="400">Dados para alteração de Disponibilidade do TCC inconsistentes.</response>
		/// <response code="404">Professor inexistente</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpPatch("{professor-id}/tccs/{disponibilidade-tcc}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public async Task<IActionResult> AlterarDisponibilidadeTccProfessor([FromRoute(Name = "professor-id")] Guid id, [FromRoute(Name = "disponibilidade-tcc")] DisponibilidadeTcc disponibilidadeTcc)
		{
			if (id.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Identificador do Professor é inválido ou nulo");

			var alterarDisponibilidadeTccProfessor = new AlterarDisponibilidadeTccProfessorCommand { ID = id, DisponibilidadeTcc = disponibilidadeTcc };

			var response = await _mediator.Send(alterarDisponibilidadeTccProfessor);

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Exclui um Professor específico
		/// </summary>
		/// <response code="204">Professor excluído com Sucesso</response>
		/// <response code="400">Dados para exclusão do Professor inconsistentes.</response>
		/// <response code="404">Professor inexistente</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpDelete("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public async Task<IActionResult> RemoverProfessor([FromRoute(Name = "id")] Guid id)
		{
			var response = await this._mediator.Send(new RemoverProfessorCommand { ID = id });
			return this.ProcessResult(response);
		}
	}
}
