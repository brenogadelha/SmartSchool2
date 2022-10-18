using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Componentes;
using SmartSchool.Aplicacao.Disciplinas.Adicionar;
using SmartSchool.Aplicacao.Disciplinas.Alterar;
using SmartSchool.Aplicacao.Disciplinas.Listar;
using SmartSchool.Aplicacao.Disciplinas.ObterPorId;
using SmartSchool.Aplicacao.Disciplinas.ObterProfessores;
using SmartSchool.Aplicacao.Disciplinas.Remover;
using SmartSchool.Dto.Disciplinas.Obter;
using SmartSchool.Dto.Dtos.Professores;
using SmartSchool.Dto.Dtos.TratamentoErros;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSchool.API.Controllers
{
	[Produces("application/json")]
	[Route("api/Disciplinas")]
	[EnableCors("PoliticaSmartSchool")]
	public class DisciplinaController : Controller
	{
		private readonly IMediator _mediator;

		public DisciplinaController( IMediator mediator)
		{
			this._mediator = mediator;
		}

		/// <summary>
		/// Obtem listagem de todos as Disciplinas cadastradas
		/// </summary>
		/// <returns>Lista de todas as Disciplinas</returns>
		/// <response code="200">Lista de Disciplinas</response> 
		/// <response code="500">Erro inesperado</response>
		[ProducesResponseType(200, Type = typeof(IEnumerable<ObterDisciplinaDto>))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		[HttpGet]
		public async Task<IActionResult> ObterTodos()
		{
			var response = await _mediator.Send(new ListarDisciplinasQuery());

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Obtém dados de Disciplina específica por ID
		/// </summary>
		/// <returns>Dados da Disciplina solicitada</returns>
		/// <response code="200">Obtem dados da Disciplina solicitada</response>
		/// <response code="404">Disciplina inexistente</response>
		/// <response code="500">Erro inesperado</response>
		[ProducesResponseType(200, Type = typeof(ObterDisciplinaDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		[HttpGet("{id}")]
		public async Task<IActionResult> ObterPorId([FromRoute(Name = "id")] Guid id)
		{
			var response = await _mediator.Send(new ObterDisciplinaQuery { Id = id });

			return this.ProcessResult(response);
		}


		/// <summary>
		/// Obtem listagem de todos os Professores de uma Disciplina
		/// </summary>
		/// <returns>Lista de todos os Professores</returns>
		/// <response code="200">Lista de Professores</response> 
		/// <response code="500">Erro inesperado</response>
		[ProducesResponseType(200, Type = typeof(IEnumerable<ObterProfessorDto>))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		[HttpGet("{disciplina-id}/professores")]
		public async Task<IActionResult> ObterTodosProfessoresDisciplina([FromRoute(Name = "disciplina-id")] Guid id)
		{
			var response = await _mediator.Send(new ObterProfessoresDisciplinaQuery { Id = id });

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Cria uma nova Disciplina
		/// </summary>
		/// <returns>Http status 201(Created)</returns>
		/// <response code="200">Disciplina criada com sucesso</response>
		/// <response code="400">Dados inconsistentes para criação da Disciplina</response>
		/// <response code="422">Erro de Negócio</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(422, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public async Task<IActionResult> CriarDisciplina([FromBody] AdicionarDisciplinaCommand disciplina)
		{
			var response = await _mediator.Send(disciplina);

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Efetua alteração de Disciplina
		/// </summary>
		/// <returns>Http status 204(No Content)</returns>
		/// <response code="204">Disciplina alterada com Sucesso</response>
		/// <response code="400">Dados para alteração de Disciplina inconsistentes.</response>
		/// <response code="404">Disciplina inexistente</response>
		/// <response code="422">Erro de Negócio</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpPut("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(422, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public async Task<IActionResult> AlterarDisciplina([FromRoute(Name = "id")] Guid id, [FromBody] AlterarDisciplinaCommand disciplinaDto)
		{
			if (disciplinaDto == null)
				throw new ArgumentNullException(null, "Objeto Disciplina nulo (não foi informado).");

			if (id.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Identificador de Disciplina é inválido ou nulo");
			disciplinaDto.ID = id;

			var response = await _mediator.Send(disciplinaDto);

			return this.ProcessResult(response);
		}

		/// <summary>
		/// Exclui uma Disciplina específica
		/// </summary>
		/// <response code="204">Disciplina excluída com Sucesso</response>
		/// <response code="400">Dados para exclusão da Disciplina inconsistentes.</response>
		/// <response code="404">Disciplina inexistente</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpDelete("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public async Task<IActionResult> RemoverDisciplina([FromRoute(Name = "id")] Guid id)
		{
			var response = await this._mediator.Send(new RemoverDisciplinaCommand { ID = id });
			return this.ProcessResult(response);
		}
	}
}
