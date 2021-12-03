using Microsoft.AspNetCore.Mvc;
using SmartSchool.Aplicacao.Professores.Interface;
using SmartSchool.Dto.Dtos.Professores;
using SmartSchool.Dto.Dtos.TratamentoErros;
using SmartSchool.Dto.Professores;
using System;
using System.Collections.Generic;
using System.Net;

namespace SmartSchool.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProfessorController : ControllerBase
	{
		private readonly IProfessorServico _professorServico;

		public ProfessorController(IProfessorServico professorServico)
		{
			this._professorServico = professorServico;
		}

		/// <summary>
		/// Obtem listagem de todos os Professores cadastrados
		/// </summary>
		/// <returns>Lista de todos os Professores</returns>
		/// <response code="200">Lista de Professores</response> 
		/// <response code="500">Erro inesperado</response>
		[ProducesResponseType(200, Type = typeof(IEnumerable<ObterProfessorDto>))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		[HttpGet]
		public OkObjectResult ObterTodos()
		{
			return Ok(_professorServico.Obter());
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
		public OkObjectResult ObterPorId(Guid id)
		{
			var professor = this._professorServico.ObterPorId(id);

			return Ok(professor);
		}

		/// <summary>
		/// Cria um novo Professor
		/// </summary>
		/// <returns>Http status 201(Created)</returns>
		/// <response code="201">Professor criado com sucesso</response>
		/// <response code="400">Dados inconsistentes para criação do Professor</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public StatusCodeResult CriarProfessor([FromBody] ProfessorDto professorDto)
		{
			this._professorServico.CriarProfessor(professorDto);

			return this.StatusCode((int)HttpStatusCode.Created);
		}

		/// <summary>
		/// Efetua alteração de Professor
		/// </summary>
		/// <returns>Http status 204(No Content)</returns>
		/// <response code="201">Professor alterado com Sucesso</response>
		/// <response code="400">Dados para alteração de Professor inconsistentes.</response>
		/// <response code="404">Professor inexistente</response>
		/// <response code="500">Erro inesperado</response> 
		[HttpPut("{id}")]
		[ProducesResponseType(201)]
		[ProducesResponseType(400, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(404, Type = typeof(TratamentoErroDto))]
		[ProducesResponseType(500, Type = typeof(TratamentoErroDto))]
		public StatusCodeResult AlterarProfessor(Guid id, AlterarProfessorDto professorDto, [FromQuery(Name = "atualizarDisciplinas")] bool? atualizarDisciplinas = null)
		{
			if (professorDto == null)
				throw new ArgumentNullException(null, "Objeto Professor nulo (não foi informado).");

			if (id.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Identificador do Professor é inválido ou nulo");

			professorDto.ID = id;

			this._professorServico.AlterarProfessor(id, professorDto, atualizarDisciplinas);

			return this.StatusCode((int)HttpStatusCode.Created);
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
		public StatusCodeResult ExcluirProfessor(Guid id)
		{
			this._professorServico.Remover(id);
			return this.StatusCode(204);
		}
		//    }
	}
}
