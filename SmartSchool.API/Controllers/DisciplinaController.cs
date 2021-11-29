using Microsoft.AspNetCore.Mvc;
using SmartSchool.Aplicacao.Disciplinas.Interface;
using SmartSchool.Dto.Disciplinas;
using SmartSchool.Dto.Disciplinas.Alterar;
using SmartSchool.Dto.Disciplinas.Obter;
using SmartSchool.Dto.Dtos.Professores;
using SmartSchool.Dto.Dtos.TratamentoErros;
using System;
using System.Collections.Generic;
using System.Net;

namespace SmartSchool.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DisciplinaController : Controller
	{
		private readonly IDisciplinaServico _disciplinaServico;

		public DisciplinaController(IDisciplinaServico disciplinaServico)
		{
			this._disciplinaServico = disciplinaServico;
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
		public OkObjectResult ObterTodos() => Ok(_disciplinaServico.Obter());

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
		public OkObjectResult ObterPorId(Guid id) => Ok(this._disciplinaServico.ObterPorId(id));


		/// <summary>
		/// Obtem listagem de todos os Professores de uma Disciplina
		/// </summary>
		/// <returns>Lista de todos os Professores</returns>
		/// <response code="200">Lista de Professores</response> 
		/// <response code="500">Erro inesperado</response>
		[ProducesResponseType(200, Type = typeof(IEnumerable<ObterProfessorDto>))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		[HttpGet("{disciplina-id}/professores")]
		public OkObjectResult ObterTodosProfessoresDisciplina([FromRoute(Name = "disciplina-id")] Guid id)
		{
			return Ok(this._disciplinaServico.ObterProfessores(id));
		}

		/// <summary>
		/// Cria uma nova Disciplina
		/// </summary>
		/// <returns>Http status 201(Created)</returns>
		/// <response code="201">Disciplina criada com sucesso</response>
		/// <response code="400">Dados inconsistentes para criação da Disciplina</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public StatusCodeResult CriarDisciplina([FromBody] DisciplinaDto disciplinaDto)
		{
			this._disciplinaServico.CriarDisciplina(disciplinaDto);

			return this.StatusCode((int)HttpStatusCode.Created);
		}

		/// <summary>
		/// Efetua alteração de Disciplina
		/// </summary>
		/// <returns>Http status 204(No Content)</returns>
		/// <response code="204">Disciplina alterada com Sucesso</response>
		/// <response code="400">Dados para alteração de Disciplina inconsistentes.</response>
		/// <response code="404">Disciplina inexistente</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpPut("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public StatusCodeResult AlterarDisciplina(Guid id, [FromBody] AlterarDisciplinaDto disciplinaDto)
		{
			if (disciplinaDto == null)
				throw new ArgumentNullException(null, "Objeto Disciplina nulo (não foi informado).");

			if (id.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Identificador de Disciplina é inválido ou nulo");

			disciplinaDto.ID = id;

			this._disciplinaServico.AlterarDisciplina(id, disciplinaDto);

			return this.StatusCode((int)HttpStatusCode.Created);
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
		public StatusCodeResult ExcluirDisciplina(Guid id)
		{
			this._disciplinaServico.Remover(id);
			return this.StatusCode(204);
		}
	}
}
